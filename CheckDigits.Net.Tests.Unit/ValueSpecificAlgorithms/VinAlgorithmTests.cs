namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

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
       char ch,
       int expected)
       => VinAlgorithm.TransliterateCharacter(ch).Should().Be(expected);

    [Theory]
    [InlineData('I')]
    [InlineData('O')]
    [InlineData('Q')]
    [InlineData('+')]
    [InlineData(';')]
    [InlineData('a')]
    public void VinAlgorithm_TransliterateCharacter_ShouldReturnMinusOne_WhenCharacterIsNotValid(char ch)
       => VinAlgorithm.TransliterateCharacter(ch).Should().Be(-1);

    #endregion

    #region TryCalculateCheckDigit Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(string.Empty, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthLessThan17()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit("12345678_0123456", out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThan17()
    {
        _sut.TryCalculateCheckDigit("12345678_012345678", out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Theory]
    [InlineData("10000000_00000000", '8')]
    [InlineData("01000000_00000000", '7')]
    [InlineData("00100000_00000000", '6')]
    [InlineData("00010000_00000000", '5')]
    [InlineData("00001000_00000000", '4')]
    [InlineData("00000100_00000000", '3')]
    [InlineData("00000010_00000000", '2')]
    [InlineData("00000001_00000000", 'X')]
    [InlineData("00000000_10000000", '9')]
    [InlineData("00000000_01000000", '8')]
    [InlineData("00000000_00100000", '7')]
    [InlineData("00000000_00010000", '6')]
    [InlineData("00000000_00001000", '5')]
    [InlineData("00000000_00000100", '4')]
    [InlineData("00000000_00000010", '3')]
    [InlineData("00000000_00000001", '2')]
    public void VinAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightCharactersByPosition(
       string value,
       char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Fact]
    public void VinAlgorithm_TryCalculateCheckDigit_ShouldIgnoreCheckDigitPosition()
    {
        // Arrange.
        var value = "10000000?00000000";
        var expectedCheckDigit = '8';

        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("1M8GDM9AXKP042788", 'X')]   // Worked example from Wikipedia (https://en.wikipedia.org/wiki/Vehicle_identification_number#Check-digit_calculation)
    [InlineData("11111111111111111", '1')]   // Test value as per Wikipedia 
    [InlineData("1G8ZG127XWZ157259", 'X')]   // Random VIN from https://vingenerator.org/
    [InlineData("1HGEM21292L047875", '9')]   // "
    public void VinAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
       string value,
       char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Fact]
    public void VinAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
    {
        // Arrange.
        var value = "00000000?00000000";
        var expectedCheckDigit = CharConstants.DigitZero;

        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Fact]
    public void VinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter()
    {
        // Arrange.
        var value = "1M8GDM9AXKPO42788";       // Zero (0) -> 'O'

        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    #endregion

    #region Validate Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
       => _sut.Validate(null!).Should().BeFalse();

    [Fact]
    public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
       => _sut.Validate(string.Empty).Should().BeFalse();

    [Fact]
    public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThan17()
       => _sut.Validate("0000000000000000").Should().BeFalse();

    [Fact]
    public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThan17()
       => _sut.Validate("000000000000000000").Should().BeFalse();

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
    public void VinAlgorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("1M8GDM9AXKP042788")]   // Worked example from Wikipedia (https://en.wikipedia.org/wiki/Vehicle_identification_number#Check-digit_calculation)
                                        //
    [InlineData("11111111111111111")]   // Test value as per Wikipedia 
    [InlineData("1G8ZG127XWZ157259")]   // Random VIN from https://vingenerator.org/
    [InlineData("1HGEM21292L047875")]   // "
    public void VinAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("11111211111111111")]   // Single character transcription error 1 -> 2
    [InlineData("1M8FDM9AXKP042788")]   // Single character transcription error G -> F
    [InlineData("1G8ZG217XWZ157259")]   // Two character transposition 12 -> 21
    [InlineData("1HGME21292L047875")]   // Two character transposition EM -> ME
    [InlineData("1HGEM21W9WL047875")]   // Two character jump transposition 292 -> W9W 
    [InlineData("11100111111111111")]   // Twin error 11 -> 00
    public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(string value)
       => _sut.Validate(value).Should().BeFalse();

    [Fact]
    public void VinAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCharacter()
       => _sut.Validate("1M8GDM9AXKPO42788").Should().BeFalse();      // Zero (0) -> 'O'

    #endregion
}
