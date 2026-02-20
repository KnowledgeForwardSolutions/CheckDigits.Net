namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Modulus 11 (Extended) algorithm which uses the IBM Modulus 11 weighting 
///   scheme of 2, 3, 4, 5, 6, 7 starting from the right-most non-check digit.
///   For strings longer than 6 characters, the weighting scheme repeats 
///   starting again with 2 for the 7th character from the right, etc. The check 
///   digit is calculated as the modulus 11 of the sum of weighted values.
/// </summary>
/// <remarks>
///   <para>
///   Calculating modulus 11 of the sum of weighted values results in a check
///   value from 0 to 10 (11 distinct values). Since it is not possible to 
///   represent 10 with a single decimal digit this algorithm extends the
///   possible check digit characters to include '0'-'9' and 'X' (represents a
///   check value of 10).
///   </para>
///   <para>
///   See <see cref="Modulus11_27DecimalAlgorithm"/> for a related modulus 11
///   algorithm that restricts check characters to '0'-'9', but at the cost of 
///   rejecting one out of every 11 values, or approximately 9.09%.
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9) or an 
///   uppercase 'X'.
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and all two digit 
///   transposition errors.
///   </para>
/// </remarks>
public sealed class Modulus11_27ExtendedAlgorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _validateMinLength = 2;
   private static readonly Int32[] _weights = [2, 3, 4, 5, 6, 7];

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Modulus11_27ExtendedAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Modulus11_27ExtendedAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      var weightIndex = new ModulusInt32(_weights.Length);
      for (var charIndex = value.Length - 1; charIndex >= 0; charIndex--)
      {
         var currentDigit = value[charIndex].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }

         sum += currentDigit * _weights[weightIndex];
         weightIndex++;
      }

      var mod = (11 - (sum % 11)) % 11;
      checkDigit = mod < 10 ? mod.ToDigitChar() : Chars.UpperCaseX;

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

      var checkDigit = value[^1].ToExtendedDecimalCheckDigit();
      if (checkDigit.IsInvalidExtendedDecimalCheckDigit())
      {
         return false;
      }
      sum += checkDigit;

      return (sum % 11) == 0;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      var weightIndex = new ModulusInt32(_weights.Length);
      var processedDigits = 0;
      for (var charIndex = value.Length - 2; charIndex >= 0; charIndex--)
      {
         if (mask.ExcludeCharacter(charIndex))
         {
            continue;
         }
         var currentDigit = value[charIndex].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }

         sum += currentDigit * _weights[weightIndex];
         weightIndex++;
         processedDigits++;
      }
      if (processedDigits == 0)
      {
         return false;
      }

      var checkDigit = value[^1].ToExtendedDecimalCheckDigit();
      if (checkDigit.IsInvalidExtendedDecimalCheckDigit())
      {
         return false;
      }
      sum += checkDigit;

      return (sum % 11) == 0;
   }
}
