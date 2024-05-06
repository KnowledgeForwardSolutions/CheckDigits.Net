namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 661-26 check digit algorithm. Pure system algorithm with
///   modulus 661 and radix 26.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphabetic characters (A-Z).
///   </para>
///   <para>
///   Check characters calculated by the algorithm are alphabetic characters 
///   (A-Z).
///   </para>
///   <para>
///   Assumes that the check characters (if present) are the two right-most 
///   characters in the input value.
///   </para>
///   <para>
///   Will detect all single character transcription errors, all or nearly all 
///   two character transposition errors, all or nearly all jump transposition 
///   errors, all or nearly all circular shift errors and a high proportion of 
///   double character transcription errors (two separate single character 
///   transcription errors in the same value).
///   </para>
/// </remarks>
public sealed class Iso7064Mod661_26Algorithm : IDoubleCheckDigitAlgorithm
{
   private const Int32 _modulus = 661;
   private const Int32 _radix = 26;
   private const Int32 _reduceThreshold = Int32.MaxValue / _radix;
   private const String _validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod661_26AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod661_26AlgorithmName;

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
         var num = value[index] - Chars.UpperCaseA;
         if (num < 0 || num > 25)
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
      sum = (sum * _radix) % _modulus;

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
      if (String.IsNullOrEmpty(value) || value.Length < 3)
      {
         return false;
      }

      // Sum non-check digit characters and first check character.
      var sum = 0;
      Int32 num;
      for (var index = 0; index < value.Length - 1; index++)
      {
         num = value[index] - Chars.UpperCaseA;
         if (num < 0 || num > 25)
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
      num = value[^1] - Chars.UpperCaseA;
      if (num < 0 || num > 25)
      {
         return false;
      }
      sum += num;

      return sum % _modulus == 1;
   }
}
