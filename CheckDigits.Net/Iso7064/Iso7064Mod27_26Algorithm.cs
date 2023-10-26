namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 27,26 check digit algorithm. Hybrid system algorithm with
///   modulus 26.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphabetic characters (A-Z).
///   </para>
///   <para>
///   Check character calculated by the algorithm is an alphabetic character (A-Z).
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
public sealed class Iso7064Mod27_26Algorithm : ISingleCheckDigitAlgorithm
{
   private readonly Int32 _modulus = 26;
   private readonly Int32 _modulusPlus1 = 27;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod27_26AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod27_26AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var product = _modulus;
      for (var index = 0; index < value.Length; index++)
      {
         var num = value[index] - CharConstants.UpperCaseA;
         if (num < 0 || num > 25)
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

      var x = (_modulus - product + 1) % _modulus;
      checkDigit = x == 26 ? CharConstants.UpperCaseA : (Char)(CharConstants.UpperCaseA + x);

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
      Int32 num;
      for (var index = 0; index < value.Length - 1; index++)
      {
         num = value[index] - CharConstants.UpperCaseA;
         if (num < 0 || num > 25)
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

      num = value[^1] - CharConstants.UpperCaseA;
      if (num < 0 || num > 25)
      {
         return false;
      }

      product += num;

      return product % _modulus == 1;
   }
}
