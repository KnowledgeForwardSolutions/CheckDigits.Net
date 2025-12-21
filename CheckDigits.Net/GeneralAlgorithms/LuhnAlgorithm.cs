// Ignore Spelling: Luhn

namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Modulus 10 algorithm created by Hans Peter Luhn. Uses a weight of 2 which
///   is applied to every odd position digit.
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
///   Will detect all single-digit transcription errors and most two digit 
///   transpositions of adjacent digits (except 09 <-> 90). Will detect most
///   twin errors (i.e. 11 <-> 44) except 22 <-> 55,  33 <-> 66 and 44 <-> 77.
///   </para>
/// </remarks>
public sealed class LuhnAlgorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _validateMinLength = 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.LuhnAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.LuhnAlgorithmName;

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
         sum += oddPosition
            ? digit > 4 ? (digit * 2) - 9 : digit * 2
            : digit;
         oddPosition = !oddPosition;
      }
      var mod = 10 - (sum % 10);
      checkDigit = mod == 10 ? Chars.DigitZero : mod.ToDigitChar();

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
         if (digit < 0 || digit > 9)
         {
            return false;
         }
         sum += oddPosition 
            ? digit > 4 ? (digit * 2) - 9 : digit * 2
            : digit;
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
         if (digit < 0 || digit > 9)
         {
            return false;
         }
         sum += oddPosition
            ? digit > 4 ? (digit * 2) - 9 : digit * 2
            : digit;
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
