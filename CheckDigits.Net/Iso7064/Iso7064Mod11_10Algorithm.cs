namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 11,10 check digit algorithm. Hybrid system algorithm with
///   modulus 10.
/// </summary>
/// <remarks>
///   <para>
///      Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///      Check digit calculated by the algorithm is a decimal digit (0-9).
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
public sealed class Iso7064Mod11_10Algorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _modulus = 10;
   private const Int32 _modulusPlus1 = 11;
   private const Int32 _validateMinLength = 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod11_10AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod11_10AlgorithmName;

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
         if (!ch.IsAsciiDigit())
         {
            return false;
         }
         var num = ch.MapAsciiDigit();
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

      var x = (_modulus - product + 1) % _modulus;
      checkDigit = x.ToAsciiDigit();

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      var product = _modulus;
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
      if (!ch.IsAsciiDigit())
      {
         return false;
      }
      num = ch.MapAsciiDigit();

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

      var product = _modulus;
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
      if (!ch.IsAsciiDigit())
      {
         return false;
      }
      num = ch.MapAsciiDigit();

      product += num;

      return product % _modulus == 1;
   }
}
