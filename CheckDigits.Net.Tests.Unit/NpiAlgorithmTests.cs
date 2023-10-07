// Ignore Spelling: Npi

namespace CheckDigits.Net.Tests.Unit;

public class NpiAlgorithmTests
{
   private readonly NpiAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void NpiAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.NpiAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void NpiAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.NpiAlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthLessThanNine()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("12345678", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThanNine()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("1234567890", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("000000001", '4')]
   [InlineData("000000100", '4')]
   [InlineData("000010000", '4')]
   [InlineData("001000000", '4')]
   [InlineData("100000000", '4')]
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightOddPositionCharacters(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("000000010", '5')]
   [InlineData("000001000", '5')]
   [InlineData("000100000", '5')]
   [InlineData("010000000", '5')]
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightEvenPositionCharacters(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("000000000", '6')]
   [InlineData("000000001", '4')]
   [InlineData("000000002", '2')]
   [InlineData("000000003", '0')]
   [InlineData("000000004", '8')]
   [InlineData("000000005", '5')]
   [InlineData("000000006", '3')]
   [InlineData("000000007", '1')]
   [InlineData("000000008", '9')]
   [InlineData("000000009", '7')]
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldCalculateCorrectDoubleForOddPositionCharacters(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("123456789", '3')]
   [InlineData("124531959", '9')]    // Example from www.hippaspace.com
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "000000000";
      var expectedCheckDigit = '6';

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("0000000I0")]     // Value 000000050 would have check digit = 5. I is 20 positions later in ASCII table than 5 and would also calculate check digit 5 unless code explicitly checks for non-digit
   [InlineData("0000000+0")]     // + is 10 positions earlier in ASCII table than 5 and would also calculate check digit 5 unless code explicitly checks for non-digit
   public void NpiAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Fact]
   public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTen()
      => _sut.Validate("123456789").Should().BeFalse();

   [Fact]
   public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanTen()
      => _sut.Validate("12345678901").Should().BeFalse();

   [Fact]
   public void NpiAlgorithm_Validate_ShouldCorrectlyPrefixValueWithConstant80840()
      => _sut.Validate("0000000006").Should().BeTrue();

   [Theory]
   [InlineData("0000000014")]
   [InlineData("0000001004")]
   [InlineData("0000100004")]
   [InlineData("0010000004")]
   [InlineData("1000000004")]
   public void NpiAlgorithm_Validate_ShouldCorrectlyWeightOddPositionCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("0000000105")]
   [InlineData("0000010005")]
   [InlineData("0001000005")]
   [InlineData("0100000005")]
   public void NpiAlgorithm_Validate_ShouldCorrectlyWeightEvenPositionCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("0000000006")]
   [InlineData("0000000014")]
   [InlineData("0000000022")]
   [InlineData("0000000030")]
   [InlineData("0000000048")]
   [InlineData("0000000055")]
   [InlineData("0000000063")]
   [InlineData("0000000071")]
   [InlineData("0000000089")]
   [InlineData("0000000097")]
   public void NpiAlgorithm_Validate_ShouldCalculateCorrectDoubleForOddPositionCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1234567893")]
   [InlineData("1245319599")]    // Example from www.hippaspace.com
   public void NpiAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1234569071")]    // Valid NPI 1234560971 with two digit transposition 09 -> 90
   [InlineData("1230967899")]    // Valid NPI 1239067899 with two digit transposition 90 -> 09
   [InlineData("1122334497")]    // Valid NPI 1122334497 with two digit twin error 22 -> 55
   [InlineData("1122337797")]    // Valid NPI 1122334497 with two digit twin error 44 -> 77
   [InlineData("1122664497")]    // Valid NPI 1122334497 with two digit twin error 33 -> 66
   public void NpiAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1238560971")]    // Valid NPI 1234560971 with single digit transcription error 4 -> 8
   [InlineData("1243560971")]    // Valid NPI 1234560971 with two digit transposition error 34 -> 43
   [InlineData("4422334497")]    // Valid NPI 1122334497 with two digit twin error 11 -> 44
   public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void NpiAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("0000000600").Should().BeTrue();

   [Theory]
   [InlineData("0000000I05")]     // Value 0000000505 would have check digit = 5. I is 20 positions later in ASCII table than 5 and would also calculate check digit 5 unless code explicitly checks for non-digit
   [InlineData("0000000+05")]     // + is 10 positions earlier in ASCII table than 5 and would also calculate check digit 5 unless code explicitly checks for non-digit
   public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
