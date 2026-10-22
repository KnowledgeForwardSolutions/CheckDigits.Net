namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 11-2 check digit algorithm. Pure system algorithm with
///   modulus 11 and radix 2.
/// </summary>
/// <remarks>
///   <para>
///      Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///      Check digit calculated by the algorithm is a decimal digit (0-9) or an 
///      uppercase 'X'.
///   </para>
///   <para>
///      Assumes that the check digit (if present) is the right-most digit in
///      the input value.
///   </para>
///   <para>
///      Will detect all single digit transcription errors, all or nearly all 
///      two  digit transposition errors, all or nearly all jump transposition 
///      errors, all or nearly all circular shift errors and a high proportion 
///      of double transcription errors (two separate single digit transcription 
///      errors in the same value).
///   </para>
/// </remarks>
public sealed class Iso7064Mod11_2Algorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _modulus = 11;
   private const Int32 _radix = 2;
   private const Int32 _validateMinLength = 2;

   // Reduce threshold is calculated to ensure that (sum + digit) * radix will
   // not overflow a 32-bit integer.
   private const Int32 _digitMaxValue = 9;
   private const Int32 _reduceThreshold = (Int32.MaxValue / _radix) - (_digitMaxValue + 1);

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
      for (var index = 0; index < value.Length; index++)
      {
         var num = value[index].ToDecimalDigit();
         if (!num.IsValidDigit())
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
      checkDigit = x == 10 ? Chars.UpperCaseX : x.ToAsciiDigit();

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
      Int32 num;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         num = value[index].ToDecimalDigit();
         if (!num.IsValidDigit())
         {
            return false;
         }
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      num = value[^1].ToExtendedDecimalDigit();       // Allow for 'X' check digit (value of 10)
      if (!num.IsValidExtendedDecimalCheckDigit())
      {
         return false;
      }

      sum += num;

      return sum % _modulus == 1;
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

      var sum = 0;
      Int32 num;
      var processedDigits = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         num = value[index].ToDecimalDigit();
         if (!num.IsValidDigit())
         {
            return false;
         }
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
         processedDigits++;
      }
      if (processedDigits == 0)
      {
         return false;
      }

      num = value[^1].ToExtendedDecimalDigit();       // Allow for 'X' check digit (value of 10)
      if (!num.IsValidExtendedDecimalCheckDigit())
      {
         return false;
      }

      sum += num;

      return sum % _modulus == 1;
   }
}
