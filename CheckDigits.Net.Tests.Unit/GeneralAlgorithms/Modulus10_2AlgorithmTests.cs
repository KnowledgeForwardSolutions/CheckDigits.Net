namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class Modulus10_2AlgorithmTests
{
    private readonly Modulus10_2Algorithm _sut = new();

    #region AlgorithmDescription Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void Modulus10_2Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
       => _sut.AlgorithmDescription.Should().Be(Resources.Modulus10_2AlgorithmDescription);

    #endregion

    #region AlgorithmName Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void Modulus10_2Algorithm_AlgorithmName_ShouldReturnExpectedValue()
       => _sut.AlgorithmName.Should().Be(Resources.Modulus10_2AlgorithmName);

    #endregion

    #region TryCalculateCheckDigit Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(string.Empty, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThanNine()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit("1234567890", out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Theory]
    [InlineData("000000001", '2')]
    [InlineData("000000010", '3')]
    [InlineData("000000100", '4')]
    [InlineData("000001000", '5')]
    [InlineData("000010000", '6')]
    [InlineData("000100000", '7')]
    [InlineData("001000000", '8')]
    [InlineData("010000000", '9')]
    [InlineData("100000000", '0')]
    public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightCharactersByPosition(
       string value,
       char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        expectedCheckDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("907472", '9')]       // Worked example of IMO Number from Wikipedia https://en.wikipedia.org/wiki/IMO_number
    [InlineData("970779", '2')]       // IMO Number from https://www.marinetraffic.com/en/ais/details/ships/shipid:155821/mmsi:219018788/imo:9707792/vessel:CARRIER#:~:text=CARRIER%20(IMO%3A%209707792)%20is,under%20the%20flag%20of%20Denmark.
    [InlineData("101056", '9')]       // IMO Number from Wikipedia https://commons.wikimedia.org/wiki/Category:Ships_by_IMO_number
    public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
       string value,
       char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Fact]
    public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
    {
        // Arrange.
        var value = "00000";
        var expectedCheckDigit = CharConstants.DigitZero;

        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("12G450")]
    [InlineData("12)450")]
    public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(string value)
    {
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    #endregion

    #region Validate Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
       => _sut.Validate(null!).Should().BeFalse();

    [Fact]
    public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
       => _sut.Validate(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData("0")]       // Zero would return true unless length is explicitly checked.
    [InlineData("1")]
    public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTwo(string value)
       => _sut.Validate(value).Should().BeFalse();

    [Theory]
    [InlineData("0000000012")]
    [InlineData("0000000103")]
    [InlineData("0000001004")]
    [InlineData("0000010005")]
    [InlineData("0000100006")]
    [InlineData("0001000007")]
    [InlineData("0010000008")]
    [InlineData("0100000009")]
    [InlineData("1000000000")]
    public void Modulus10_2Algorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("9074729")]       // Worked example of IMO Number from Wikipedia https://en.wikipedia.org/wiki/IMO_number
    [InlineData("9707792")]       // IMO Number from https://www.marinetraffic.com/en/ais/details/ships/shipid:155821/mmsi:219018788/imo:9707792/vessel:CARRIER#:~:text=CARRIER%20(IMO%3A%209707792)%20is,under%20the%20flag%20of%20Denmark.
    [InlineData("1010569")]       // IMO Number from Wikipedia https://commons.wikimedia.org/wiki/Category:Ships_by_IMO_number
    public void Modulus10_2Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("1020569")]       // IMO Number 1010569 with single digit transcription error 1 -> 2
    [InlineData("9704729")]       // IMO Number 9074729 with two digit transposition error 07 -> 70
    [InlineData("9470729")]       // IMO Number 9074729 with jump transposition 074 -> 470
    [InlineData("9706692")]       // IMO Number 9707792 with twin error 77 -> 66
    public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(string value)
       => _sut.Validate(value).Should().BeFalse();

    [Fact]
    public void Modulus10_2Algorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
       => _sut.Validate("0000000000").Should().BeTrue();

    [Fact]
    public void ModulusAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
       => _sut.Validate("1010480").Should().BeTrue();

    [Theory]
    [InlineData("12G4505")]     // Value 12345 would have check digit = 0. G is 20 positions later in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
    [InlineData("12)4505")]     // ) is 10 positions earlier in ASCII table than 3 and would also calculate check digit 0 unless code explicitly checks for non-digit
    public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(string value)
       => _sut.Validate(value).Should().BeFalse();

    #endregion
}
