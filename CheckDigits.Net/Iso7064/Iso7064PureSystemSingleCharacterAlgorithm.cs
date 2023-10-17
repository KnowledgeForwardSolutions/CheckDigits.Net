namespace CheckDigits.Net.Iso7064;

/// <summary>
///   Abstract base class for an ISO/IEC 7064 pure system algorithm (i.e. an 
///   algorithm that uses a single modulus) and that generates a single check
///   character.
/// </summary>
public abstract class Iso7064PureSystemSingleCharacterAlgorithm : IIso7064Algorithm
{
   protected readonly Int32 _reduceThreshold;

   /// <summary>
   ///   Initialize a new <see cref="Iso7064PureSystemSingleCharacterAlgorithm"/>.
   /// </summary>
   /// <param name="modulus">
   ///   The value used by the algorithm modulus operation.
   /// </param>
   /// <param name="radix">
   ///   The base of the geometric progression used by the algorithm.
   /// </param>
   /// <param name="characterDomain">
   ///   The domain of characters that the algorithm operates on. Specifies both
   ///   the valid characters for the algorithm and the possible check 
   ///   characters produced by the algorithm.
   /// </param>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   <paramref name="modulus"/> is less than 2.
   ///   - or -
   ///   <paramref name="radix"/> is less than 2.
   /// </exception>
   /// <exception cref="ArgumentNullException">
   ///   <paramref name="characterDomain"/> is <see langword="null"/>.
   /// </exception>
   public Iso7064PureSystemSingleCharacterAlgorithm(
      Int32 modulus,
      Int32 radix,
      ICharacterDomain characterDomain)
   {
      if (modulus < 2)
      {
         throw new ArgumentOutOfRangeException(nameof(modulus), modulus, Resources.Iso7064ModulusOutOfRange);
      }
      if (radix < 2)
      {
         throw new ArgumentOutOfRangeException(nameof(radix), radix, Resources.Iso7064RadixOutOfRange);
      }

      Modulus = modulus;
      Radix = radix;
      CharacterDomain = characterDomain
         ?? throw new ArgumentNullException(nameof(characterDomain), Resources.CharacterDomainIsNull);

      _reduceThreshold = Int32.MaxValue / Radix;
   }

   /// <inheritdoc/>
   public ICharacterDomain CharacterDomain { get; }

   /// <inheritdoc/>
   public Int32 Modulus { get; }

   /// <inheritdoc/>
   public Int32 Radix { get; }

   /// <inheritdoc/>
   public virtual Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      for (var index = 0; index < value.Length; index++)
      {
         var num = CharacterDomain.MapCharacterToNumber(value[index]);
         if (num == -1)
         {
            return false;
         }
         sum = (sum + num) * Radix;
         if (sum >= _reduceThreshold)
         {
            sum %= Modulus;
         }
      }

      var remainder = sum % Modulus;
      var x = (Modulus - remainder + 1) % Modulus;
      checkDigit = CharacterDomain.GetCheckCharacter(x);

