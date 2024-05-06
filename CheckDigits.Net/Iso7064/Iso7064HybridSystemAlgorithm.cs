namespace CheckDigits.Net.Iso7064;

/// <summary>
///   Generic ISO/IEC 7064 hybrid system algorithm (i.e. an algorithm that uses 
///   two modulus values, M and M+1) that generates a single check character.
/// </summary>
public class Iso7064HybridSystemAlgorithm : ISingleCheckDigitAlgorithm
{
   private readonly IAlphabet _alphabet;
   private readonly Int32 _modulus;
   private readonly Int32 _modulusPlus1;

   /// <summary>
   ///   Initialize a new <see cref="Iso7064HybridSystemAlgorithm"/>.
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
   /// </exception>
   public Iso7064HybridSystemAlgorithm(
      String algorithmName,
      String algorithmDescription,
      Int32 modulus,
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
      _ = alphabet ?? throw new ArgumentNullException(nameof(alphabet), Resources.Iso7046AlphabetIsNull);

      AlgorithmName = algorithmName;
      AlgorithmDescription = algorithmDescription;
      _modulus = modulus;
      _alphabet = alphabet;
      _modulusPlus1 = _modulus + 1;
   }

   /// <inheritdoc/>
   public String AlgorithmDescription { get; }

   /// <inheritdoc/>
   public String AlgorithmName { get; }

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var product = _modulus;
      for (var index = 0; index < value.Length; index++)
      {
         var num = _alphabet.CharacterToInteger(value[index]);
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

      var product = _modulus;
      Int32 num;
      for (var index = 0; index < value.Length - 1; index++)
      {
         num = _alphabet.CharacterToInteger(value[index]);
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

      num = _alphabet.CharacterToInteger(value[^1]);
      if (num == -1)
      {
         return false;
      }

      product += num;

      return product % _modulus == 1;
   }
}
