namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Modulus 11 (Decimal) algorithm which uses the IBM Modulus 11 weighting 
///   scheme of 2, 3, 4, 5, 6, 7 starting from the right-most non-check digit.
///   For strings longer than 6 characters, the weighting scheme repeats 
///   starting again with 2 for the 7th character from the right, etc. The check 
///   digit is calculated as the modulus 11 of the sum of weighted values.
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
///   See <see cref="Modulus11_27ExtendedAlgorithm"/> for a related modulus 11
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
public class Modulus11_27DecimalAlgorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _validateMinLength = 2;
   private static readonly Int32[] _weights = [2, 3, 4, 5, 6, 7];

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Modulus11_27DecimalAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Modulus11_27DecimalAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      throw new NotImplementedException();
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      var sum = 0;
      var weightIndex = new ModulusInt32(_weights.Length);
      for (var charIndex = value.Length - 2; charIndex >= 0; charIndex--)
      {
         var currentDigit = value[charIndex].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }

         sum += currentDigit * _weights[weightIndex];
         weightIndex++;
      }

      var checkValue = value[^1].ToIntegerDigit();
      if (checkValue.IsInvalidDigit())
      {
         return false;
      }
      sum += checkValue;

      return (sum % 11) == 0;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      throw new NotImplementedException();
   }
}
