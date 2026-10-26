namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 37,36 check digit algorithm. Hybrid system algorithm with
///   modulus 36.
/// </summary>
/// <remarks>
///   <para>
///      Valid characters are alphanumeric characters (0-9, A-Z).
///   </para>
///   <para>
///      Check character calculated by the algorithm is an alphanumeric 
///      character (0-9, A-Z).
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
public sealed class Iso7064Mod37_36Algorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _modulus = 36;
   private const Int32 _modulusPlus1 = 37;
   private const String _validCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
   private const Int32 _validateMinLength = 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod37_36AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod37_36AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var product = _modulus;
      for (var index = 0; index < value.Length; index++)
      {
         var ch = value[index];
         if (!ch.IsAsciiUppercaseAlphanumeric())
         {
            return false;
         }
         var num = ch.MapAsciiUppercaseAlphanumeric();
         product += num;
         if (product > _modulus)
         {
            product -= _modulus;
         }
         product *= 2;
         if (product >= _modulusPlus1)
         {
            product -= _modulusPlus1;
         }
      }

      var remainder = (_modulus - product + 1) % _modulus;
      checkDigit = _validCharacters[remainder];

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
      var product = _modulus;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         ch = value[index];
         if (!ch.IsAsciiUppercaseAlphanumeric())
         {
            return false;
         }
         num = ch.MapAsciiUppercaseAlphanumeric();
         product += num;
         if (product > _modulus)
         {
            product -= _modulus;
         }
         product *= 2;
         if (product >= _modulusPlus1)
         {
            product -= _modulusPlus1;
         }
      }

      ch = value[^1];
      if (!ch.IsAsciiUppercaseAlphanumeric())
      {
         return false;
      }
      num = ch.MapAsciiUppercaseAlphanumeric();
      product += num;

      return product % _modulus == 1;
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
      var product = _modulus;
      var processLength = value.Length - 1;
      var processedDigits = 0;
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
         product += num;
         if (product > _modulus)
         {
            product -= _modulus;
         }
         product *= 2;
         if (product >= _modulusPlus1)
         {
            product -= _modulusPlus1;
         }
         processedDigits++;
      }
      if (processedDigits == 0)
      {
         return false;
      }

      ch = value[^1];
      if (!ch.IsAsciiUppercaseAlphanumeric())
      {
         return false;
      }
      num = ch.MapAsciiUppercaseAlphanumeric();
      product += num;

      return product % _modulus == 1;
   }
}
