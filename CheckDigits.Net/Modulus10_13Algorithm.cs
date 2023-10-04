namespace CheckDigits.Net;

/// <summary>
///   Widely used modulus 10 algorithm that uses weights 1 and 3 (odd positions
///   have weight 3 and even positions have weight 1).
/// </summary>
/// <remarks>
///   Valid characters are decimal digits (0-9).
///   </para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   Will detect all single-digit transcription errors and ~89% of two digit 
///   transpositions of adjacent digits except cases where the difference
///   between two transposed digits is 5 (i.e. 1 -> 6, 2 -> 6, etc.). Will not
///   detect two digit jump transpositions (i.e. 123 -> 424).
/// </remarks>
public class Modulus10_13Algorithm : ISingleCheckDigitAlgorithm
{
   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Modulus10_13AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Modulus10_13AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
      => CalculateCheckDigit(value, false, out checkDigit);

   /// <inheritdoc/>
   public Boolean Validate(String value)
      => CalculateCheckDigit(value, true, out var checkDigit)
         && value[^1] == checkDigit;

   private static Boolean CalculateCheckDigit(
      String value,
      Boolean containsCheckDigit,
      out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value) || (containsCheckDigit && value.Length < 2))
      {
         return false;
      }

      var sum = 0;
      var oddPosition = true;
      var startPosition = value.Length - (containsCheckDigit ? 2 : 1);
      for (var index = startPosition; index >= 0; index--)
      {
         var digit = value[index].ToIntegerDigit();
         if (digit < 0 || digit > 9)
         {
            return false;
         }
         sum += oddPosition ? digit * 3 : digit;
         oddPosition = !oddPosition;
      }
      var mod = 10 - (sum % 10);
      checkDigit = mod == 10 ? CharConstants.DigitZero : mod.ToDigitChar();

      return true;
   }
}
