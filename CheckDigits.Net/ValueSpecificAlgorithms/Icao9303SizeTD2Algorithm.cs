// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used to validate the check digits contained in the MRZ (Machine 
///   Readable Zone) of ICAO (International Civil Aviation Organization) Machine 
///   Readable Travel Document Size TD2.
/// </summary>
/// <remarks>
///   <para>
///      Validates the following fields in the MRZ:
///   <list type="bullet">
///      <item>Document number, line 2, characters 1-9, check digit in character 10</item>
///      <item>Optional document number extension, line 2, characters 29-35, check digit in final non-filler character</item>
///      <item>Date of birth, line 2, characters 14-19, check digit in character 20</item>
///      <item>Date of expiry, line 2, characters 22-27, check digit in character 28</item>
///      <item>Composite check digit for all of the above fields and their check digits</item>
///   </list>
///   </para>
///   <para>
///      Valid characters are decimal digits ('0'-'9'), uppercase alphabetic 
///      characters ('A'-'Z') and a filler character ('&lt;') for the document 
///      number/document number extension fields and decimal digits ('0'-'9') 
///      and filler character ('&lt;') for date of birth and date of expiry 
///      fields. Valid characters for check digits are decimal digits ('0'-'9').
///      Note that characters in positions other than the document number, date 
///      of birth and date of expiry fields plus the composite check digit (and 
///      line separator characters) are not validated.
///   </para>
///   <para>
///      Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///      Uses the same Modulus 10 algorithm as the 
///      <see cref="Icao9303Algorithm"/> and has the same weaknesses as that 
///      algorithm.
///   </para>
/// </remarks>
public sealed class Icao9303SizeTD2Algorithm : ICheckDigitAlgorithm
{
   private const Int32 _initialWeight = 7;
   private static readonly FieldDetails[] _fields = [       // starting position, field length, valid num upper bound
      new (0, 9, 35),            // Document number field, allows alphanumeric characters plus filler char
      new (13, 6, 9),            // Date of birth field, allows digit characters plus filler char
      new (21, 6, 9)];           // Date of expiry field, allows digit characters plus filler char
   private static readonly FieldDetails _extendedDocumentNumber = new(28, 5, 35);   // Extended document number field is 7 chars in length with up to 5 extended document number chars, one check digit char and at least one trailing filler character

   private const Int32 _numFields = 3;
   private const Int32 _lineLength = 36;

   private const Int32 _nullSeparatorLength = _lineLength * 2;
   private const Int32 _crLfSeparatorLength = _nullSeparatorLength + 2;    // + "\r\n"
   private const Int32 _lfSeparatorLength = _nullSeparatorLength + 1;      // + "\n"

   private const Int32 _separatorPosition = _lineLength;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303SizeTD2AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303SizeTD2AlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || !TryValidateLengthAndSeparator(value, out var lineSeparatorLength))
      {
         return false;
      }

      Char ch;
      var compositeSum = 0;
      var compositeWeight = _initialWeight;
      for (var fieldIndex = 0; fieldIndex < _numFields; fieldIndex++)
      {
         Int32 num;
         var fieldSum = 0;
         var fieldWeight = _initialWeight;
         var fieldDetails = _fields[fieldIndex];
         var (start, end) = fieldDetails.GetFieldBounds(lineSeparatorLength);
         var numUpperBound = fieldDetails.NumUpperBound;
         for (var charIndex = start; charIndex < end; charIndex++)
         {
            num = value[charIndex].MapIcao9303Character();
            if (num < 0 || num > numUpperBound)
            {
               return false;
            }

            fieldSum += num * fieldWeight;
            fieldWeight = fieldWeight.Icao9303NextWeight();

            compositeSum += num * compositeWeight;
            compositeWeight = compositeWeight.Icao9303NextWeight();
         }

         // Handle possible extended document number field. If the document 
         // number is greater than 9 characters in length, then the extra
         // characters are placed in the extended document number field. An 
         // extended document number is indicated by a filler character '<' in
         // the document number check digit position. See
         // https://www.icao.int/publications/Documents/9303_p6_cons_en.pdf,
         // note j on page 16 for details.
         if (fieldIndex == 0 && value[end] == Chars.LeftAngleBracket)
         {
            // The extended document number has variable length. The extended 
            // document number field has a maximum length of 7 characters, with
            // up to 5 extended document number characters and one check digit 
            // character and at least one filler character. The check digit
            // character follows the last extended document number character and
            // any remaining positions in the field are filled with filler
            // characters.
            (start, end) = _extendedDocumentNumber.GetFieldBounds(lineSeparatorLength);
            numUpperBound = _extendedDocumentNumber.NumUpperBound;
            for (var charIndex = start; charIndex < end; charIndex++)
            {
               // Exit the loop if the character at charIndex + 1 is a filler
               // character. If it is a filler character, it means that the
               // current character is the check digit character. (The
               // definition of _extendedDocumentNumber ensures that the one
               // character look ahead will always be in bounds.)
               if (value[charIndex + 1] == Chars.LeftAngleBracket)
               {
                  end = charIndex;     // Set end to the current charIndex so that we can process the field check digit in the main loop.
                  break;
               }

               num = value[charIndex].MapIcao9303Character();
               if (num < 0 || num > numUpperBound)
               {
                  return false;
               }

               fieldSum += num * fieldWeight;
               fieldWeight = fieldWeight.Icao9303NextWeight();

               compositeSum += num * compositeWeight;
               compositeWeight = compositeWeight.Icao9303NextWeight();
            }
         }

         // Field check digit.
         ch = value[end];
         if (!ch.IsAsciiDigit())
         {
            return false;
         }

         num = ch.MapAsciiDigit();
         if (num != fieldSum % 10)
         {
            return false;
         }

         compositeSum += num * compositeWeight;
         compositeWeight = compositeWeight.Icao9303NextWeight();
      }

      var compositeCheckDigit = compositeSum % 10;

      ch = value[^1];
      if (!ch.IsAsciiDigit())
      {
         return false;
      }

      return ch.MapAsciiDigit() == compositeCheckDigit;
   }

   private static Boolean TryValidateLengthAndSeparator(String value, out Int32 lineSeparatorLength)
   {
      lineSeparatorLength = value.Length switch
      {
         _nullSeparatorLength => 0,
         _crLfSeparatorLength => ValidateCrlfSeparator(value) ? 2 : -1,
         _lfSeparatorLength => ValidateLfSeparator(value) ? 1 : -1,
         _ => -1
      };

      return lineSeparatorLength != -1;
   }

   private static Boolean ValidateCrlfSeparator(String value)
      => value[_separatorPosition] == Chars.CarriageReturn 
         && value[_separatorPosition + 1] == Chars.LineFeed;

   private static Boolean ValidateLfSeparator(String value)
      => value[_separatorPosition] == Chars.LineFeed ;

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
#if NET7_0_OR_GREATER
   private readonly record struct FieldDetails(
#else
   private record struct FieldDetails(
#endif
      Int32 CharPosition, 
      Int32 Length, 
      Int32 NumUpperBound)
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
