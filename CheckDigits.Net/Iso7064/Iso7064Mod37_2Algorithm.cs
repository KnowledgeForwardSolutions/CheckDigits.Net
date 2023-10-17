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
public class Iso7064Mod37_2Algorithm : ISingleCheckDigitAlgorithm
{
   private const Int32 _modulus = 37;
   private const Int32 _radix = 2;
   private const Int32 _reduceThreshold = Int32.MaxValue / _radix;
   private static readonly Int32[] _lookupTable =
      Enumerable.Range(CharConstants.Asterisk, CharConstants.UpperCaseZ - CharConstants.Asterisk + 1)
         .Select(x => x switch
         {
            CharConstants.Asterisk => 36,
            Int32 d when d >= CharConstants.DigitZero && d <= CharConstants.DigitNine => d - CharConstants.DigitZero,
            Int32 c when c >= CharConstants.UpperCaseA && c <= CharConstants.UpperCaseZ => c - CharConstants.UpperCaseA + 10,
            _ => -1
         }).ToArray();
   private const String _checkCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*";

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod37_2AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod37_2AlgorithmName;

   /// <inheritdoc/>
   public virtual Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      Int32 num;
      for (var index = 0; index < value.Length; index++)
      {
         var offset = value[index] - CharConstants.Asterisk;
         if (offset < 1 || offset > 48)
         {
            return false;
         }
         num = _lookupTable[offset];
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

      var sum = 0;
      Int32 num;
      Int32 offset;
      for (var index = 0; index < value.Length - 1; index++)
      {
         offset = value[index] - CharConstants.Asterisk;
         if (offset < 1 || offset > 48)
         {
            return false;
         }
         num = _lookupTable[offset];
         sum = (sum + num) * _radix;
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      offset = value[^1] - CharConstants.Asterisk;
      if (offset < 0 || offset > 48)
      {
         return false;
      }
      num = _lookupTable[offset];

      sum += num;

      return sum % _modulus == 1;
   }
}
