// Ignore Spelling: Verhoeff

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class VerhoeffAlgorithmTests
{
    private readonly VerhoeffAlgorithm _sut = new();

    #region AlgorithmDescription Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void VerhoeffAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
       => _sut.AlgorithmDescription.Should().Be(Resources.VerhoeffAlgorithmDescription);

    #endregion

    #region AlgorithmName Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void VerhoeffAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
       => _sut.AlgorithmName.Should().Be(Resources.VerhoeffAlgorithmName);

    #endregion

    #region TryCalculateCheckDigit Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(string.Empty, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Theory]
    [InlineData("236", '3')]                      // Worked example from Wikipedia
    [InlineData("0", '4')]
    [InlineData("12345", '1')]                    // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
    [InlineData("75872", '2')]                    // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
    [InlineData("142857", '0')]                   // "
    public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
       string value,
       char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    // NOTE: algorithm applies mod 8 to the index when indexing into the permutation table
    [Theory]
    [InlineData("123456789012", '0')]             // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
    [InlineData("8473643095483728456789", '2')]   // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
    [InlineData("11223344556677889900", '9')]     // Value calculated by https://kik.amc.nl/home/rcornet/verhoeff.html
    public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenValueHasLengthGreaterThanEight(
       string value,
       char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("12G45")]
    [InlineData("12)45")]
    public void VerhoeffAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(string value)
    {
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    #endregion

    #region Validate Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
       => _sut.Validate(null!).Should().BeFalse();

    [Fact]
    public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
       => _sut.Validate(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData("0")]    // "0" is the only digit that would pass unless length is checked explicitly
    [InlineData("1")]
    public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputIsLengthOne(string value)
       => _sut.Validate(value).Should().BeFalse();

    [Theory]
    [InlineData("2363")]                      // Worked example from Wikipedia
    [InlineData("04")]
    [InlineData("123451")]                    // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
    [InlineData("758722")]                    // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
    [InlineData("1428570")]                   // "
    public void VerhoeffAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(string value)
       => _sut.Validate(value).Should().BeTrue();

    // NOTE: algorithm applies mod 8 to the index when indexing into the permutation table
    [Theory]
    [InlineData("1234567890120")]             // Test data from https://rosettacode.org/wiki/Verhoeff_algorithm
    [InlineData("84736430954837284567892")]   // Test data from https://codereview.stackexchange.com/questions/221229/verhoeff-check-digit-algorithm
    [InlineData("112233445566778899009")]     // Value calculated by https://kik.amc.nl/home/rcornet/verhoeff.html
    public void VerhoeffAlgorithm_Validate_ShouldReturnTrue_WhenValueWithLengthGreaterThanEightContainsValidCheckDigit(string value)
       => _sut.Validate(value).Should().BeTrue();

    [Theory]
    [InlineData("112233445566778899019")]     // Single digit errors (using "112233445566778899009" as a valid value)
    [InlineData("112233445566778892009")]     // "
    [InlineData("112233445566778399009")]     // "
    [InlineData("112233445566748899009")]     // "
    [InlineData("112233445565778899009")]     // "
    [InlineData("112233445666778899009")]     // "
    [InlineData("112233475566778899009")]     // "
    [InlineData("112238445566778899009")]     // "
    [InlineData("112933445566778899009")]     // "
    [InlineData("102233445566778899009")]     // "
    [InlineData("121233445566778899009")]     // Transposition errors (using "112233445566778899009" as a valid value)
    [InlineData("112323445566778899009")]     // "
    [InlineData("112234345566778899009")]     // "
    [InlineData("112233454566778899009")]     // "
    [InlineData("112233445656778899009")]     // "
    [InlineData("112233445567678899009")]     // "
    [InlineData("112233445566787899009")]     // "
    [InlineData("112233445566778989009")]     // "
    [InlineData("112233445566778890909")]     // "
    [InlineData("84736430459837284567892")]   // Jump transposition error using "84736430954837284567892" as a valid value (954 -> 459)
    [InlineData("112255445566778899009")]     // Twin error using "112233445566778899009" as a valid value (33 -> 55)
    public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(string value)
       => _sut.Validate(value).Should().BeFalse();

    [Theory]
    [InlineData("12G455")]
    [InlineData("12)455")]
    public void VerhoeffAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(string value)
       => _sut.Validate(value).Should().BeFalse();

    #endregion
}
