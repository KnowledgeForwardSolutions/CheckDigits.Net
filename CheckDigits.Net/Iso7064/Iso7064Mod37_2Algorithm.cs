namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 37-2 check digit algorithm. Pure system algorithm with
///   modulus 37 and radix 2.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z).
///   </para>
///   <para>
///   Check character calculated by the algorithm is an alphanumeric character 
///   (0-9, A-Z) or an asterisk character '*'.
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
public sealed class Iso7064Mod37_2Algorithm : ISingleCheckDigitAlgorithm
{
   private const Int32 _modulus = 37;
   private const Int32 _radix = 2;
   private const Int32 _reduceThreshold = Int32.MaxValue / _radix;
   private const String _checkCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*";

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod37_2AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod37_2AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      Char ch;
      Int32 num;
      var sum = 0;
      for (var index = 0; index < value.Length; index++)
      {
         ch = value[index];
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else if (ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ)
         {
            num = ch - CharConstants.UpperCaseA + 10;
         }
         else
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
      checkDigit = _checkCharacters[x];

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < 2)
      {
         return false;
      }

      Char ch;
      Int32 num;
      var sum = 0;
      for (var index = 0; index < value.Length - 1; index++)
      {
         ch = value[index];
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else if (ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ)
         {
            num = ch - CharConstants.UpperCaseA + 10;
         }
         else
         {
            return false;
         }
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      ch = value[^1];
      if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
      {
         num = ch.ToIntegerDigit();
      }
      else if (ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ)
      {
         num = ch - CharConstants.UpperCaseA + 10;
      }
      else if (ch == CharConstants.Asterisk)
      {
         num = 36;
      }
      else
      {
         return false;
      }
      sum += num;

      return sum % _modulus == 1;
   }
}
