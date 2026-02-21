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
///   </list>
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9), uppercase alphabetic characters 
///   (A-Z) and a filler character ('<').
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
   private static readonly Int32[] _fieldSLengths = [9, 6, 6];
   private static readonly Int32[] _charMap = Chars.Range(Chars.DigitZero, Chars.UpperCaseZ)
      .Select(x => Icao9303Algorithm.MapCharacter(x))
      .ToArray();

   private const Int32 _numFields = 3;
   private const Int32 _formatALineLength = 44;
   private const Int32 _formatBLineLength = 36;
   private readonly LineSeparator _lineSeparator = LineSeparator.None;

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
         var end = start + _fieldSLengths[fieldIndex];
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
         ch = value[end];
         if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else
         {
            return false;
         }
         if (num != fieldSum % 10)
         {
            return false;
         }
      }

      return true;
   }

   private static Boolean TryValidateLength(
      String value, 
      out Int32 lineLength,
      out Int32 lineSeparatorLength)
   {
      lineSeparatorLength = 0;
      if (value.Contains(Chars.CarriageReturn))
      {
         lineSeparatorLength = 2;
      }
      else if (value.Contains(Chars.LineFeed))
      {
         lineSeparatorLength = 1;
      }

      if (value.Length == _formatALineLength + lineSeparatorLength + _formatALineLength)
      {
         lineLength = _formatALineLength;
         return true;
      }
      else if (value.Length == _formatBLineLength + lineSeparatorLength + _formatBLineLength)
      {
         lineLength = _formatBLineLength;
         return true;
      }

      lineLength = 0;
      return false;
   }
}
