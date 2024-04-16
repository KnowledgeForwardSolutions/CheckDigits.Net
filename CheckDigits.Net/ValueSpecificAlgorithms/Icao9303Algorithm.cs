// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Modulus 10 algorithm used by ICAO (International Civil Aviation 
///   Organization) Machine Readable Travel Documents. The algorithm uses 
///   weights 7, 3 and 1 (weights are applied starting from the left-most 
///   character).
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are decimal digits (0-9), uppercase alphabetic characters 
///   (A-Z) and a filler character ('<').
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit is the right-most digit in the input value.
///   </para>
///   <para>
///   Can not detect single character transcription errors where the difference
///   between the correct and incorrect characters is 10, i.e. 0 -> A, B->L. Nor
///   can the algorithm detect two character transposition errors where the
///   difference between the transposed characters is 10, i.e. BL <-> LB.
///   </para>
/// </remarks>
public sealed class Icao9303Algorithm : IEmbeddedCheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [7, 3, 1];
   private static readonly Int32[] _charMap = Icao9303CharacterMap.GetCharacterMap();

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303AlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < 2)
      {
         return false;
      }

      Char ch;
      Int32 num;
      var sum = 0;
      var weightIndex = new ModulusInt32(3);
      for (var charIndex = 0; charIndex < value.Length - 1; charIndex++)
      {
         ch = value[charIndex];
         num = (ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ)
            ? _charMap[ch - CharConstants.DigitZero]
            : -1;
         if (num == -1)
         {
            return false;
         }
         sum += num * _weights[weightIndex];
         weightIndex++;
      }
      var checkDigit = sum % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, Int32 start, Int32 length)
   {
      if (String.IsNullOrEmpty(value) 
         || start < 0
         || length < 2
         || start + length > value.Length)
      {
         return false;
      }

      Char ch;
      Int32 num;
      var sum = 0;
      var weightIndex = new ModulusInt32(3);
      var end = start + length - 1;
      for(var charIndex = start; charIndex < end; charIndex++)
      {
         ch = value[charIndex];
         num = (ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ)
            ? _charMap[ch - CharConstants.DigitZero]
            : -1;
         if (num == -1)
         {
            return false;
         }
         sum += num * _weights[weightIndex];
         weightIndex++;
      }
      var checkDigit = sum % 10;

      return value[start + length - 1].ToIntegerDigit() == checkDigit;
   }
}
