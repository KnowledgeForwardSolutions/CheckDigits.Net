// Ignore Spelling: Ncd

namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   NOID Check Digit Algorithm for betanumeric characters and modulus 29.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are betanumeric characters (0123456789bcdfghjkmnpqrstvwxz).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a betanumeric character (0123456789bcdfghjkmnpqrstvwxz).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-character transcription errors and all transpositions 
///   of adjacent characters when the value's length is less than 29 characters.
///   Slightly less capable for values of 29 characters or greater.
///   </para>
/// </remarks>
public class NcdAlgorithm : ISingleCheckDigitAlgorithm
{
   private const String _checkCharacters = "0123456789bcdfghjkmnpqrstvwxz";
   private static readonly Int32[] _charMap = Chars.Range(Chars.DigitZero, Chars.LowerCaseZ)
      .Select(x => MapCharacter(x))
      .ToArray();

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.NcdAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.NcdAlgorithmName;

   public static Int32 MapCharacter(Char ch) => ch switch
   {
      var d when ch >= Chars.DigitZero && ch <= Chars.DigitNine => d.ToIntegerDigit(),
      var b when ch >= Chars.LowerCaseB && ch <= Chars.LowerCaseD => b - Chars.LowerCaseB + 10,
      var f when ch >= Chars.LowerCaseF && ch <= Chars.LowerCaseH => f - Chars.LowerCaseF + 13,
      Chars.LowerCaseJ => 16,
      Chars.LowerCaseK => 17,
      Chars.LowerCaseM => 18,
      Chars.LowerCaseN => 19,
      var p when ch >= Chars.LowerCaseP && ch <= Chars.LowerCaseT => p - Chars.LowerCaseP + 20,
      var v when ch >= Chars.LowerCaseV && ch <= Chars.LowerCaseX => v - Chars.LowerCaseV + 25,
      Chars.LowerCaseZ => 28,
      _ => 0
   };

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      Int32 num;
      var sum = 0;
      for (var index = 0; index < value.Length; index++)
      {
         var ch = value[index];
         // Benchmarks show that this version of character mapping is more
         // performant TryCalculateCheckDigit than the version used in the
         // Validate method.
         num = ch >= Chars.DigitZero && ch <= Chars.DigitNine
            ? ch.ToIntegerDigit()
            : ch >= Chars.LowerCaseB && ch <= Chars.LowerCaseZ
               ? _charMap[ch - Chars.DigitZero]
               : 0;
         sum += num * (index + 1);
      }

      var checksum = sum % 29;
      checkDigit = _checkCharacters[checksum];

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < 2)
      {
         return false;
      }

      Int32 num;
      var sum = 0;
      for (var index = 0; index < value.Length - 1; index++)
      {
         var ch = value[index];
         num = ch >= Chars.DigitZero && ch <= Chars.LowerCaseZ
            ? _charMap[ch - Chars.DigitZero]
            : 0;
         sum += num * (index + 1);
      }

      var checksum = sum % 29;

      return value[^1] == _checkCharacters[checksum];
   }
}
