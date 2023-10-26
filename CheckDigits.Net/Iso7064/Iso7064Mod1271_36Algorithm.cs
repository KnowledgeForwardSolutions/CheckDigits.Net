namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 1271-36 check digit algorithm. Pure system algorithm with
///   modulus 1271 and radix 36.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z).
///   </para>
///   <para>
///   Check characters calculated by the algorithm are alphanumeric characters 
///   (0-9, A-Z).
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
public sealed class Iso7064Mod1271_36Algorithm : IDoubleCheckDigitAlgorithm
{
   private const Int32 _modulus = 1271;
   private const Int32 _radix = 36;
   private const Int32 _reduceThreshold = Int32.MaxValue / _radix;
   private static readonly Int32[] _lookupTable =
      Enumerable.Range(CharConstants.DigitZero, CharConstants.UpperCaseZ - CharConstants.DigitZero + 1)
         .Select(x => x switch
         {
            Int32 d when d >= CharConstants.DigitZero && d <= CharConstants.DigitNine => d - CharConstants.DigitZero,
            Int32 c when c >= CharConstants.UpperCaseA && c <= CharConstants.UpperCaseZ => c - CharConstants.UpperCaseA + 10,
            _ => -1
         }).ToArray();
   private const Int32 _digitLowerBound = 0;
   private const Int32 _digitUpperBound = 9;
   private const Int32 _alphaLowerBound = CharConstants.UpperCaseA - CharConstants.DigitZero;
   private const Int32 _alphaUpperBound = CharConstants.UpperCaseZ - CharConstants.DigitZero;
   private const String _validCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

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
      first = CharConstants.NUL;
      second = CharConstants.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      for (var index = 0; index < value.Length; index++)
      {
         var offset = value[index] - CharConstants.DigitZero;
         if ((offset >= _digitLowerBound && offset <= _digitUpperBound)
            || (offset >= _alphaLowerBound && offset <= _alphaUpperBound))
         {
            sum = (sum + _lookupTable[offset]) * _radix;
         }
         else
         {
            return false;
         }
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
      Int32 offset;
      for (var index = 0; index < value.Length - 1; index++)
      {
         offset = value[index] - CharConstants.DigitZero;
         if ((offset >= _digitLowerBound && offset <= _digitUpperBound)
            || (offset >= _alphaLowerBound && offset <= _alphaUpperBound))
         {
            sum = (sum + _lookupTable[offset]) * _radix;
         }
         else
         {
            return false;
         }
         if (sum >= _reduceThreshold)
         {
            sum %= _modulus;
         }
      }

      // Add value for second check character.
      offset = value[^1] - CharConstants.DigitZero;
      if ((offset >= _digitLowerBound && offset <= _digitUpperBound)
         || (offset >= _alphaLowerBound && offset <= _alphaUpperBound))
      {
         sum += _lookupTable[offset];
      }
      else
      {
         return false;
      }

      return sum % _modulus == 1;
   }
}
