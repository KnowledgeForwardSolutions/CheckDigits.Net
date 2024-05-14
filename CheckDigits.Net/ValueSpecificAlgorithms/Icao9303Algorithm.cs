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
public sealed class Icao9303Algorithm : ISingleCheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [7, 3, 1];
   private static readonly Int32[] _charMap = Chars.Range(Chars.DigitZero, Chars.UpperCaseZ)
      .Select(x => MapCharacter(x))
      .ToArray();
   private const Int32 _validateMinLength = 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303AlgorithmName;

   /// <summary>
   ///   Map a character to its integer equivalent in the 
   ///   <see cref="Icao9303Algorithm"/>. Characters that are not valid for the 
   ///   Icao9303Algorithm are mapped to -1.
   /// </summary>
   /// <param name="ch">
   ///   The character to map.
   /// </param>
   /// <returns>
   ///   The integer value associated with <paramref name="ch"/>.
   /// </returns>
   public static Int32 MapCharacter(Char ch) => ch switch
   {
      var d when ch >= Chars.DigitZero && ch <= Chars.DigitNine => d.ToIntegerDigit(),
      var c when ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ => c - Chars.UpperCaseA + 10,
      Chars.LeftAngleBracket => 0,
      _ => -1
   };

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      Char ch;
      Int32 num;
      var sum = 0;
      var weightIndex = new ModulusInt32(3);
      for (var charIndex = 0; charIndex < value.Length; charIndex++)
      {
         ch = value[charIndex];
         num = (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
            ? _charMap[ch - Chars.DigitZero]
            : -1;
         if (num == -1)
         {
            return false;
         }
         sum += num * _weights[weightIndex];
         weightIndex++;
      }
      
      checkDigit = (sum % 10).ToDigitChar();

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
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
         num = (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
            ? _charMap[ch - Chars.DigitZero]
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
}
