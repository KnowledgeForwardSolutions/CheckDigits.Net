namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 37-2 check digit algorithm. Pure system algorithm with
///   modulus 37 and radix 2.
/// </summary>
/// <remarks>
///   <para>
///      Valid characters are alphanumeric characters (0-9, A-Z).
///   </para>
///   <para>
///      Check character calculated by the algorithm is an alphanumeric 
///      character (0-9, A-Z) or an asterisk character '*'.
///   </para>
///   <para>
///      Assumes that the check character (if present) is the right-most 
///      character in the input value.
///   </para>
///   <para>
///      Will detect all single character transcription errors, nearly all two 
///      character transposition errors, jump transposition errors and circular 
///      shift errors and a high proportion of double character transcription 
///      errors (two separate single character transcription errors in the same 
///      value).
///   </para>
/// </remarks>
public sealed class Iso7064Mod37_2Algorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _modulus = 37;
   private const Int32 _radix = 2;
   private const String _checkCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*";
   private const Int32 _validateMinLength = 2;

   // Reduce threshold is calculated to ensure that (sum + charValue) * radix will
   // not overflow a 32-bit integer.
   private const Int32 _characterMaxValue = 35;
   private const Int32 _reduceThreshold = (Int32.MaxValue / _radix) - (_characterMaxValue + 1);

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod37_2AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod37_2AlgorithmName;

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
         if (!ch.IsAsciiUppercaseAlphanumeric())
         {
            return false;
         }
         var num = ch.MapAsciiUppercaseAlphanumeric();
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      var remainder = sum % _modulus;
      var x = (_modulus - remainder + 1) % _modulus;
      checkDigit = _checkCharacters[x];

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
      Int32 num;
      var sum = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         ch = value[index];
         if (!ch.IsAsciiUppercaseAlphanumeric())
         {
            return false;
         }
         num = ch.MapAsciiUppercaseAlphanumeric();
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      num = MapCheckDigitCharacter(value[^1]);
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

      Char ch;
      Int32 num;
      var sum = 0;
      var processedDigits = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         ch = value[index];
         if (!ch.IsAsciiUppercaseAlphanumeric())
         {
            return false;
         }
         num = ch.MapAsciiUppercaseAlphanumeric();
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

      num = MapCheckDigitCharacter(value[^1]);
      if (num == -1)
      {
         return false;
      }
      sum += num;

      return sum % _modulus == 1;
   }

   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   private static Int32 MapCheckDigitCharacter(Char ch)
      => ch switch
      {
         _ when ch.IsAsciiDigit() => ch.MapAsciiDigit(),
         _ when ch.IsAsciiLetterUpper() => 10 + (ch - Chars.UpperCaseA),
         Chars.Asterisk => 36,
         _ => -1                 // This will require validation after mapping
      };
}
