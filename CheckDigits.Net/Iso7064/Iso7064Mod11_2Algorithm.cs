namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 11-2 check digit algorithm. Pure system algorithm with
///   modulus 11 and radix 2.
/// </summary>
/// <remarks>
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
///   Will detect all single digit transcription errors, all or nearly all two 
///   digit transposition errors, all or nearly all jump transposition errors,
///   all or nearly all circular shift errors and a high proportion of double 
///   digit transcription errors (two separate single digit transcription errors
///   in the same value).
///   </para>
/// </remarks>
public sealed class Iso7064Mod11_2Algorithm : ISingleCheckDigitAlgorithm
{
   private const Int32 _modulus = 11;
   private const Int32 _radix = 2;
   private const Int32 _reduceThreshold = Int32.MaxValue / _radix;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod11_2AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod11_2AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      Int32 num;
      for (var index = 0; index < value.Length; index++)
      {
         num = value[index].ToIntegerDigit();
         if (num < 0 || num > 9)
         {
            return false;
         }
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      var remainder = sum % _modulus;
      var x = (_modulus - remainder + 1) % _modulus;
      checkDigit = x == 10 ? Chars.UpperCaseX : x.ToDigitChar();

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < 2)
      {
         return false;
      }

      var sum = 0;
      Int32 num;
      for (var index = 0; index < value.Length - 1; index++)
      {
         num = value[index].ToIntegerDigit();
         if (num < 0 || num > 9)
         {
            return false;
         }
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      num = value[^1].ToIntegerDigit();
      if (num == 40)
      {
         num = 10;
      }
      else if (num < 0 || num > 9)
      {
         return false;
      }

      sum += num;

      return sum % _modulus == 1;
   }
}
