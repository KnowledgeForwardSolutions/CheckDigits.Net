namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 1271-36 check digit algorithm. Pure system algorithm with
///   modulus 1271 and radix 36.
/// </summary>
/// <remarks>
///   <para>
///      Valid characters are alphanumeric characters (0-9, A-Z).
///   </para>
///   <para>
///      Check characters calculated by the algorithm are alphanumeric 
///      characters (0-9, A-Z).
///   </para>
///   <para>
///      Assumes that the check characters (if present) are the two right-most 
///      characters in the input value.
///   </para>
///   <para>
///      Will detect all single character transcription errors, nearly 100% of
///      character transposition errors, jump transposition errors, and circular
///      shift errors and a high proportion of double character transcription 
///      errors (two separate single character transcription errors in the same
///      value). This algorithm offers the highest protection available for
///      alphanumeric values.
///   </para>
///   <para>
///      This algorithm implements <see cref="IMaskedCheckDigitAlgorithm"/> 
///      and can validate values that contain non-check digit characters (such 
///      as spaces or dashes included for human readability) when used with an 
///      <see cref="ICheckDigitMask"/>. Note that the trailing two check digit 
///      characters are never masked.
///   </para>
/// </remarks>
public sealed class Iso7064Mod1271_36Algorithm : IDoubleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _modulus = 1271;
   private const Int32 _radix = 36;
   private const String _validCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
   private const Int32 _validateMinLength = 3;

   // Reduce threshold is calculated to ensure that (sum + charValue) * radix will
   // not overflow a 32-bit integer.
   private const Int32 _characterMaxValue = 35;
   private const Int32 _reduceThreshold = (Int32.MaxValue / _radix) - (_characterMaxValue + 1);

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod1271_36AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod1271_36AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigits(
      String value,
      out Char first,
      out Char second)
   {
      first = Chars.NUL;
      second = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      for (var index = 0; index < value.Length; index++)
      {
         var num = value[index].MapAsciiUppercaseAlphanumeric();
         if (num == -1)
         {
            return false;
         }
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      // Per ISO/IEC 7064, two character algorithms perform one final pass with
      // effective character value of zero.
      sum = sum * _radix % _modulus;

      var checkSum = _modulus - sum + 1;
      var quotient = checkSum / _radix;
      var remainder = checkSum % _radix;

      first = _validCharacters[quotient];
      second = _validCharacters[remainder];

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      // Sum non-check digit characters and first check character.
      Char ch;
      Int32 num;
      var sum = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         num = value[index].MapAsciiUppercaseAlphanumeric();
         if (num == -1)
         {
            return false;
         }
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      // Add value for second check character.
      num = value[^1].MapAsciiUppercaseAlphanumeric();
      if (num == -1)
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


      // Sum non-check digit characters.
      Char ch;
      Int32 num;
      var sum = 0;
      var processedDigits = 0;
      var processLength = value.Length - 2;
      for (var index = 0; index < processLength; index++)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         num = value[index].MapAsciiUppercaseAlphanumeric();
         if (num == -1)
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

      // Process check characters outside of loop without considering mask.
      num = value[^2].MapAsciiUppercaseAlphanumeric();
      if (num == -1)
      {
         return false;
      }
      sum = (sum + num) * _radix;
      if (sum >= _reduceThreshold)
      {
         sum %= _modulus;
      }

      num = value[^1].MapAsciiUppercaseAlphanumeric();
      if (num == -1)
      {
         return false;
      }
      sum += num;

      return sum % _modulus == 1;
   }
}
