// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used to validate the check digits contained in the MRZ (Machine
///   Readable Zone) of ICAO (International Civil Aviation Organization) Machine 
///   Readable Travel Visas (both format MRV-A and format MRV-B).
/// </summary>
/// <remarks>
///   <para>
///      Validates the following fields in the MRZ:
///      <list type="bullet">
///         <item>Document number, line 2, characters 1-9, check digit in character 10</item>
///         <item>Date of birth, line 2, characters 14-19, check digit in character 20</item>
///         <item>Date of expiry, line 2, characters 22-27, check digit in character 28</item>
///         <item>Line separator characters in expected location, if length of the value indicates that a line separator was used</item>
///      </list>
///   </para>
///   <para>
///      Valid characters are decimal digits (0-9), uppercase alphabetic 
///      characters (A-Z) and a filler character ('&lt;'). Note that characters 
///      in positions other than the document number, date of birth and date of 
///      expiry fields (and line separator characters) are not validated.
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
public sealed class Icao9303MachineReadableVisaAlgorithm : ICheckDigitAlgorithm
{
   private const Int32 _initialWeight = 7;
   private static readonly FieldDetails[] _fields = [       // starting position, field length, valid num upper bound
      new (0, 9, 35),         // Document number field, allows alphanumeric characters plus placeholder char
      new (13, 6, 9),         // Date of birth field, allows digit characters plus placeholder char
      new (21, 6, 9)];        // Date of expiry field, allows digit characters plus placeholder char

   private const Int32 _numFields = 3;
   private const Int32 _formatALineLength = 44;
   private const Int32 _formatBLineLength = 36;

   // Format A valid lengths for the three supported line separator options.
   private const Int32 _formatANullLength = _formatALineLength * 2;
   private const Int32 _formatACrlfLength = _formatANullLength + 2;
   private const Int32 _formatALfLength = _formatANullLength + 1;

   // Format B valid lengths for the three supported line separator options.
   private const Int32 _formatBNullLength = _formatBLineLength * 2;
   private const Int32 _formatBCrlfLength = _formatBNullLength + 2;
   private const Int32 _formatBLfLength = _formatBNullLength + 1;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303MachineReadableVisaAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303MachineReadableVisaAlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }
      if (!TryValidateLength(value, out var lineLength, out var lineSeparatorLength))
      {
         return false;
      }

      for (var fieldIndex = 0; fieldIndex < _numFields; fieldIndex++)
      {
         Int32 num;
         var fieldSum = 0;
         var fieldWeight = _initialWeight;
         var (start, end) = _fields[fieldIndex].GetFieldBounds(lineLength, lineSeparatorLength);
         var numUpperBound = _fields[fieldIndex].NumUpperBound;
         for (var charIndex = start; charIndex < end; charIndex++)
         {
            num = value[charIndex].MapIcao0303Character();
            if (num < 0 || num > numUpperBound)
            {
               return false;
            }

            fieldSum += num * fieldWeight;
            fieldWeight = fieldWeight.Icao9303NextWeight();
         }

         // Field check digit.
         var ch = value[end];
         if (!ch.IsAsciiDigit())
         {
            return false;
         }

         num = ch.MapAsciiDigit();
         if (num != fieldSum % 10)
         {
            return false;
         }
      }

      return true;
   }

   /// <summary>
   ///   Validates that the length of the input <paramref name="value"/> is
   ///   valid, including the separator between lines in the value. If the
   ///   length of the value indicates that a line separator other than an empty
   ///   string is used, then the line separator characters are validated to be
   ///   correct for the value length.
   /// </summary>
   private static Boolean TryValidateLength(
      String value,
      out Int32 lineLength,
      out Int32 lineSeparatorLength)
   {
      (lineLength, lineSeparatorLength) = value.Length switch
      {
         _formatANullLength => (_formatALineLength, 0),
         _formatACrlfLength => (_formatALineLength, 2),
         _formatALfLength => (_formatALineLength, 1),
         _formatBNullLength => (_formatBLineLength, 0),
         _formatBCrlfLength => (_formatBLineLength, 2),
         _formatBLfLength => (_formatBLineLength, 1),
         _ => (-1, 0)
      };

      return lineLength != -1 && ValidateSeparator(value, lineLength, lineSeparatorLength);
   }

   /// <summary>
   ///   Validates that the separator character(s) (if any) are correct for the
   ///   specified line length.
   /// </summary>
   private static Boolean ValidateSeparator(
      String value,
      Int32 lineLength,
      Int32 lineSeparatorLength)
   {
      return lineSeparatorLength switch
      {
         0 => true,
         1 => value[lineLength] == Chars.LineFeed,
         2 => value[lineLength] == Chars.CarriageReturn
              && value[lineLength + 1] == Chars.LineFeed,
         _ => false
      };
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
   private 
#if NET7_0_OR_GREATER
      readonly
#endif
      record struct FieldDetails(Int32 CharPosition, Int32 Length, Int32 NumUpperBound)
   {
      [Pure]
      public readonly (Int32 start, Int32 end) GetFieldBounds(
         Int32 lineLength,
         Int32 lineSeparatorLength)
      {
         // Always add _lineLength because all fields are on the second line of data.
         var start = lineLength + lineSeparatorLength + CharPosition;
         var end = start + Length;

         return (start, end);
      }
   }
}
