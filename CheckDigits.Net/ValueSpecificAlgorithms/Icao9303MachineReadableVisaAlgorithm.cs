// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used to validate the check digits contained in the machine readable
///   zone of ICAO (International Civil Aviation Organization) Machine Readable 
///   Travel Visas (both format MRV-A and format MRV-B).
/// </summary>
/// <remarks>
///   <para>
///   Validates the following fields in the machine readable zone:
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
public sealed class Icao9303MachineReadableVisaAlgorithm : ICheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [7, 3, 1];
   private static readonly Int32[] _fieldStartPositions = [0, 13, 21];
   private static readonly Int32[] _fieldSLengths = [9, 6, 6];
   private static readonly Int32[] _charMap = Icao9303CharacterMap.GetCharacterMap();

   private const Int32 _numFields = 3;
   private const Int32 _formatALineLength = 44;
   private const Int32 _formatBLineLength = 36;
   private LineSeparator _lineSeparator = LineSeparator.None;
   private Int32 _lineSeparatorLength = 0;

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
         _lineSeparatorLength = value.CharacterLength();
      }
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }
      Int32 lineLength;
      if (value.Length == (_formatALineLength * 2) + _lineSeparatorLength)
      {
         lineLength = _formatALineLength;
      }
      else if (value.Length == (_formatBLineLength * 2) + _lineSeparatorLength)
      {
         lineLength = _formatBLineLength;
      }
      else
      {
         return false;
      }

      Char ch;
      Int32 num;
      for (var fieldIndex = 0; fieldIndex < _numFields; fieldIndex++)
      {
         var fieldSum = 0;
         var fieldWeightIndex = new ModulusInt32(3);
         var start = _fieldStartPositions[fieldIndex] + lineLength + _lineSeparatorLength;
         var end = start + _fieldSLengths[fieldIndex];
         for (var charIndex = start; charIndex < end; charIndex++)
         {
            ch = value[charIndex];
            num = (ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ)
               ? _charMap[ch - CharConstants.DigitZero]
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
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
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
}
