// Ignore Spelling: Sedol

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class SedolAlgorithmTests
{
   private readonly SedolAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void SedolAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.SedolAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void SedolAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.SedolAlgorithmName);

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void SedolAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void SedolAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Fact]
   public void SedolAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanSevenCharacters()
      => _sut.Validate("000011").Should().BeFalse();

   [Fact]
   public void SedolAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanSevenCharacters()
      => _sut.Validate("00000011").Should().BeFalse();

   [Theory]
   [InlineData("0000011")]
   [InlineData("0000107")]
   [InlineData("0001003")]
   [InlineData("0010009")]
   [InlineData("0100007")]
   [InlineData("1000009")]
   public void SedolAlgorithm_Validate_ShouldCorrectlyWeightByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("0000000")]
   [InlineData("1000009")]
   [InlineData("2000008")]
   [InlineData("3000007")]
   [InlineData("4000006")]
   [InlineData("5000005")]
   [InlineData("6000004")]
   [InlineData("7000003")]
   [InlineData("8000002")]
   [InlineData("9000001")]
   [InlineData("B000009")]
   [InlineData("C000008")]
   [InlineData("D000007")]
   [InlineData("F000005")]
   [InlineData("G000004")]
   [InlineData("H000003")]
   [InlineData("J000001")]
   [InlineData("K000000")]
   [InlineData("L000009")]
   [InlineData("M000008")]
   [InlineData("N000007")]
   [InlineData("P000005")]
   [InlineData("Q000004")]
   [InlineData("R000003")]
   [InlineData("S000002")]
   [InlineData("T000001")]
   [InlineData("V000009")]
   [InlineData("W000008")]
   [InlineData("X000007")]
   [InlineData("Y000006")]
   [InlineData("Z000005")]
   public void SedolAlgorithm_Validate_ShouldCalculateCorrectlyMapCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("B0YQ5W0")]     // Apple
   [InlineData("BSJC712")]     // Tesla
   [InlineData("3134865")]     // Barclays 
   [InlineData("BPLW322")]     // Facebook ETF
   [InlineData("BF4LK58")]     // Google/Alphabet
   [InlineData("B10S3T0")]     // Oracle Corporation
   [InlineData("BRDVMH9")]     // Tesla bond
   [InlineData("BKPBC67")]     // Google bond
   public void SedolAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("3174865")]     // 3134865 with single digit transcription error 3 -> 7
   [InlineData("BTJC712")]     // BSJC712 with single character transcription error S -> T
   [InlineData("3138465")]     // 3134865 with two digit transposition error 48 -> 84 
   [InlineData("BJSC712")]     // BSJC712 with two character transposition error SJ -> JS
   [InlineData("1155334")]     // 1122334 with two digit twin error 22 -> 55
   [InlineData("BBGGDD4")]     // BBCCDD4 with two letter twin error CC -> GG
   public void SedolAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void SedolAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("0000000").Should().BeTrue();

   [Fact]
   public void SedolAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("K000000").Should().BeTrue();

   [Theory]
   [InlineData("78!7100")]
   [InlineData("78.7100")]
   [InlineData("78:7100")]
   [InlineData("78^7100")]
   [InlineData("A000000")]
   [InlineData("E000006")]
   [InlineData("I000002")]
   [InlineData("O000006")]
   [InlineData("U000000")]
   public void SedolAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
