// Ignore Spelling: Damm

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class DammAlgorithmTests
{
    private readonly DammAlgorithm _sut = new();

    #region AlgorithmDescription Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void DammAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
       => _sut.AlgorithmDescription.Should().Be(Resources.DammAlgorithmDescription);

    #endregion

    #region AlgorithmName Property Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void DammAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
       => _sut.AlgorithmName.Should().Be(Resources.DammAlgorithmName);

    #endregion

    #region TryCalculateCheckDigit Tests
    // ==========================================================================
    // ==========================================================================

    [Fact]
    public void DammAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Fact]
    public void DammAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(string.Empty, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

    [Theory]
    [InlineData("572", '4')]                      // Worked example from Wikipedia
    [InlineData("11294", '6')]                    // Test data from https://www.rosettacode.org/wiki/Damm_algorithm#C#
    [InlineData("12345678901", '8')]              // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
    [InlineData("123456789012345", '0')]          // "
    [InlineData("11223344556677889900", '6')]     // "
    public void DammAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
       string value,
       char expectedCheckDigit)
    {
        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Fact]
    public void DamnAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
    {
        // Arrange.
        var value = "00000";
        var expectedCheckDigit = CharConstants.DigitZero;

        // Act/assert.
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
        checkDigit.Should().Be(expectedCheckDigit);
    }

    [Theory]
    [InlineData("12G45")]
    [InlineData("12)45")]
    public void DammAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(string value)
    {
        _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
        checkDigit.Should().Be('\0');
    }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
    public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
       => _sut.Validate(null!).Should().BeFalse();

    [Fact]
    public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
       => _sut.Validate(string.Empty).Should().BeFalse();

    [Theory]
    [InlineData("0")]    // "0" is the only digit that would pass unless length is checked explicitly
    [InlineData("1")]
    public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputIsLengthOne(string value)
       => _sut.Validate(value).Should().BeFalse();

    [Theory]
    [InlineData("5724")]                      // Worked example from Wikipedia
    [InlineData("112946")]                    // Test data from https://www.rosettacode.org/wiki/Damm_algorithm#C#
    [InlineData("123456789018")]              // Value calculated by https://jackanderson.me/2020/09/damm-algorithm-check-digit-tool/
    [InlineData("1234567890123450")]          // "
    [InlineData("112233445566778899006")]     // "
    public void DammAlgorithm_Validate_ShouldReturnTrue_WhenInputContainsValidCheckDigit(string value)
          => _sut.Validate(value).Should().BeTrue();

    [Fact]
    public void DammAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
       => _sut.Validate("0000000000000000").Should().BeTrue();

    [Theory]
    [InlineData("112233445566778899016")]     // Single digit errors (using "112233445566778899006" as a valid value)
    [InlineData("112233445566778892006")]     // "
    [InlineData("112233445566778399006")]     // "
    [InlineData("112233445566748899006")]     // "
    [InlineData("112233445565778899006")]     // "
    [InlineData("112233445666778899006")]     // "
    [InlineData("112233475566778899006")]     // "
    [InlineData("112238445566778899006")]     // "
    [InlineData("112933445566778899006")]     // "
    [InlineData("102233445566778899006")]     // "
    [InlineData("121233445566778899006")]     // Transposition errors (using "112233445566778899006" as a valid value)
    [InlineData("112323445566778899006")]     // "
    [InlineData("112234345566778899006")]     // "
    [InlineData("112233454566778899006")]     // "
    [InlineData("112233445656778899006")]     // "
    [InlineData("112233445567678899006")]     // "
    [InlineData("112233445566787899006")]     // "
    [InlineData("112233445566778989006")]     // "
    [InlineData("112233445566778890906")]     // "
    [InlineData("1236547890123450")]          // Jump transposition error using "1234567890123450" as a valid value (456 -> 654)
    [InlineData("112255445566778899006")]     // Twin error using "112233445566778899006" as a valid value (33 -> 55)
    public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(string value)
       => _sut.Validate(value).Should().BeFalse();

    [Theory]
    [InlineData("12G455")]
    [InlineData("12)455")]
    public void DammAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(string value)
       => _sut.Validate(value).Should().BeFalse();

   #endregion
}
