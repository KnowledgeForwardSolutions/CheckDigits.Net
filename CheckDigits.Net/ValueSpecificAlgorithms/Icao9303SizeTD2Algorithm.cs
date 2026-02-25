// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used to validate the check digits contained in the MRZ (Machine 
///   Readable Zone) of ICAO (International Civil Aviation Organization) Machine 
///   Readable Travel Document Size TD2.
/// </summary>
/// <remarks>
///   <para>
///   Validates the following fields in the MRZ:
///   <list type="bullet">
///      <item>Document number, line 2, characters 1-9, check digit in character 10</item>
///      <item>Optional document number extension, line 2, characters 29-35, check digit in final non-filler character</item>
///      <item>Date of birth, line 2, characters 14-19, check digit in character 20</item>
///      <item>Date of expiry, line 2, characters 22-27, check digit in character 28</item>
///      <item>Composite check digit for all of the above fields and their check digits</item>
///   </list>
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9), uppercase alphabetic characters 
///   (A-Z) and a filler character ('<') for the document number/document number
///   extension fields and decimal digits (0-9) and filler character ('<') for
///   date of birth and date of expiry fields. Valid characters for check digits
///   are decimal digits (0-9). Note that characters in positions other than the 
///   document number, date of birth and date of expiry fields plus the 
///   composite check digit (and line separator characters) are not validated.
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Uses the same Modulus 10 algorithm as the <see cref="Icao9303Algorithm"/>
///   and has the same weaknesses as that algorithm.
///   </para>
/// </remarks>
public sealed class Icao9303SizeTD2Algorithm : ICheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [7, 3, 1];
   private static readonly FieldDetails[] _fields = [  // starting position, field length, valid num upper bound
      new (0, 9, 35),    // Document number field
      new (13, 6, 9),    // Date of birth field
      new (21, 6, 9)];   // Date of expiry field
   private static readonly FieldDetails _extendedDocumentNumber = new(28, 5, 35);
   private static readonly Int32[] _charMap = 
      [.. Chars.Range(Chars.DigitZero, Chars.UpperCaseZ).Select(x => Icao9303Algorithm.MapCharacter(x))];

   private const Int32 _numFields = 3;
   private const Int32 _lineLength = 36;

   private const Int32 _nullSeparatorLength = _lineLength * 2;
   private const Int32 _crLfSeparatorLength = _nullSeparatorLength + 2;    // + "\r\n"
   private const Int32 _lfSeparatorLength = _nullSeparatorLength + 1;      // + "\n"

   // Only retained for obsolete LineSeparator property.
   private LineSeparator _lineSeparator = LineSeparator.None;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303SizeTD2AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303SizeTD2AlgorithmName;

   /// <summary>
   ///   Specifies the character(s) used to separate lines in the value being
   ///   validated.
   /// </summary>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   The value used to set the <see cref="LineSeparator"/> property is not
   ///   a defined member of the <see cref="LineSeparator"/> enumeration.
   /// </exception>
   [Obsolete("The LineSeparator property is deprecated and will be removed in a future version. " +
             "The algorithm will automatically detect the line separator used in the value being validated.")]
   public LineSeparator LineSeparator
   {
      get => _lineSeparator;
      set
      {
         if (!value.IsDefined())
         {
            throw new ArgumentOutOfRangeException(nameof(value), value, Resources.LineSeparatorInvalidValueMessage);
         }

         _lineSeparator = value;
      }
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || !TryValidateLength(value, out var lineSeparatorLength))
      {
         return false;
      }

      var compositeSum = 0;
      var compositeWeightIndex = new ModulusInt32(3);
      for (var fieldIndex = 0; fieldIndex < _numFields; fieldIndex++)
      {
         Int32 num;
         var fieldSum = 0;
         var fieldWeightIndex = new ModulusInt32(3);
         var (start, end) = _fields[fieldIndex].GetFieldBounds(lineSeparatorLength);
         var numUpperBound = _fields[fieldIndex].NumUpperBound;
         for (var charIndex = start; charIndex < end; charIndex++)
         {
            num = ToIcao9303IntegerValue(value[charIndex]);
            if (IsInvalidValueForField(num, numUpperBound))
            {
               return false;
            }

            fieldSum += num * _weights[fieldWeightIndex];
            fieldWeightIndex++;

            compositeSum += num * _weights[compositeWeightIndex];
            compositeWeightIndex++;
         }

         // Handle possible extended document number field. See
         // https://www.icao.int/publications/Documents/9303_p6_cons_en.pdf,
         // note j on page 16 for details.
         if (fieldIndex == 0 && value[end] == Chars.LeftAngleBracket)
         {
            (start, end) = _extendedDocumentNumber.GetFieldBounds(lineSeparatorLength);
            numUpperBound = _extendedDocumentNumber.NumUpperBound;
            for (var charIndex = start; charIndex < end; charIndex++)
            {
               // Stop processing if we've reached the check digit character
               // early (i.e. before reaching the end of the field).  This is
               // signaled by the next character after the check digit character
               // being a filler character ('<'). This means we test by looking
               // ahead by one character. (The definition of _extendedDocumentNumber
               // ensures that the one character look ahead will always be in bounds.)
               if (value[charIndex + 1] == Chars.LeftAngleBracket)
               {
                  end = charIndex;
                  break;
               }

               num = ToIcao9303IntegerValue(value[charIndex]);
               if (IsInvalidValueForField(num, numUpperBound))
               {
                  return false;
               }

               fieldSum += num * _weights[fieldWeightIndex];
               fieldWeightIndex++;

               compositeSum += num * _weights[compositeWeightIndex];
               compositeWeightIndex++;
            }
         }

         // Field check digit.
         num = value[end].ToIntegerDigit();
         if (num.IsInvalidDigit())
         {
            return false;
         }
         if (num != fieldSum % 10)
         {
            return false;
         }

         compositeSum += num * _weights[compositeWeightIndex];
         compositeWeightIndex++;
      }

      var compositeCheckDigit = compositeSum % 10;

      return value[^1].ToIntegerDigit() == compositeCheckDigit;
   }

   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   private static Int32 ToIcao9303IntegerValue(Char ch)
      => (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
         ? _charMap[ch - Chars.DigitZero]
         : -1;

   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   private static Boolean IsInvalidValueForField(Int32 value, Int32 numUpperBound)
      => value < 0 || value > numUpperBound;

   private static Boolean TryValidateLength(String value, out Int32 lineSeparatorLength)
   {
      lineSeparatorLength = value.Length switch
      {
         _nullSeparatorLength => 0,
         _crLfSeparatorLength => 2,
         _lfSeparatorLength => 1,
         _ => -1
      };

      if (lineSeparatorLength == -1)
      {
         return false;
      }

      // Validate separator characters if present
      if (lineSeparatorLength == 2)
      {
         // Should have \r\n at positions 37
         return value[_lineLength] == Chars.CarriageReturn
                && value[_lineLength + 1] == Chars.LineFeed;
      }
      else if (lineSeparatorLength == 1)
      {
         // Should have \n at positions 37
         return value[_lineLength] == Chars.LineFeed;
      }

      return true;  // No separator to validate
   }

   /// <summary>
   ///   Represents the positional and size information for a field within a 
   ///   data line, including its starting character position, length, and the 
   ///   upper bound of a field character when converted to an integer.
   /// </summary>
   /// <param name="CharPosition">
   ///   The zero-based character position at which the field begins within the line.
   /// </param>
   /// <param name="Length">
   ///   The number of characters that make up the field.
   /// </param>
   /// <param name="NumUpperBound">
   ///   The upper bound of a field character when converted to an integer.
   /// </param>
   private record struct FieldDetails(Int32 CharPosition, Int32 Length, Int32 NumUpperBound)
   {
      [Pure]
      public readonly (Int32 start, Int32 end) GetFieldBounds(Int32 lineSeparatorLength)
      {
         // Always add _lineLength because all fields are on the second line of data.
         var start = _lineLength + lineSeparatorLength + CharPosition;
         var end = start + Length;

         return (start, end);
      }
   }
}
