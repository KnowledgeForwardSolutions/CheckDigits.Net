// Ignore Spelling: Cusip

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used by North American security identification number (CUSIP
///   number). Has similarities with ISIN and Luhn algorithms.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z) and *, @, #
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
/// </remarks>
public class CusipAlgorithm : ICheckDigitAlgorithm
{
   private const Int32 _validateMinLength = 9;
   private static readonly Int32[] _evenValues = Chars.Range(Chars.HashMark, Chars.UpperCaseZ)
      .Select(x => MapCharacter(x))
      .Select(x => (x * 2 / 10) + (x * 2 % 10))
      .ToArray();
   private static readonly Int32[] _oddValues = Chars.Range(Chars.HashMark, Chars.UpperCaseZ)
      .Select(x => MapCharacter(x))
      .Select(x => (x / 10) + (x % 10))
      .ToArray();

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.CusipAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.CusipAlgorithmName;

   /// <summary>
   ///   Map a character to its integer equivalent in the 
   ///   <see cref="CusipAlgorithm"/>. Characters that are not valid for the 
   ///   CusipAlgorithm are mapped to -1.
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
      Chars.Asterisk => 36,
      Chars.AtSign => 37,
      Chars.HashMark => 38,
      _ => -1
   };

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _validateMinLength)
      {
         return false;
      }

      var sum = 0;
      var oddPosition = false;
      Int32 num;
      for (var index = value.Length - 2; index >= 0; index--)
      {
         var ch = value[index];
         if (ch >= Chars.HashMark && ch <= Chars.UpperCaseZ)
         {
            var offset = ch - Chars.HashMark;
            num = oddPosition ? _oddValues[offset] : _evenValues[offset];
         }
         else
         {
            return false;
         }

         if (num > -1)
         {
            sum += num;
         }
         else
         {
            return false;
         }

         oddPosition = !oddPosition;
      }
      var checkDigit = (10 - (sum % 10)) % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }
}
