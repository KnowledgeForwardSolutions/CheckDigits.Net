// Ignore Spelling: Ncd

namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   NOID Check Digit Algorithm for betanumeric characters and modulus 29.
/// </summary>
/// <remarks>
///   <para>
///      Valid characters are betanumeric characters 
///      (0123456789bcdfghjkmnpqrstvwxz).
///   </para>
///   <para>
///      Check digit calculated by the algorithm is a betanumeric character 
///      (0123456789bcdfghjkmnpqrstvwxz).
///   </para>
///   <para>
///      The algorithm ignores non-betanumeric characters. If encountered, they
///      are passed over, but they do not result in a validation failure.
///   </para>
///   <para>
///      Assumes that the check digit (if present) is the right-most digit in 
///      the input value.
///   </para>
///   <para>
///      Will detect all single-character transcription errors and all 
///      transpositions of adjacent characters when the value's length is less 
///      than 29 characters. Slightly less capable for values of 29 characters 
///      or greater.
///   </para>
/// </remarks>
public sealed class NcdAlgorithm : ISingleCheckDigitAlgorithm
{
   private static readonly Int32[] _charMap = [.. Chars.Range(Chars.DigitZero, Chars.LowerCaseZ).Select(MapCharacter)];
   private const String _checkCharacters = "0123456789bcdfghjkmnpqrstvwxz";
   private const Int32 _validateMinLength = 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.NcdAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.NcdAlgorithmName;

   /// <summary>
   ///   Map a character to its integer equivalent in the 
   ///   <see cref="NcdAlgorithm"/>. Characters that are not valid for the 
   ///   NcdAlgorithm are mapped to 0.
   /// </summary>
   /// <param name="ch">
   ///   The character to map.
   /// </param>
   /// <returns>
   ///   The integer value associated with <paramref name="ch"/>.
   /// </returns>
   public static Int32 MapCharacter(Char ch) => ch switch
   {
      var d when ch is >= Chars.DigitZero and <= Chars.DigitNine => d.MapAsciiDigit(),
      var b when ch is >= Chars.LowerCaseB and <= Chars.LowerCaseD => b - Chars.LowerCaseB + 10,
      var f when ch is >= Chars.LowerCaseF and <= Chars.LowerCaseH => f - Chars.LowerCaseF + 13,
      Chars.LowerCaseJ => 16,
      Chars.LowerCaseK => 17,
      Chars.LowerCaseM => 18,
      Chars.LowerCaseN => 19,
      var p when ch is >= Chars.LowerCaseP and <= Chars.LowerCaseT => p - Chars.LowerCaseP + 20,
      var v when ch is >= Chars.LowerCaseV and <= Chars.LowerCaseX => v - Chars.LowerCaseV + 25,
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

      var sum = 0;
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var ch = value[index];
         // Benchmarks show that this version of character mapping is more
         // performant TryCalculateCheckDigit than the version used in the
         // Validate method.
         var num = ch switch
         {
            >= Chars.DigitZero and <= Chars.DigitNine => ch.MapAsciiDigit(),
            >= Chars.LowerCaseB and <= Chars.LowerCaseZ => _charMap[ch - Chars.DigitZero],
            _ => 0
         };
         sum += num * (index + 1);
      }

      var checksum = sum % 29;
      checkDigit = _checkCharacters[checksum];

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      var sum = 0;
      for (var index = 0; index < value.Length - 1; index++)
      {
         var ch = value[index];
         var num = ch is >= Chars.DigitZero and <= Chars.LowerCaseZ
            ? _charMap[ch - Chars.DigitZero]
            : 0;
         sum += num * (index + 1);
      }

      var checksum = sum % 29;

      return value[^1] == _checkCharacters[checksum];
   }
}
