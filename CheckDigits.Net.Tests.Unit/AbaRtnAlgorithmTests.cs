// Ignore Spelling: Aba Rtn

namespace CheckDigits.Net.Tests.Unit;

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

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AbaRtnAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void AbaRtnAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void AbaRtnAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthLessThanEight()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("1234567", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void AbaRtnAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThanEight()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("123456789", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("10000000", '7')]
   [InlineData("01000000", '3')]
   [InlineData("00100000", '9')]
   [InlineData("00010000", '7')]
   [InlineData("00001000", '3')]
   [InlineData("00000100", '9')]
   [InlineData("00000010", '7')]
   [InlineData("00000001", '3')]
   public void AbaRtnAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightWeightCharactersByPosition(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("11100002", '5')]     // Worked example from Wikipedia (https://en.wikipedia.org/wiki/ABA_routing_transit_number#Check_digit)
   [InlineData("12223582", '1')]     // US Bank
   [InlineData("32508140", '3')]     // BECU
   [InlineData("32507076", '0')]     // Chase - Washington
   [InlineData("32527202", '1')]     // Alaska USA Federal Credit Union
   public void AbaRtnAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void AbaRtnAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "00000000";
      var expectedCheckDigit = CharConstants.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("32I27202")]     // I is 20 positions later in ASCII table than 5 and would return true unless code explicitly checks for non-digit
   [InlineData("32+27202")]     // + is 10 positions earlier in ASCII table than 5 and would return true unless code explicitly checks for non-digit
   public void AbaRtnAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

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
   public void AbaRtnAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
