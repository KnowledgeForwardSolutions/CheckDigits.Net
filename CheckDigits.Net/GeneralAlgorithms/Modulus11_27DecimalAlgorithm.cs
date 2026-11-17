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
///   This algorithm implements <see cref="IMaskedCheckDigitAlgorithm"/> 
///   and can validate values that contain non-check digit characters (such as 
///   spaces or dashes for human readability) when used with an 
///   <see cref="ICheckDigitMask"/>. Note that the trailing check digit 
///   character is never masked.
///   </para>
/// </remarks>
public sealed class Modulus11_27DecimalAlgorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
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
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      var weightIndex = new ModulusInt32(_weights.Length);
      for (var charIndex = value.Length - 1; charIndex >= 0; charIndex--)
      {
         var ch = value[charIndex];
         if (!ch.IsAsciiDigit())
         {
            return false;
         }
         var currentDigit = ch.MapAsciiDigit();
         sum += currentDigit * _weights[weightIndex];
         weightIndex++;
      }

      var mod = (11 - (sum % 11)) % 11;
      if (mod == 10)
      {
         return false;
      }

      checkDigit = mod.ToAsciiDigit();

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      Char ch;
      var sum = 0;
      var weightIndex = new ModulusInt32(_weights.Length);
      for (var charIndex = value.Length - 2; charIndex >= 0; charIndex--)
      {
         ch = value[charIndex];
         if (!ch.IsAsciiDigit())
         {
            return false;
         }
         var currentDigit = ch.MapAsciiDigit();
         sum += currentDigit * _weights[weightIndex];
         weightIndex++;
      }

      ch = value[^1];
      if (!ch.IsAsciiDigit())
      {
         return false;
      }
      var checkValue = ch.MapAsciiDigit();
      sum += checkValue;

      return (sum % 11) == 0;
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

      Char ch;
      var sum = 0;
      var weightIndex = new ModulusInt32(_weights.Length);
      var processedDigits = 0;
      for (var charIndex = value.Length - 2; charIndex >= 0; charIndex--)
      {
         if (mask.ExcludeCharacter(charIndex))
         {
            continue;
         }
         ch = value[charIndex];
         if (!ch.IsAsciiDigit())
         {
            return false;
         }
         var currentDigit = ch.MapAsciiDigit();
         sum += currentDigit * _weights[weightIndex];
         weightIndex++;
         processedDigits++;
      }
      if (processedDigits == 0)
      {
         return false;
      }

      ch = value[^1];
      if (!ch.IsAsciiDigit())
      {
         return false;
      }
      var checkValue = ch.MapAsciiDigit();
      sum += checkValue;

      return (sum % 11) == 0;
   }
}
