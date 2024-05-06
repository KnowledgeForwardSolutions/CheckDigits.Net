namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 37,36 check digit algorithm. Hybrid system algorithm with
///   modulus 36.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z).
///   </para>
///   <para>
///   Check character calculated by the algorithm is an alphanumeric character (0-9, A-Z).
///   </para>
///   <para>
///   Assumes that the check character (if present) is the right-most character 
///   in the input value.
///   </para>
///   <para>
///   Will detect all single character transcription errors, all or nearly all 
///   two character transposition errors, all or nearly all jump transposition 
///   errors, all or nearly all circular shift errors and a high proportion of 
///   double character transcription errors (two separate single character 
///   transcription errors in the same value).
///   </para>
/// </remarks>
public sealed class Iso7064Mod37_36Algorithm : ISingleCheckDigitAlgorithm
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

      Char ch;
      Int32 num;
      var product = _modulus;
      for (var index = 0; index < value.Length; index++)
      {
         ch = value[index];
         if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else if (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ)
         {
            num = ch - Chars.UpperCaseA + 10;
         }
         else
         {
            return false;
         }
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
      checkDigit = _validCharacters[remainder == _modulus ? 0 : remainder];

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
      for (var index = 0; index < value.Length - 1; index++)
      {
         ch = value[index];
         if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else if (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ)
         {
            num = ch - Chars.UpperCaseA + 10;
         }
         else
         {
            return false;
         }
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
      if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
      {
         num = ch.ToIntegerDigit();
      }
      else if (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ)
      {
         num = ch - Chars.UpperCaseA + 10;
      }
      else
      {
         return false;
      }
      product += num;

      return product % _modulus == 1;
   }
}
