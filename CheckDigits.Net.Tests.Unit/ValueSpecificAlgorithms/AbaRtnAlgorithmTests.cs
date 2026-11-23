// Ignore Spelling: Aba Rtn

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class AbaRtnAlgorithmTests
{
   private readonly AbaRtnAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AbaRtnAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.AbaRtnAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AbaRtnAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.AbaRtnAlgorithmName);

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AbaRtnAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void AbaRtnAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Fact]
   public void AbaRtnAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanNine()
      => _sut.Validate("12345678").Should().BeFalse();

   [Fact]
   public void AbaRtnAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanNine()
      => _sut.Validate("1234567890").Should().BeFalse();

   [Theory]
   [InlineData("100000007")]
   [InlineData("010000003")]
   [InlineData("001000009")]
   [InlineData("000100007")]
   [InlineData("000010003")]
   [InlineData("000001009")]
   [InlineData("000000107")]
   [InlineData("000000013")]
   public void AbaRtnAlgorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("111000025")]     // Worked example from Wikipedia (https://en.wikipedia.org/wiki/ABA_routing_transit_number#Check_digit)
   [InlineData("122235821")]     // US Bank
   [InlineData("325081403")]     // BECU
   [InlineData("325070760")]     // Chase - Washington
   [InlineData("325272021")]     // Alaska USA Federal Credit Union
   public void AbaRtnAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("325722021")]     // Alaska USA with two digit transposition with delta of 5: 27 -> 72
   public void AbaRtnAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("122238821")]     // US Bank with single digit transcription error 5 -> 8
   [InlineData("352081403")]     // BECU two digit transposition error 25 -> 52
   [InlineData("305270760")]     // Chase - WA with jump transposition 250 -> 052
   [InlineData("111235821")]     // US Bank with twin error 22 -> 11
   [InlineData("325373021")]     // Alaska USA with jump twin error 272 -> 373
   public void AbaRtnAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void AbaRtnAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000000000").Should().BeTrue();

   [Theory]
   [InlineData("32I272021")]     // I is 20 positions later in ASCII table than 5 and would return true unless code explicitly checks for non-digit
   [InlineData("32+272021")]     // + is 10 positions earlier in ASCII table than 5 and would return true unless code explicitly checks for non-digit
   [InlineData("32i272021")]
   [InlineData("32\u2153272021")]     // Unicode fraction 1/3
   [InlineData("32\u0BE6272021")]     // Unicode Tamil digit 0
   [InlineData("32\u00D6272021")]     // Unicode uppercase O with umlaut
   [InlineData("32\u00F6272021")]     // Unicode lowercase o with umlaut
   public void AbaRtnAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("11100002I")]
   [InlineData("11100002+")]
   [InlineData("11100002i")]
   [InlineData("11100002\u2153")]     // Unicode fraction 1/3
   [InlineData("11100002\u0BE6")]     // Unicode Tamil digit 0
   [InlineData("11100002\u00D6")]     // Unicode uppercase O with umlaut
   [InlineData("11100002\u00F6")]     // Unicode lowercase o with umlaut
   public void AbaRtnAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCheckCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("111000025")]
   [InlineData("122235821")]
   [InlineData("325081403")]
   public void AbaRtnAlgorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion
}
