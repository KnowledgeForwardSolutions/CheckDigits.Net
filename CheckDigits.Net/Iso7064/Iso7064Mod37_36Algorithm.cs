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
public class Iso7064Mod37_36Algorithm : ISingleCheckDigitAlgorithm
{
   private readonly Int32 _modulus = 36;
   private readonly Int32 _modulusPlus1 = 37;
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
   public String AlgorithmDescription => Resources.Iso7064Mod37_36AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod37_36AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var product = _modulus;
      Int32 offset;
      for (var index = 0; index < value.Length; index++)
      {
         offset = value[index] - CharConstants.DigitZero;
         if ((offset >= _digitLowerBound && offset <= _digitUpperBound)
            || (offset >= _alphaLowerBound && offset <= _alphaUpperBound))
         {
            product += _lookupTable[offset];
         }
         else
         {
            return false;
         }
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
      if (String.IsNullOrEmpty(value) || value.Length < 2)
      {
         return false;
      }

      var product = _modulus;
      Int32 offset;
      for (var index = 0; index < value.Length - 1; index++)
      {
         offset = value[index] - CharConstants.DigitZero;
         if ((offset >= _digitLowerBound && offset <= _digitUpperBound)
            || (offset >= _alphaLowerBound && offset <= _alphaUpperBound))
         {
            product += _lookupTable[offset];
         }
         else
         {
            return false;
         }
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

      offset = value[^1] - CharConstants.DigitZero;
      if ((offset >= _digitLowerBound && offset <= _digitUpperBound)
         || (offset >= _alphaLowerBound && offset <= _alphaUpperBound))
      {
         product += _lookupTable[offset];
      }
      else
      {
         return false;
      }

      return product % _modulus == 1;
   }
}