      return true;
   }

   /// <inheritdoc/>
   public virtual Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < 2)
      {
         return false;
      }

      var sum = 0;
      Int32 num;
      for(var index = 0; index < value.Length - 1; index++)
      {
         num = CharacterDomain.MapCharacterToNumber(value[index]);
         if (num == -1)
         {
            return false;
         }
         sum = (sum + num) * Radix;
         if (sum >= _reduceThreshold)
         {
            sum %= Modulus;
         }
      }

      num = CharacterDomain.MapCheckCharacterToNumber(value[^1]);
      if (num == -1)
      {
         return false;
      }

      sum += num;

      return sum % Modulus == 1;
   }

   //public Boolean Validate2(String value)
   //{
   //   if (String.IsNullOrEmpty(value) || value.Length < 2)
   //   {
   //      return false;
   //   }

   //   var sum = 0;
   //   Int32 num;
   //   for (var index = 0; index < value.Length - 1; index++)
   //   {
   //      num = value[index].ToIntegerDigit();
   //      if (num < 0 || num > 9)
   //      {
   //         return false;
   //      }
   //      sum = (sum + num) * Radix;
   //      if (sum >= _reduceThreshold)
   //      {
   //         sum %= Modulus;
   //      }
   //   }

   //   num = value[^1].ToIntegerDigit();
   //   if (num == 40)
   //   {
   //      num = 10;
   //   }
   //   else if (num < 0 || num > 9)
   //   {
   //      return false;
   //   }

   //   sum += num;

   //   return sum % Modulus == 1;
   //}

   //private const String _valueChars = "0123456789";
   //private const String _checkChars = "0123456789X";

   //public Boolean Validate3(String value)
   //{
   //   if (String.IsNullOrEmpty(value) || value.Length < 2)
   //   {
   //      return false;
   //   }

   //   var sum = 0;
   //   Int32 num;
   //   for (var index = 0; index < value.Length - 1; index++)
   //   {
   //      num = _valueChars.IndexOf(value[index]);
   //      if (num < 0)
   //      {
   //         return false;
   //      }
   //      sum = (sum + num) * Radix;
   //      if (sum >= _reduceThreshold)
   //      {
   //         sum %= Modulus;
   //      }
   //   }

   //   num = _checkChars.IndexOf(value[^1]);
   //   if (num < 0)
   //   {
   //      return false;
   //   }

   //   sum += num;

   //   return sum % Modulus == 1;
   //}

   //private static readonly DigitsSupplementaryDomain2 _domain2 = new();

   //public Boolean Validate4(String value)
   //{
   //   if (String.IsNullOrEmpty(value) || value.Length < 2)
   //   {
   //      return false;
   //   }

   //   var sum = 0;
   //   Int32 num;
   //   for (var index = 0; index < value.Length - 1; index++)
   //   {
   //      if (!_domain2.TryGetValue(value[index], out num))
   //      {
   //         return false;
   //      }
   //      sum = (sum + num) * Radix;
   //      if (sum >= _reduceThreshold)
   //      {
   //         sum %= Modulus;
   //      }
   //   }

   //   if (!_domain2.TryGetCheckCharacterValue(value[^1], out num))
   //   {
   //      return false;
   //   }

   //   sum += num;

   //   return sum % Modulus == 1;
   //}

   //public Boolean Validate5(String value)
   //{
   //   if (String.IsNullOrEmpty(value) || value.Length < 2)
   //   {
   //      return false;
   //   }

   //   var sum = 0;
   //   Int32 num;
   //   for (var index = 0; index < value.Length - 1; index++)
   //   {
   //      num = _domain2.GetCharacterValue(value[index]);
   //      if (num == -1)
   //      {
   //         return false;
   //      }
   //      sum = (sum + num) * Radix;
   //      if (sum >= _reduceThreshold)
   //      {
   //         sum %= Modulus;
   //      }
   //   }

   //   num = _domain2.GetCharacterValue(value[^1]);
   //   if (num == -1)
   //   {
   //      return false;
   //   }

   //   sum += num;

   //   return sum % Modulus == 1;
   //}

   //private static readonly Int32[] _lookupTable =
   //   Enumerable.Range(0, 41).Select(x => ((x >= 0 && x <= 9) ? x : (x == 40 ? 10 : -1))).ToArray();

   //public Boolean Validate6(String value)
   //{
   //   if (String.IsNullOrEmpty(value) || value.Length < 2)
   //   {
   //      return false;
   //   }

   //   var sum = 0;
   //   Int32 num;
   //   for (var index = 0; index < value.Length - 1; index++)
   //   {
   //      num = GetCharacterValue(value[index]);
   //      if (num == -1)
   //      {
   //         return false;
   //      }
   //      sum = (sum + num) * Radix;
   //      if (sum >= _reduceThreshold)
   //      {
   //         sum %= Modulus;
   //      }
   //   }

   //   num = GetCharacterValue(value[^1]);
   //   if (num == -1)
   //   {
   //      return false;
   //   }

   //   sum += num;

   //   return sum % Modulus == 1;
   //}

   //private static Int32 GetCharacterValue(Char ch)
   //{
   //   var index = ch - '0';
   //   if (index < 0 || index > 40)
   //   {
   //      return -1;
   //   }

   //   return _lookupTable[index];
   //}
}
