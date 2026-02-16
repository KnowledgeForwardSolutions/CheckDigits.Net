namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Modulus 11 (Decimal) algorithm where every digit is weighted by its 
///   position in the value, starting from the right-most position, and the 
///   check digit being the modulus 11 of the sum of weighted values.
/// </summary>
/// <remarks>
///   <para>
///   Calculating modulus 11 of the sum of weighted values results in a check
///   value from 0 to 10 (11 distinct values). Since it is not possible to 
///   represent 10 with a single decimal digit, any value with a check value of
///   10 is rejected by this algorithm. Both the Validate and 
///   TryCalculateCheckDigit methods will return false if the value results in
///   a check value of 10. This eliminates approximately 9.09% of possible 
///   numbers from consideration.
///   </para>
///   <para>
///   See <see cref="Modulus11ExtendedAlgorithm"/> for a related modulus 11
///   algorithm that allows check values of 10, but at the cost of using a 
///   non-decimal digit character for the check digit.
///   </para>
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
///   Will detect all single-digit transcription errors and all two digit 
///   transposition errors.
///   </para>
///   <para>
///   Maximum length allowed is 9 characters for calculating a new check digit 
///   and 10 characters for validating a value that contains a check digit.
///   </para>
/// </remarks>
public class Modulus11DecimalAlgorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _tryCalculateMaxLength = 9;
   private const Int32 _validateMinLength = 2;
   private const Int32 _validateMaxLength = 10;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Modulus11DecimalAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Modulus11DecimalAlgorithmName;

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
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var currentDigit = value[index].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }
         t += currentDigit;
         s += t;
      }
      s += t;

      var mod = (11 - (s % 11)) % 11;
      if (mod == 10)
      {
         return false;
      }

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
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var currentDigit = value[index].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }
         t += currentDigit;
         s += t;
      }

      return (s % 11) == 0;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      // Process all characters except the check digit character inside loop
      // that handles masked characters.
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
         var currentDigit = value[index].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }
         t += currentDigit;
         s += t;
         processedDigits++;
      }
      if (processedDigits == 0 || processedDigits >= _validateMaxLength)
      {
         return false;
      }

      // Perform final weight accumulation that would not be necessary if entire
      // value was processed inside the loop.
      s += t;

      // Check digit is never masked so handle separately.
      var checkDigit = value![^1].ToIntegerDigit();
      if (checkDigit.IsInvalidDigit())
      {
         return false;
      }
      s += checkDigit;

      return (s % 11) == 0;
   }
}
