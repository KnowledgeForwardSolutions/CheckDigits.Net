namespace CheckDigits.Net.Tests.Unit;

public class VinAlgorithmTests
{
   private readonly VinAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VinAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.VinAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VinAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.VinAlgorithmName);

   #endregion

   #region TransliterateCharacter Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('0', 0)]
   [InlineData('1', 1)]
   [InlineData('2', 2)]
   [InlineData('3', 3)]
   [InlineData('4', 4)]
   [InlineData('5', 5)]
   [InlineData('6', 6)]
   [InlineData('7', 7)]
   [InlineData('8', 8)]
   [InlineData('9', 9)]
   [InlineData('A', 1)]
   [InlineData('B', 2)]
   [InlineData('C', 3)]
   [InlineData('D', 4)]
   [InlineData('E', 5)]
   [InlineData('F', 6)]
   [InlineData('G', 7)]
   [InlineData('H', 8)]
   [InlineData('J', 1)]
   [InlineData('K', 2)]
   [InlineData('L', 3)]
   [InlineData('M', 4)]
   [InlineData('N', 5)]
   [InlineData('P', 7)]
   [InlineData('R', 9)]
   [InlineData('S', 2)]
   [InlineData('T', 3)]
   [InlineData('U', 4)]
   [InlineData('V', 5)]
   [InlineData('W', 6)]
   [InlineData('X', 7)]
   [InlineData('Y', 8)]
   [InlineData('Z', 9)]
   public void VinAlgorithm_TransliterateCharacter_ShouldReturnExpectedValue_WhenCharacterIsValid(
      Char ch,
      Int32 expected)
      => VinAlgorithm.TransliterateCharacter(ch).Should().Be(expected);

   [Theory]
   [InlineData('I')]
   [InlineData('O')]
   [InlineData('Q')]
   [InlineData('+')]
   [InlineData(';')]
   [InlineData('a')]
   public void VinAlgorithm_TransliterateCharacter_ShouldReturnMinusOne_WhenCharacterIsNotValid(Char ch)
      => VinAlgorithm.TransliterateCharacter(ch).Should().Be(-1);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Fact]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThan17()
      => _sut.Validate("1234567890123456").Should().BeFalse();

   [Fact]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThan17()
      => _sut.Validate("123456789012345678").Should().BeFalse();

   [Theory]
   [InlineData("10000000800000000")]
   [InlineData("01000000700000000")]
   [InlineData("00100000600000000")]
   [InlineData("00010000500000000")]
   [InlineData("00001000400000000")]
   [InlineData("00000100300000000")]
   [InlineData("00000010200000000")]
   [InlineData("00000001X00000000")]
   [InlineData("00000000910000000")]
   [InlineData("00000000801000000")]
   [InlineData("00000000700100000")]
   [InlineData("00000000600010000")]
   [InlineData("00000000500001000")]
   [InlineData("00000000400000100")]
   [InlineData("00000000300000010")]
   [InlineData("00000000200000001")]
   public void Modulus11Algorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1M8GDM9AXKP042788")]   // Worked example from Wikipedia (https://en.wikipedia.org/wiki/Vehicle_identification_number#Check-digit_calculation)
   //
   [InlineData("11111111111111111")]   // Test value as per Wikipedia 
   [InlineData("1G8ZG127XWZ157259")]   // Random VIN from https://vingenerator.org/
   [InlineData("1HGEM21292L047875")]   // "
   public void VinAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("11111211111111111")]   // Single character transcription error 1 -> 2
   [InlineData("1M8FDM9AXKP042788")]   // Single character transcription error G -> F
   [InlineData("1G8ZG217XWZ157259")]   // Two character transposition 12 -> 21
   [InlineData("1HGME21292L047875")]   // Two character transposition EM -> ME
   [InlineData("1HGEM21W9WL047875")]   // Two character jump transposition 292 -> W9W 
   [InlineData("11100111111111111")]   // Twin error 11 -> 00
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCharacter()
      => _sut.Validate("1M8GDM9AXKPO42788").Should().BeFalse();      // Zero (0) -> 'O'

   #endregion
}
