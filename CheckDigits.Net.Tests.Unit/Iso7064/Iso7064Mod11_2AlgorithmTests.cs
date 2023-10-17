namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod11_2AlgorithmTests
{
   private readonly Iso7064Mod11_2Algorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod11_2Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso7064Mod11_2AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod11_2Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso7064Mod11_2AlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod11_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod11_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("079", 'X')]                // Example from ISO/IEC 7064 specification
   [InlineData("0794", '0')]               // Example from ISO/IEC 7064 specification
   [InlineData("000000012146438", 'X')]    // Example ISNI from Wikipedia https://en.wikipedia.org/wiki/International_Standard_Name_Identifier
   [InlineData("000000007366914", '4')]    // ISNI for Richard, Zachary from https://isni.org/page/search-database/
   [InlineData("000000012095650", 'X')]    // ISNI for Lucas, George from https://isni.org/page/search-database/
   [InlineData("000000010930246", '8')]    // ISNI for Roddenberry, Gene from https://isni.org/page/search-database/
   [InlineData("000000005887031", '7')]    // ISNI for Barrett, Majel from https://isni.org/page/search-database/
   [InlineData("000000011476741", '1')]    // ISNI for Wheaton, Wil from https://isni.org/page/search-database/
   [InlineData("12345", '8')]
   [InlineData("123456789", 'X')]
   [InlineData("12345678912345", 'X')]
   [InlineData("123456789123456789", '9')]
   [InlineData("9999999999999999999999999999", '9')]
   [InlineData("99999999999999999999999999999999999", '4')]
   public void Iso7064Mod11_2AlgorithmAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("123X5")]
   [InlineData("123D56")]
   [InlineData("123!56")]
   [InlineData("123^56")]
   public void Iso7064Mod11_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
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
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]          // "1" will fail if length check is not performed
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("079X")]                // Example from ISO/IEC 7064 specification
   [InlineData("07940")]               // Example from ISO/IEC 7064 specification
   [InlineData("000000012146438X")]    // Example ISNI from Wikipedia https://en.wikipedia.org/wiki/International_Standard_Name_Identifier
   [InlineData("0000000073669144")]    // ISNI for Richard, Zachary from https://isni.org/page/search-database/
   [InlineData("000000012095650X")]    // ISNI for Lucas, George from https://isni.org/page/search-database/
   [InlineData("0000000109302468")]    // ISNI for Roddenberry, Gene from https://isni.org/page/search-database/
   [InlineData("0000000058870317")]    // ISNI for Barrett, Majel from https://isni.org/page/search-database/
   [InlineData("0000000114767411")]    // ISNI for Wheaton, Wil from https://isni.org/page/search-database/
   [InlineData("123458")]
   [InlineData("123456789X")]
   [InlineData("12345678912345X")]
   [InlineData("1234567891234567899")]
   [InlineData("99999999999999999999999999999")]
   [InlineData("999999999999999999999999999999999994")]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000012156438X")]    // 000000012146438X with single digit transcription error 4 -> 5
   [InlineData("0000000073669134")]    // 0000000073669144 with single digit transcription error 4 -> 3
   [InlineData("0000000073696144")]    // 0000000073669144 with two digit transposition error 69 -> 96 
   [InlineData("0000000109320468")]    // 0000000109302468 with two digit transposition error 02 -> 20
   [InlineData("0000000059970317")]    // 0000000058870317 with two digit twin error 88 -> 99
   [InlineData("0000000444767411")]    // 0000000114767411 two digit twin error 11 -> 44
   [InlineData("0000000901302468")]    // 0000000109302468 with jump transposition error 109 -> 901
   [InlineData("0000000736691440")]    // 0000000073669144 with circular shift error
   [InlineData("4000000007366914")]    // 0000000073669144 with circular shift error
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsX()
      => _sut.Validate("079X").Should().BeTrue();

   [Fact]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsXOtherThanTrailingPosition()
      => _sut.Validate("123X56").Should().BeFalse();

   [Theory]
   [InlineData("123D56X")]
   [InlineData("123!56X")]
   [InlineData("123^56X")]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter()
      => _sut.Validate("12345Q").Should().BeFalse();

   #endregion
}
