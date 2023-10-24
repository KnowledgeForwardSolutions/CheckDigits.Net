// Ignore Spelling: Nhs

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class NhsAlgorithmTests
{
    private readonly NhsAlgorithm _sut = new();

    #region AlgorithmDescription Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void NhsAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
       => _sut.AlgorithmDescription.Should().Be(Resources.NhsAlgorithmDescription);

    #endregion

    #region AlgorithmName Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void NhsAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
       => _sut.AlgorithmName.Should().Be(Resources.NhsAlgorithmName);

    #endregion

    #region Validate Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void NhsAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
       => _sut.Validate(null!).Should().BeFalse();

    [Fact]
    public void NhsAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
       => _sut.Validate(string.Empty).Should().BeFalse();

    [Fact]
    public void NhsAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTen()
       => _sut.Validate("000000000").Should().BeFalse();

    [Fact]
    public void NhsAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanTen()
       => _sut.Validate("00000000000").Should().BeFalse();

    [Theory]
    [InlineData("0000000019")]
    [InlineData("0000000108")]
    [InlineData("0000001007")]
    [InlineData("0000010006")]
    [InlineData("0000100005")]
    [InlineData("0001000004")]
    [InlineData("0010000003")]
    [InlineData("0100000002")]
    [InlineData("1000000001")]
    public void NhsAlgorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("9434765919")]    // Worked example from Wikipedia https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters
    [InlineData("4505577104")]    // Example from https://www.clatterbridgecc.nhs.uk/patients/general-information/nhs-number#:~:text=Your%20NHS%20Number%20is%20printed,is%20an%20example%20number%20only).
    [InlineData("5301194917")]    // Random NHS number from http://danielbayley.uk/nhs-number/
    [InlineData("8514468243")]    // "
    [InlineData("3967487881")]    // "
    public void NhsAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("9434764919")]    // Valid NHS number (9434765919) with single digit transcription error 5 -> 4
    [InlineData("4550577104")]    // Valid NHS number (4505577104) with two digit transposition error 05 -> 50
    [InlineData("3946787881")]    // Valid NHS number (9876544321) with jump transposition 674 -> 467
    [InlineData("8515568243")]    // Valid NHS number (8514468243) with twin error 44 -> 55
    public void NhsAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(string value)
       => _sut.Validate(value).Should().BeFalse();

    [Fact]
    public void NhsAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
       => _sut.Validate("0000000000").Should().BeTrue();

    [Fact]
    public void NhsAlgorithm_Validate_ShouldReturnFalse_WhenModulus11HasRemainderOf10()
       => _sut.Validate("010000010:").Should().BeFalse();

    [Theory]
    [InlineData("1000G00005")]    // Value 1000300005 would have check digit = 5. G is 20 positions later in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
    [InlineData("1000)00005")]    // ) is 10 positions earlier in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
    public void NhsAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(string value)
       => _sut.Validate(value).Should().BeFalse();

    #endregion
}
