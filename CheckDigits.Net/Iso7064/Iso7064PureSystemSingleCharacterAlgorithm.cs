namespace CheckDigits.Net.Iso7064;

/// <summary>
///   Generic ISO/IEC 7064 pure system algorithm (i.e. an algorithm that uses a 
///   single modulus) that generates a single check character.
/// </summary>
public class Iso7064PureSystemSingleCharacterAlgorithm : ISingleCheckDigitAlgorithm
{
   private readonly ISupplementalCharacterAlphabet _alphabet;
   private readonly Int32 _modulus;
   private readonly Int32 _radix;
   private readonly Int32 _reduceThreshold;

   /// <summary>
   ///   Initialize a new <see cref="Iso7064PureSystemSingleCharacterAlgorithm"/>.
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
   ///   <see cref="ISupplementalCharacterAlphabet"/> used to map characters to 
   ///   their integer equivalents and vice versa.
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
   public Iso7064PureSystemSingleCharacterAlgorithm(
      String algorithmName,
      String algorithmDescription,
      Int32 modulus,
      Int32 radix,
      ISupplementalCharacterAlphabet alphabet)
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
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
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

      var remainder = sum % _modulus;
      var x = (_modulus - remainder + 1) % _modulus;
      checkDigit = _alphabet.IntegerToCheckCharacter(x);

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
      for (var index = 0; index < value.Length - 1; index++)
      {
         num = _alphabet.CharacterToInteger(value[index]);
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

      num = _alphabet.CheckCharacterToInteger(value[^1]);
      if (num == -1)
      {
         return false;
      }

      sum += num;

      return sum % _modulus == 1;
   }
}