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
///      Will detect all single character transcription errors, nearly 100% of
///      character transposition errors, jump transposition errors, and circular
///      shift errors and a high proportion of double character transcription 
///      errors (two separate single character transcription errors in the same
///      value).
///   </para>
///   <para>
///      This algorithm implements <see cref="IMaskedCheckDigitAlgorithm"/> 
///      and can validate values that contain non-check digit characters (such 
///      as spaces or dashes included for human readability) when used with an 
///      <see cref="ICheckDigitMask"/>. Note that the trailing check digit 
///      character is never masked.
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
         var ch = value[index];
         if (!ch.IsAsciiDigit())
         {
            return false;
         }
         var num = ch.MapAsciiDigit();
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      var remainder = sum % _modulus;
      var x = (_modulus - remainder + 1) % _modulus;
      checkDigit = x.ToAsciiExtendedDecimalDigit();         // Allow for x = 10 which would use 'X' for the check digit

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
      Char ch;
      Int32 num;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         ch = value[index];
         if (!ch.IsAsciiDigit())
         {
            return false;
         }
         num = ch.MapAsciiDigit();
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      ch = value[^1];
      if (!ch.IsAsciiExtendedDecimalDigit())       // Allow for 'X' check digit (value of 10)
      {
         return false;
      }
      num = ch.MapAsciiExtendedDecimalDigit();

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
      Char ch;
      Int32 num;
      var processedDigits = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         ch = value[index];
         if (!ch.IsAsciiDigit())
         {
            return false;
         }
         num = ch.MapAsciiDigit();
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

      ch = value[^1];
      if (!ch.IsAsciiExtendedDecimalDigit())       // Allow for 'X' check digit (value of 10)
      {
         return false;
      }
      num = ch.MapAsciiExtendedDecimalDigit();

      sum += num;

      return sum % _modulus == 1;
   }
}
