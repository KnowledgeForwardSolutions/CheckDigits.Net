namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class Modulus10_1AlgorithmTests
{
    private readonly Modulus10_1Algorithm _sut = new();

    #region AlgorithmDescription Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void Modulus10_1Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
       => _sut.AlgorithmDescription.Should().Be(Resources.Modulus10_1AlgorithmDescription);

    #endregion

    #region AlgorithmName Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void Modulus10_1Algorithm_AlgorithmName_ShouldReturnExpectedValue()
       => _sut.AlgorithmName.Should().Be(Resources.Modulus10_1AlgorithmName);

    #endregion

    #region TryCalculateCheckDigit Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThanNine()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit("1234567890", out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Theory]
    [InlineData("000000001", '1')]
    [InlineData("000000010", '2')]
    [InlineData("000000100", '3')]
    [InlineData("000001000", '4')]
    [InlineData("000010000", '5')]
    [InlineData("000100000", '6')]
    [InlineData("001000000", '7')]
    [InlineData("010000000", '8')]
    [InlineData("100000000", '9')]
    public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightCharactersByPosition(
       String value,
       Char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("773218", '5')]       // Worked example of CAS Registry Number from Wikipedia https://en.wikipedia.org/wiki/CAS_Registry_Number
    [InlineData("5808", '2')]         // CAS Registry Number for caffeine
    [InlineData("2872855", '4')]      // CAS Registry Number for Hexadimethrine bromide
    public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
       String value,
       Char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Fact]
    public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
    {
        // Arrange.
        var value = "00000";
        var expectedCheckDigit = CharConstants.DigitZero;

        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("714G2")]
    [InlineData("714)2")]
    public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
    {
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    #endregion

    #region Validate Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void Modulus10_1Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
       => _sut.Validate(null!).Should().BeFalse();

    [Fact]
    public void Modulus10_1Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
       => _sut.Validate(String.Empty).Should().BeFalse();

    [Theory]
    [InlineData("0")]       // Zero would return true unless length is explicitly checked.
    [InlineData("1")]
    public void Modulus10_1Algorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTwo(String value)
       => _sut.Validate(value).Should().BeFalse();

    [Theory]
    [InlineData("0000000011")]
    [InlineData("0000000102")]
    [InlineData("0000001003")]
    [InlineData("0000010004")]
    [InlineData("0000100005")]
    [InlineData("0001000006")]
    [InlineData("0010000007")]
    [InlineData("0100000008")]
    [InlineData("1000000009")]
    public void Modulus10_1Algorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(String value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("7732185")]       // Worked example of CAS Registry Number from Wikipedia https://en.wikipedia.org/wiki/CAS_Registry_Number
    [InlineData("58082")]         // CAS Registry Number for caffeine
    [InlineData("28728554")]      // CAS Registry Number for Hexadimethrine bromide
    public void Modulus10_1Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("7742185")]       // CAS Registry Number 7732185 with single digit transcription error 3 -> 4
    [InlineData("50882")]         // CAS Registry Number 58082 with two digit transposition error 80 -> 08
    [InlineData("28827554")]      // CAS Registry Number 28728554 with jump transposition 728 -> 827
    [InlineData("6632185")]       // CAS Registry Number 7732185 with twin error 77 -> 66
    public void Modulus10_1Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
       => _sut.Validate(value).Should().BeFalse();

    [Fact]
    public void Modulus10_1Algorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
       => _sut.Validate("0000000000").Should().BeTrue();

    [Fact]
    public void ModulusAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
       => _sut.Validate("1000000010").Should().BeTrue();

    [Theory]
    [InlineData("714G2")]       // Value 7143 would have check digit = 2. G is 20 positions later in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
    [InlineData("714)2")]       // ) is 10 positions earlier in ASCII table than 3 and would also calculate check digit 0 unless code explicitly checks for non-digit
    public void Modulus10_1Algorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
       => _sut.Validate(value).Should().BeFalse();

    #endregion
}
