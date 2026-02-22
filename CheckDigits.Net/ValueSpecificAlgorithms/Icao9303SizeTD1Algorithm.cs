// Ignore Spelling: Icao

using System.Diagnostics.Contracts;

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used to validate the check digits contained in the machine readable
///   zone of ICAO (International Civil Aviation Organization) Machine Readable 
///   Travel Document Size TD1.
/// </summary>
/// <remarks>
///   <para>
///   Validates the following fields in the machine readable zone:
///   <list type="bullet">
///      <item>Document number, line 1, characters 6-14, check digit in character 15</item>
///      <item>Optional document number extension, line 1, characters 16-28, check digit in final non-filler character</item>
///      <item>Date of birth, line 2, characters 1-6, check digit in character 7</item>
///      <item>Date of expiry, line 2, characters 9-14, check digit in character 15</item>
///      <item>Composite check digit for all of the above fields and their check digits</item>
///      <item>Line separator characters in expected location, if length of the value indicates that a line separator was used</item>
///   </list>
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9), uppercase alphabetic characters 
///   (A-Z) and a filler character ('<'). Note that characters in positions
///   other than the document number, date of birth and date of expiry fields 
///   plus the composite check digit (and line separator characters) are not 
///   validated.
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Uses the same Modulus 10 algorithm as the <see cref="Icao9303Algorithm"/>
///   and has the same weaknesses as that algorithm.
///   </para>
/// </remarks>
public sealed class Icao9303SizeTD1Algorithm : ICheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [7, 3, 1];
   private static readonly FieldDetails[] _fields = [  // line #, starting position, field length
      new (0, 5, 9),    // Document number field
      new (1, 0, 6),    // Date of birth field
      new (1, 8, 6)];   // Date of expiry field
   private static readonly FieldDetails _extendedDocumentNumber = new (0, 15, 13);
   private const Int32 _numFields = 3;
   private const Int32 _lineLength = 30;
   private const Int32 _compositeCheckDigitPosition = 59;
   private static readonly Int32[] _charMap = Chars.Range(Chars.DigitZero, Chars.UpperCaseZ)
      .Select(x => Icao9303Algorithm.MapCharacter(x))
      .ToArray();

   private const Int32 _nullSeparatorLength = _lineLength * 3;
   private const Int32 _crLfSeparatorLength = _nullSeparatorLength + 4;    //  "\r\n" * 2
   private const Int32 _lfSeparatorLength = _nullSeparatorLength + 2;      // "\n" * 2

   // Only retained for obsolete LineSeparator property.
   private LineSeparator _lineSeparator = LineSeparator.None;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303SizeTD1AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303SizeTD1AlgorithmName;

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

      Char ch;
      Int32 num;
      var compositeSum = 0;
      var compositeWeightIndex = new ModulusInt32(3);
      for (var fieldIndex = 0; fieldIndex < _numFields; fieldIndex++)
      {
         var fieldSum = 0;
         var fieldWeightIndex = new ModulusInt32(3);
         var (start, end) = _fields[fieldIndex].GetFieldBounds(lineSeparatorLength);
         for (var charIndex = start; charIndex < end; charIndex++)
         {
            ch = value[charIndex];
            num = (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
               ? _charMap[ch - Chars.DigitZero]
               : -1;
            if (num == -1)
            {
               return false;
            }

            fieldSum += num * _weights[fieldWeightIndex];
            fieldWeightIndex++;

            compositeSum += num * _weights[compositeWeightIndex];
            compositeWeightIndex++;
         }

         // Handle possible extended document number field. See
         // https://www.icao.int/publications/Documents/9303_p5_cons_en.pdf,
         // note j on page 17 for details.
         if (fieldIndex == 0 && value[end] == Chars.LeftAngleBracket)
         {
            (start, end) = _extendedDocumentNumber.GetFieldBounds(lineSeparatorLength);
            for (var charIndex = start; charIndex < end; charIndex++)
            {
               // Stop processing if we've reached the check digit character
               // before reaching the end of the field.  This is signaled by the
               // next character after the check digit character being a filler
               // character ('<'). This means we test by looking ahead by one
               // character. (The definition of _extendedDocumentNumber ensures
               // that the one character look ahead will always be in bounds.)
               if (value[charIndex + 1] == Chars.LeftAngleBracket)
               {
                  end = charIndex;
                  break;
               }

               ch = value[charIndex];
               num = (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
                  ? _charMap[ch - Chars.DigitZero]
                  : -1;
               if (num == -1)
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
      return value[_compositeCheckDigitPosition + lineSeparatorLength].ToIntegerDigit() == compositeCheckDigit;
   }

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
         // Should have \r\n at positions 30 and 61
         return value[_lineLength] == Chars.CarriageReturn
                && value[_lineLength + 1] == Chars.LineFeed
                && value[_lineLength * 2 + 2] == Chars.CarriageReturn
                && value[_lineLength * 2 + 3] == Chars.LineFeed;
      }
      else if (lineSeparatorLength == 1)
      {
         // Should have \n at positions 30 and 61
         return value[_lineLength] == Chars.LineFeed
                && value[_lineLength * 2 + 1] == Chars.LineFeed;
      }

      return true;  // No separator to validate
   }

   private record struct FieldDetails(Int32 Line, Int32 CharPosition, Int32 Length)
   {
      [Pure]
      public (Int32 start, Int32 end) GetFieldBounds(Int32 lineSeparatorLength)
      {
         var start = (Line * (_lineLength + lineSeparatorLength)) + CharPosition;
         var end = start + Length;

         return (start, end);
      }
   }
}
