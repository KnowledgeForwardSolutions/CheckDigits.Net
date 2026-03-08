namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Modulus 10 algorithm where every digit is weighted by its position in the
///   value, starting with weight 1 for the right-most non-check digit position.
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
///   Maximum length allowed is 9 characters for calculating a new check digit 
///   and 10 characters for validating a value that contains a check digit.
///   </para>
/// </remarks>
public sealed class Modulus10_1Algorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _tryCalculateMaxLength = 9;
   private const Int32 _validateMinLength = 2;
   private const Int32 _validateMaxLength = 10;
   private const Int32 _validateMaskedMaxProcessed = 9;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Modulus10_1AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Modulus10_1AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value) || value.Length > _tryCalculateMaxLength)
      {
         return false;
      }

      var s = 0;
      var t = 0;
      for (var index = 0; index < value.Length; index++)
      {
         var currentDigit = value![index].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }
         t += currentDigit;
         s += t;
      }

      var mod = s % 10;
      checkDigit = mod.ToDigitChar();

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) 
         || value.Length < _validateMinLength 
         || value.Length > _validateMaxLength)
      {
         return false;
      }

      // See https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digit_calculation
      // for use of accumulators s and t instead of multiplying by weights.
      var s = 0;
      var t = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         var currentDigit = value![index].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }
         t += currentDigit;
         s += t;
      }

      var checkDigit = s % 10;

      // No need to check for non-digit check digit character explicitly as it
      // would calculate as < 0 or > 9 and return false in that case.
      return value[^1].ToIntegerDigit() == checkDigit;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      if (mask is null)
      {
         throw new ArgumentNullException(nameof(mask), Resources.NullMaskMessage);
      }
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var s = 0;
      var t = 0;
      var processedDigits = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         var currentDigit = value![index].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }
         t += currentDigit;
         s += t;
         processedDigits++;
      }
      if (processedDigits == 0 || processedDigits > _validateMaskedMaxProcessed)
      {
         return false;
      }

      var checkDigit = s % 10;

      // No need to check for non-digit check digit character explicitly as it
      // would calculate as < 0 or > 9 and return false in that case.
      return value[^1].ToIntegerDigit() == checkDigit;
   }
}