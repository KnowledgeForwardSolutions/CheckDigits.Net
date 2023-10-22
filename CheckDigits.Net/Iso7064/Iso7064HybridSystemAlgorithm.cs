namespace CheckDigits.Net.Iso7064;

/// <summary>
///   Abstract base class for an ISO/IEC 7064 hybrid system algorithm (i.e. an 
///   algorithm that uses two modulus values, M and M+1) and that generates a 
///   single check character.
/// </summary>
public class Iso7064HybridSystemAlgorithm
{
   private readonly Int32 _modulus;
   private readonly Int32 _modulusPlus1;
   private readonly String _validCharacters;

   public Iso7064HybridSystemAlgorithm(
      Int32 modulus,
      String validCharacters)
   {
      if (modulus < 2)
      {
         throw new ArgumentOutOfRangeException(nameof(modulus), modulus, Resources.Iso7064ModulusOutOfRange);
      }
      ArgumentException.ThrowIfNullOrEmpty(validCharacters, nameof(validCharacters));

      _modulus = modulus;
      _modulusPlus1 = _modulus + 1;
      _validCharacters = validCharacters;
   }

   /// <summary>
   ///   Map a character of the value being validated to its integer equivalent.
   /// </summary>
   /// <param name="ch">
   ///   The character to map.
   /// </param>
   /// <returns>
   ///   The integer equivalent of <paramref name="ch"/> or -1 if the character
   ///   is not valid.
   /// </returns>
   public virtual Int32 MapCharacterToNumber(Char ch) => throw new NotImplementedException();

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
         var num = MapCharacterToNumber(value[index]);
         if (num == -1)
         {
            return false;
         }
         product = (product + num) % _modulus;
         if (product == 0)
         {
            product = _modulus;
         }
         product = (product * 2) % _modulusPlus1;
      }

      var x = (_modulus - product + 1) % _modulus;
      if (x < 0 || x > _validCharacters.Length - 1)
      {
         return false;
      }
      checkDigit = _validCharacters[x];

      return true;
   }

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
         num = MapCharacterToNumber(value[index]);
         if (num == -1)
         {
            return false;
         }
         product = (product + num) % _modulus;
         if (product == 0)
         {
            product = _modulus;
         }
         product = (product * 2) % _modulusPlus1;
      }

      num = MapCharacterToNumber(value[^1]);
      if (num == -1)
      {
         return false;
      }

      product += num;

      return product % _modulus == 1;
   }
}
