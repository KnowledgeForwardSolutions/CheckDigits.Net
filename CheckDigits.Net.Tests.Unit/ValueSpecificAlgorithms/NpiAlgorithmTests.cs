// Ignore Spelling: Npi

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

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

    #region Validate Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
       => _sut.Validate(null!).Should().BeFalse();

    [Fact]
    public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
       => _sut.Validate(string.Empty).Should().BeFalse();

    [Fact]
    public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTen()
       => _sut.Validate("000000006").Should().BeFalse();

    [Fact]
    public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanTen()
       => _sut.Validate("00000000006").Should().BeFalse();

    [Fact]
    public void NpiAlgorithm_Validate_ShouldCorrectlyPrefixValueWithConstant80840()
       => _sut.Validate("0000000006").Should().BeTrue();

    [Theory]
    [InlineData("0000000014")]
    [InlineData("0000001004")]
    [InlineData("0000100004")]
    [InlineData("0010000004")]
    [InlineData("1000000004")]
    public void NpiAlgorithm_Validate_ShouldCorrectlyWeightOddPositionCharacters(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("0000000105")]
    [InlineData("0000010005")]
    [InlineData("0001000005")]
    [InlineData("0100000005")]
    public void NpiAlgorithm_Validate_ShouldCorrectlyWeightEvenPositionCharacters(string value)
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
    public void NpiAlgorithm_Validate_ShouldCalculateCorrectDoubleForOddPositionCharacters(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("1234567893")]
    [InlineData("1245319599")]    // Example from www.hippaspace.com
    public void NpiAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("1234569071")]    // Valid NPI 1234560971 with two digit transposition 09 -> 90
    [InlineData("1230967899")]    // Valid NPI 1239067899 with two digit transposition 90 -> 09
    [InlineData("1122334497")]    // Valid NPI 1122334497 with two digit twin error 22 -> 55
    [InlineData("1122337797")]    // Valid NPI 1122334497 with two digit twin error 44 -> 77
    [InlineData("1122664497")]    // Valid NPI 1122334497 with two digit twin error 33 -> 66
    public void NpiAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("1238560971")]    // Valid NPI 1234560971 with single digit transcription error 4 -> 8
    [InlineData("1243560971")]    // Valid NPI 1234560971 with two digit transposition error 34 -> 43
    [InlineData("4422334497")]    // Valid NPI 1122334497 with two digit twin error 11 -> 44
    public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(string value)
       => _sut.Validate(value).Should().BeFalse();

    [Fact]
    public void NpiAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
       => _sut.Validate("0000000600").Should().BeTrue();

    [Theory]
    [InlineData("0000000I05")]     // Value 0000000505 would have check digit = 5. I is 20 positions later in ASCII table than 5 and would also calculate check digit 5 unless code explicitly checks for non-digit
    [InlineData("0000000+05")]     // + is 10 positions earlier in ASCII table than 5 and would also calculate check digit 5 unless code explicitly checks for non-digit
    public void NpiAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(string value)
       => _sut.Validate(value).Should().BeFalse();

    #endregion
}
