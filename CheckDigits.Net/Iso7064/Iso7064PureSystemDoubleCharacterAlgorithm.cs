namespace CheckDigits.Net.Iso7064;

/// <summary>
///   Generic ISO/IEC 7064 pure system algorithm (i.e. an algorithm that uses a 
///   single modulus) that generates two check characters.
/// </summary>
public class Iso7064PureSystemDoubleCharacterAlgorithm : IDoubleCheckDigitAlgorithm
{
   private readonly IAlphabet _alphabet;
   private readonly Int32 _modulus;
   private readonly Int32 _radix;
   private readonly Int32 _reduceThreshold;

   /// <summary>
   ///   Initialize a new <see cref="Iso7064PureSystemDoubleCharacterAlgorithm"/>.
   /// </summary>
   /// <param name="algorithmName">
   ///   The algorithm name.
   /// </param>
   /// <param name="algorithmDescription">
   ///   Description of the algorithm.
   /// </param>
   /// <param name="modulus">
   ///   The value used by the algorithm modulus operation.
   /// </param>
   /// <param name="radix">
   ///   The base of the geometric progression used by the algorithm.
   /// </param>
   /// <param name="alphabet">
   ///   <see cref="IAlphabet"/> used to map characters to their integer 
   ///   equivalents and vice versa.
   /// </param>
   /// <exception cref="ArgumentNullException">
   ///   <paramref name="algorithmName"/> is <see langword="null"/>.
   ///   - or -
   ///   <paramref name="algorithmDescription"/> is <see langword="null"/>.
   ///   - or -
   ///   <paramref name="alphabet"/> is <see langword="null"/>.
   /// </exception>
   /// <exception cref="ArgumentException">
   ///   <paramref name="algorithmName"/> is <see cref="String.Empty"/> or all
   ///   whitespace characters.
   ///   - or. 
   ///   <paramref name="algorithmDescription"/> is <see cref="String.Empty"/> 
   ///   or all whitespace characters.
   /// </exception>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   <paramref name="modulus"/> is less than 2.
   ///   - or -
   ///   <paramref name="radix"/> is less than 2.
   /// </exception>
   public Iso7064PureSystemDoubleCharacterAlgorithm(
      String algorithmName,
      String algorithmDescription,
      Int32 modulus,
      Int32 radix,
      IAlphabet alphabet)
   {
      _ = algorithmName ?? throw new ArgumentNullException(nameof(algorithmName), Resources.AlgorithmNameIsEmptyMessage);
      if (String.IsNullOrWhiteSpace(algorithmName))
      {
         throw new ArgumentException(Resources.AlgorithmNameIsEmptyMessage, nameof(algorithmName));
      }
      _ = algorithmDescription ?? throw new ArgumentNullException(nameof(algorithmDescription), Resources.AlgorithmDescriptionIsEmptyMessage);
      if (String.IsNullOrWhiteSpace(algorithmDescription))
      {
         throw new ArgumentException(Resources.AlgorithmDescriptionIsEmptyMessage, nameof(algorithmDescription));
      }
      if (modulus < 2)
      {
         throw new ArgumentOutOfRangeException(nameof(modulus), modulus, Resources.Iso7064ModulusOutOfRange);
      }
      if (radix < 2)
      {
         throw new ArgumentOutOfRangeException(nameof(radix), radix, Resources.Iso7064RadixOutOfRange);
      }
      _ = alphabet ?? throw new ArgumentNullException(nameof(alphabet), Resources.Iso7046AlphabetIsNull);

      AlgorithmName = algorithmName;
      AlgorithmDescription = algorithmDescription;
      _modulus = modulus;
      _radix = radix;
      _alphabet = alphabet;

      _reduceThreshold = Int32.MaxValue / _radix;
   }

   /// <inheritdoc/>
   public String AlgorithmDescription { get; }

   /// <inheritdoc/>
   public String AlgorithmName { get; }

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
         var num = _alphabet.CharacterToInteger(value[index]);
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

      first = _alphabet.IntegerToCheckCharacter(quotient);
      second = _alphabet.IntegerToCheckCharacter(remainder);

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
      for (var index = 0; index < value.Length - 1; index++)
      {
         var num = _alphabet.CharacterToInteger(value[index]);
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
      var second = _alphabet.CharacterToInteger(value[^1]);
      if (second == -1)
      {
         return false;
      }
      sum += second;

      return sum % _modulus == 1;
   }
}
