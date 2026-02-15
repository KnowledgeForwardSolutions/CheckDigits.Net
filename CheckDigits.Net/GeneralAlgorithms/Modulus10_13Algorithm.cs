namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Widely used modulus 10 algorithm that uses weights 1 and 3 (odd positions
///   have weight 3 and even positions have weight 1).
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and ~89% of two digit 
///   transpositions of adjacent digits except cases where the difference
///   between two transposed digits is 5 (i.e. 1 -> 6, 2 -> 6, etc.). Will not
///   detect two digit jump transpositions (i.e. 123 -> 424).
///   </para>
/// </remarks>
public sealed class Modulus10_13Algorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _validateMinLength = 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Modulus10_13AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Modulus10_13AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      var oddPosition = true;
      for (var index = value.Length - 1; index >= 0; index--)
      {
         var digit = value[index].ToIntegerDigit();
         if (digit < 0 || digit > 9)
         {
               return false;
         }
         sum += oddPosition ? digit * 3 : digit;
         oddPosition = !oddPosition;
      }
      checkDigit = ((10 - (sum % 10)) % 10).ToDigitChar();

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
      var oddPosition = true;
      for (var index = value.Length - 2; index >= 0; index--)
      {
         var digit = value[index].ToIntegerDigit();
         if (digit is < 0 or > 9)
         {
            return false;
         }
         sum += oddPosition ? digit * 3 : digit;
         oddPosition = !oddPosition;
      }
      var checkDigit = (10 - (sum % 10)) % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      var oddPosition = true;
      var processedDigits = 0;
      for (var index = value.Length - 2; index >= 0; index--)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }

         var digit = value[index].ToIntegerDigit();
         if (digit is < 0 or > 9)
         {
            return false;
         }
         sum += oddPosition ? digit * 3 : digit;
         oddPosition = !oddPosition;
         processedDigits++;
      }
      if (processedDigits == 0)
      {
         return false;
      }
      var checkDigit = (10 - (sum % 10)) % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }
}
