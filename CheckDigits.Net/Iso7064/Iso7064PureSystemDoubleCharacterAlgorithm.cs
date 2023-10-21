namespace CheckDigits.Net.Iso7064;

/// <summary>
///   Abstract base class for an ISO/IEC 7064 pure system algorithm (i.e. an 
///   algorithm that uses a single modulus) and that generates two check
///   characters.
/// </summary>
public abstract class Iso7064PureSystemDoubleCharacterAlgorithm
{
   private readonly Int32 _modulus;
   private readonly Int32 _radix;
   private readonly Int32 _reduceThreshold;
   private readonly String _validCharacters;

   /// <summary>
   ///   Initialize a new <see cref="Iso7064PureSystemSingleCharacterAlgorithm"/>.
   /// </summary>
   /// <param name="modulus">
   ///   The value used by the algorithm modulus operation.
   /// </param>
   /// <param name="radix">
   ///   The base of the geometric progression used by the algorithm.
   /// </param>
   /// <param name="validCharacters">
   ///   <para>
   ///   String containing the characters that are valid for the algorithm, 
   ///   including the check character if algorithm uses a supplemental check
   ///   character.
   ///   </para>
   ///   <para>
   ///   The characters in the string should be in order of their integer 
   ///   equivalent, from lowest to highest.
   ///   </para>
   /// </param>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   <paramref name="modulus"/> is less than 2.
   ///   - or -
   ///   <paramref name="radix"/> is less than 2.
   /// </exception>
   /// <exception cref="ArgumentNullException">
   ///   <paramref name="validCharacters"/> is <see langword="null"/>.
   /// </exception>
   /// <exception cref="ArgumentException">
   ///   <paramref name="validCharacters"/> is <see cref="String.Empty"/>.
   /// </exception>
   public Iso7064PureSystemDoubleCharacterAlgorithm(
      Int32 modulus,
      Int32 radix,
      String validCharacters)
   {
      if (modulus < 2)
      {
         throw new ArgumentOutOfRangeException(nameof(modulus), modulus, Resources.Iso7064ModulusOutOfRange);
      }
      if (radix < 2)
      {
         throw new ArgumentOutOfRangeException(nameof(radix), radix, Resources.Iso7064RadixOutOfRange);
      }
      ArgumentException.ThrowIfNullOrEmpty(validCharacters, nameof(validCharacters));

      _modulus = modulus;
      _radix = radix;
      _validCharacters = validCharacters;

      _reduceThreshold = Int32.MaxValue / _radix;
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

   public Boolean TryCalculateCheckDigit(
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
         var num = MapCharacterToNumber(value[index]);
         if (num == -1)
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

      first = GetCheckCharacter(quotient);
      second = GetCheckCharacter(remainder);
      if (first == CharConstants.NUL || second == CharConstants.NUL)
      {
         first = CharConstants.NUL;
         second = CharConstants.NUL;
         return false;
      }

      return true;
   }

   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < 3)
      {
         return false;
      }

      // Sum non-check digit characters and first check character.
      var sum = 0;
      for (var index = 0; index < value.Length - 1; index++)
      {
         var num = MapCharacterToNumber(value[index]);
         if (num == -1)
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
      var second = MapCharacterToNumber(value[^1]);
      if (second == -1)
      {
         return false;
      }
      sum += second;

      return sum % _modulus == 1;
   }

   private Char GetCheckCharacter(Int32 value)
      => value < 0 || value >= _validCharacters.Length ? CharConstants.NUL : _validCharacters[value];
}
