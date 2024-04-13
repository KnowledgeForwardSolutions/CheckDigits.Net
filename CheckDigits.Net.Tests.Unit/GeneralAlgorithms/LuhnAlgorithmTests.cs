// Ignore Spelling: Luhn

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class LuhnAlgorithmTests
{
    private readonly LuhnAlgorithm _sut = new();

    #region AlgorithmDescription Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void LuhnAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
       => _sut.AlgorithmDescription.Should().Be(Resources.LuhnAlgorithmDescription);

    #endregion

    #region AlgorithmName Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void LuhnAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
       => _sut.AlgorithmName.Should().Be(Resources.LuhnAlgorithmName);

    #endregion

    #region TryCalculateCheckDigit Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void LuhnAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void LuhnAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Theory]
    [InlineData("000000001", '8')]
    [InlineData("000000100", '8')]
    [InlineData("000010000", '8')]
    [InlineData("001000000", '8')]
    [InlineData("100000000", '8')]
    public void LuhnAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightOddPositionCharacters(
       String value,
       Char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("000000010", '9')]
    [InlineData("000001000", '9')]
    [InlineData("000100000", '9')]
    [InlineData("010000000", '9')]
    public void LuhnAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightEvenPositionCharacters(
       String value,
       Char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("0", '0')]
    [InlineData("1", '8')]
    [InlineData("2", '6')]
    [InlineData("3", '4')]
    [InlineData("4", '2')]
    [InlineData("5", '9')]
    [InlineData("6", '7')]
    [InlineData("7", '5')]
    [InlineData("8", '3')]
    [InlineData("9", '1')]
    public void LuhnAlgorithm_TryCalculateCheckDigit_ShouldCalculateCorrectDoubleForOddPositionCharacters(
       String value,
       Char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("37828224631000", '5')]     // American Express test credit card number
    [InlineData("601111111111111", '7')]    // Discover test credit card number
    [InlineData("555555555555444", '4')]    // MasterCard test credit card number
    [InlineData("401288888888188", '1')]    // Visa test credit card number
    [InlineData("305693000902000", '4')]    // Diners Club test credit card number
    [InlineData("356611111111111", '3')]    // JCB test credit card number
    [InlineData("80840123456789", '3')]     // NPI (National Provider Identifier), including 80840 prefix
    [InlineData("49015420323751", '8')]     // IMEI (International Mobile Equipment Identity)
    [InlineData("29344343", '8')]           // Canadian Social Insurance Number from https://www.ibm.com/docs/en/sga?topic=patterns-canada-social-insurance-number
    [InlineData("51170095", '7')]           // "
    public void LuhnAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
       String value,
       Char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Fact]
    public void LuhnAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
    {
        // Arrange.
        var value = "00000";
        var expectedCheckDigit = CharConstants.DigitZero;

        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("12G45")]      // Value 12345 would have check digit = 5. G is 20 positions later in ASCII table than 3 so test will fail unless code explicitly checks for non-digit
    [InlineData("12)45")]      // ) is 10 positions earlier in ASCII table than 3 so test will fail unless code explicitly checks for non-digit
    public void LuhnAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
    {
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
    public void LuhnAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
       => _sut.Validate(null!).Should().BeFalse();

    [Fact]
    public void LuhnAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
       => _sut.Validate(String.Empty).Should().BeFalse();

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    public void LuhnAlgorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
       => _sut.Validate(value).Should().BeFalse();

    [Theory]
    [InlineData("0000000018")]
    [InlineData("0000001008")]
    [InlineData("0000100008")]
    [InlineData("0010000008")]
    [InlineData("1000000008")]
    public void LuhnAlgorithm_Validate_ShouldCorrectlyWeightOddPositionCharacters(String value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("0000000109")]
    [InlineData("0000010009")]
    [InlineData("0001000009")]
    [InlineData("0100000009")]
    public void LuhnAlgorithm_Validate_ShouldCorrectlyWeightEvenPositionCharacters(String value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("00")]
    [InlineData("18")]
    [InlineData("26")]
    [InlineData("34")]
    [InlineData("42")]
    [InlineData("59")]
    [InlineData("67")]
    [InlineData("75")]
    [InlineData("83")]
    [InlineData("91")]
    public void LuhnAlgorithm_Validate_ShouldCalculateCorrectDoubleForOddPositionCharacters(String value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("378282246310005")]     // American Express test credit card number
    [InlineData("6011111111111117")]    // Discover test credit card number
    [InlineData("5555555555554444")]    // MasterCard test credit card number
    [InlineData("4012888888881881")]    // Visa test credit card number
    [InlineData("3056930009020004")]    // Diners Club test credit card number
    [InlineData("3566111111111113")]    // JCB test credit card number
    [InlineData("808401234567893")]     // NPI (National Provider Identifier), including 80840 prefix
    [InlineData("490154203237518")]     // IMEI (International Mobile Equipment Identity)
    [InlineData("293443438")]           // Canadian Social Insurance Number from https://www.ibm.com/docs/en/sga?topic=patterns-canada-social-insurance-number
    [InlineData("511700957")]           // "
    public void LuhnAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("3056930090020004")]    // Diners Club test card number with two digit transposition 09 -> 90
    [InlineData("3056930000920004")]    // Diners Club test card number with two digit transposition 90 -> 09
    [InlineData("5555555225554444")]    // MasterCard test card number with two digit twin error 55 -> 22
    [InlineData("5555555225554774")]    // MasterCard test card number with two digit twin error 44 -> 77
    [InlineData("3533111111111113")]    // JCB test card number with two digit twin error 66 -> 33
    public void LuhnAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(String value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("5558555555554444")]    // MasterCard test card number with single digit transcription error 5 -> 8
    [InlineData("5558555555554434")]    // MasterCard test card number with single digit transcription error 4 -> 3
    [InlineData("3059630009020004")]    // Diners Club test card number with two digit transposition error 69 -> 96 
    [InlineData("3056930009002004")]    // Diners Club test card number with two digit transposition error 20 -> 02
    [InlineData("5559955555554444")]    // MasterCard test card number with two digit twin error 55 -> 99
    [InlineData("3566111144111113")]    // JCB test card number with two digit twin error 11 -> 44
    public void LuhnAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
       => _sut.Validate(value).Should().BeFalse();

    [Fact]
    public void LuhnAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
       => _sut.Validate("0000000000000000").Should().BeTrue();

    [Fact]
    public void LuhnAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
       => _sut.Validate("7624810").Should().BeTrue();

    [Theory]
    [InlineData("12G455")]     // Value 12345 would have check digit = 5. G is 20 positions later in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
    [InlineData("12)455")]     // ) is 10 positions earlier in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
    public void LuhnAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
       => _sut.Validate(value).Should().BeFalse();

   #endregion
}
