// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used to validate the check digits contained in the machine-readable
///   zone of ICAO (International Civil Aviation Organization) Machine Readable 
///   Travel Visas (both format MRV-A and format MRV-B).
/// </summary>
/// <remarks>
///   <para>
///   Validates the following fields in the machine-readable zone:
///   <list type="bullet">
///      <item>Document number, line 2, characters 1-9, check digit in character 10</item>
///      <item>Date of birth, line 2, characters 14-19, check digit in character 20</item>
///      <item>Date of expiry, line 2, characters 22-27, check digit in character 28</item>
///      <item>Line separator characters in expected location, if length of the value indicates that a line separator was used</item>
///   </list>
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9), uppercase alphabetic characters 
///   (A-Z) and a filler character ('<'). Note that characters in positions
///   other than the document number, date of birth and date of expiry fields 
///   (and line separator characters) are not validated.
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Uses the same Modulus 10 algorithm as the <see cref="Icao9303Algorithm"/>
///   and has the same weaknesses as that algorithm.
///   </para>
/// </remarks>
public sealed class Icao9303MachineReadableVisaAlgorithm : ICheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [7, 3, 1];
   private static readonly Int32[] _fieldStartPositions = [0, 13, 21];
   private static readonly Int32[] _fieldLengths = [9, 6, 6];
   private static readonly Int32[] _charMap = Chars.Range(Chars.DigitZero, Chars.UpperCaseZ)
      .Select(x => Icao9303Algorithm.MapCharacter(x))
      .ToArray();

   private const Int32 _numFields = 3;
   private const Int32 _formatALineLength = 44;
   private const Int32 _formatBLineLength = 36;

   private const Int32 _formatANullLength = _formatALineLength * 2;
   private const Int32 _formatACrlfLength = _formatANullLength + 2;
   private const Int32 _formatALfLength = _formatANullLength + 1;

   private const Int32 _formatBNullLength = _formatBLineLength * 2;
   private const Int32 _formatBCrlfLength = _formatBNullLength + 2;
   private const Int32 _formatBLfLength = _formatBNullLength + 1;

   // Only retained for obsolete LineSeparator property.
   private LineSeparator _lineSeparator = LineSeparator.None;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303MachineReadableVisaAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303MachineReadableVisaAlgorithmName;

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
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }
      if (!TryValidateLength(value, out var lineLength, out var lineSeparatorLength))
      {
         return false;
      }

      Char ch;
      Int32 num;
      for (var fieldIndex = 0; fieldIndex < _numFields; fieldIndex++)
      {
         var fieldSum = 0;
         var fieldWeightIndex = new ModulusInt32(3);
         var start = _fieldStartPositions[fieldIndex] + lineLength + lineSeparatorLength;
         var end = start + _fieldLengths[fieldIndex];
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
         }

         // Field check digit.
         num = value[end].ToIntegerDigit();
         if (num.IsInvalidDigit() || num != fieldSum % 10)
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
      lineSeparatorLength = 0;
      var valueLength = value.Length;
      if (valueLength >= _formatANullLength && valueLength <= _formatACrlfLength)
      {
         lineLength = _formatALineLength;
         lineSeparatorLength = valueLength - _formatANullLength;
         return valueLength == _formatANullLength
                || (valueLength == _formatACrlfLength
                    && value[_formatALineLength] == Chars.CarriageReturn
                    && value[_formatALineLength + 1] == Chars.LineFeed)
                || (valueLength == _formatALfLength
                    && value[_formatALineLength] == Chars.LineFeed);
      }
      else if (valueLength >= _formatBNullLength && valueLength <= _formatBCrlfLength)
      {
         lineLength = _formatBLineLength;
         lineSeparatorLength = valueLength - _formatBNullLength;
         return valueLength == _formatBNullLength
                || (valueLength == _formatBCrlfLength
                    && value[_formatBLineLength] == Chars.CarriageReturn
                    && value[_formatBLineLength + 1] == Chars.LineFeed)
                || (valueLength == _formatBLfLength
                    && value[_formatBLineLength] == Chars.LineFeed);
      }
      lineLength = 0;

      return false;
   }
}
