namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod11_10AlgorithmTests
{
   private readonly Iso7064Mod11_10Algorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod11_10Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso7064Mod11_10AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod11_10Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso7064Mod11_10AlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod11_10Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod11_10Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("0", '2')]
   [InlineData("1", '9')]
   [InlineData("2", '7')]
   [InlineData("3", '5')]
   [InlineData("4", '3')]
   [InlineData("5", '1')]
   [InlineData("6", '0')]
   [InlineData("7", '8')]
   [InlineData("8", '6')]
   [InlineData("9", '4')]
   public void Iso7064Mod11_10Algorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("0794", '5')]           // Example from ISO 7064 specification
   [InlineData("12345678", '8')]
   [InlineData("11223344", '6')]
   [InlineData("1632175818351910", '3')]
   [InlineData("12345678901234567890123456", '5')]
   public void Iso7064Mod11_10AlgorithmAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("079H5")]
   [InlineData("079*5")]
   public void Iso7064Mod11_10Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod11_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso7064Mod11_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]          // "1" will fail if length check is not performed
   public void Iso7064Mod11_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("02")]
   [InlineData("19")]
   [InlineData("27")]
   [InlineData("35")]
   [InlineData("43")]
   [InlineData("51")]
   [InlineData("60")]
   [InlineData("78")]
   [InlineData("86")]
   [InlineData("94")]
   public void Iso7064Mod11_10Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("07945")]               // Example from ISO 7064 specification
   [InlineData("123456788")]
   [InlineData("112233446")]
   [InlineData("16321758183519103")]
   [InlineData("123456789012345678901234565")]
   public void Iso7064Mod11_10Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("123556788")]           // 123456788 with single digit transcription error 4 -> 5
   [InlineData("16341758183519103")]   // 16321758183519103 with single digit transcription error 2 -> 4
   [InlineData("123465788")]           // 123456788 with two digit transposition error 56 -> 65 
   [InlineData("16321578183519103")]   // 16321758183519103 with two digit transposition error 75 -> 57
   [InlineData("114433446")]           // 112233446 with two digit twin error 22 -> 44
   [InlineData("992233446")]           // 112233446 two digit twin error 11 -> 99
   [InlineData("16321758153819103")]   // 16321758183519103 with jump transposition error 835 -> 538
   [InlineData("31632175818351910")]   // 16321758183519103 with circular shift error
   [InlineData("63217581835191031")]   // 16321758183519103 with circular shift error
   // [InlineData("16325718183519103")]   // 16321758183519103 with jump transposition error 175 -> 571 not detected
   public void Iso7064Mod11_10Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod11_10Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsX()
      => _sut.Validate("60").Should().BeTrue();

   [Theory]
   [InlineData("079H5")]
   [InlineData("079*5")]
   public void Iso7064Mod11_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod11_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter()
      => _sut.Validate("12345Q").Should().BeFalse();

   #endregion
}
