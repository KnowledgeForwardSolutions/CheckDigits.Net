namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod11_2AlgorithmTests
{
   private readonly Iso7064Mod11_2Algorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

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
   [InlineData("0", '1')]
   [InlineData("1", 'X')]
   [InlineData("2", '8')]
   [InlineData("3", '6')]
   [InlineData("4", '4')]
   [InlineData("5", '2')]
   [InlineData("6", '0')]
   [InlineData("7", '9')]
   [InlineData("8", '7')]
   [InlineData("9", '5')]
   public void Iso7064Mod11_2Algorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
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
   public void Iso7064Mod11_2Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("123X5")]
   [InlineData("123x5")]
   [InlineData("123D56")]
   [InlineData("123!56")]
   [InlineData("123^56")]
   public void Iso7064Mod11_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("140")]
   [InlineData("140662")]
   [InlineData("140662538")]
   [InlineData("140662538042")]
   [InlineData("140662538042551")]
   [InlineData("140662538042551028")]
   [InlineData("140662538042551028265")]
   public void Iso7064Mod11_2Algorithm_TryCalculateCheckDigit_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();

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
   [InlineData("01")]
   [InlineData("1X")]
   [InlineData("28")]
   [InlineData("36")]
   [InlineData("44")]
   [InlineData("52")]
   [InlineData("60")]
   [InlineData("79")]
   [InlineData("87")]
   [InlineData("95")]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

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
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitCalculatesAsX()
      => _sut.Validate("079X").Should().BeTrue();

   [Fact]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsXOtherThanTrailingPosition()
      => _sut.Validate("123X56").Should().BeFalse();

   [Theory]
   [InlineData("123D56X")]
   [InlineData("123x56X")]
   [InlineData("123!56X")]
   [InlineData("123^56X")]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("12345Q")]
   [InlineData("12345x")]
   [InlineData("12345!")]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("140X")]
   [InlineData("1406628")]
   [InlineData("1406625380")]
   [InlineData("1406625380426")]
   [InlineData("1406625380425511")]
   [InlineData("140662538042551028X")]
   [InlineData("1406625380425510282651")]
   public void Iso7064Mod11_2Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
      => _sut
         .Invoking(x => x.Validate("07940", null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mask")
         .WithMessage(Resources.NullMaskMessage + "*");

   [Fact]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]          // "1" will fail if length check is not performed
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("07940", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("01")]
   [InlineData("1X")]
   [InlineData("28")]
   [InlineData("36")]
   [InlineData("44")]
   [InlineData("52")]
   [InlineData("60")]
   [InlineData("79")]
   [InlineData("87")]
   [InlineData("95")]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("079 X")]                  // Example from ISO/IEC 7064 specification
   [InlineData("079 40")]                 // Example from ISO/IEC 7064 specification
   [InlineData("000 000 012 146 438 X")]  // Example ISNI from Wikipedia https://en.wikipedia.org/wiki/International_Standard_Name_Identifier
   [InlineData("000 000 007 366 914 4")]  // ISNI for Richard, Zachary from https://isni.org/page/search-database/
   [InlineData("000 000 012 095 650 X")]  // ISNI for Lucas, George from https://isni.org/page/search-database/
   [InlineData("000 000 010 930 246 8")]  // ISNI for Roddenberry, Gene from https://isni.org/page/search-database/
   [InlineData("000 000 005 887 031 7")]  // ISNI for Barrett, Majel from https://isni.org/page/search-database/
   [InlineData("000 000 011 476 741 1")]  // ISNI for Wheaton, Wil from https://isni.org/page/search-database/
   [InlineData("123 458")]
   [InlineData("123 456 789 X")]
   [InlineData("123 456 789 123 45X")]
   [InlineData("123 456 789 123 456 789 9")]
   [InlineData("999 999 999 999 999 999 999 999 999 99")]
   [InlineData("999 999 999 999 999 999 999 999 999 999 999 994")]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

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
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]
   [InlineData("000 000 012 156 438 X")]  // 000000012146438X with single digit transcription error 4 -> 5
   [InlineData("000 000 007 366 913 4")]  // 0000000073669144 with single digit transcription error 4 -> 3
   [InlineData("000 000 007 369 614 4")]  // 0000000073669144 with two digit transposition error 69 -> 96 
   [InlineData("000 000 010 932 046 8")]  // 0000000109302468 with two digit transposition error 02 -> 20
   [InlineData("000 000 005 997 031 7")]  // 0000000058870317 with two digit twin error 88 -> 99
   [InlineData("000 000 044 476 741 1")]  // 0000000114767411 two digit twin error 11 -> 44
   [InlineData("000 000 090 130 246 8")]  // 0000000109302468 with jump transposition error 109 -> 901
   [InlineData("000 000 073 669 144 0")]  // 0000000073669144 with circular shift error
   [InlineData("400 000 000 736 691 4")]  // 0000000073669144 with circular shift error
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnTrue_WhenCheckDigitCalculatesAsX()
      => _sut.Validate("079 X", _groupsOfThreeMask).Should().BeTrue();

   [Fact]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsXOtherThanTrailingPosition()
      => _sut.Validate("123 X56", _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("123 D56 X")]
   [InlineData("123 x56 X")]
   [InlineData("123 !56 X")]
   [InlineData("123 ^56 X")]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("123 45Q")]
   [InlineData("123 45x")]
   [InlineData("123 45!")]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCheckDigitCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("140 X")]
   [InlineData("140 662 8")]
   [InlineData("140 662 538 0")]
   [InlineData("140 662 538 042 6")]
   [InlineData("140 662 538 042 551 1")]
   [InlineData("140 662 538 042 551 028 X")]
   [InlineData("140 662 538 042 551 028 265 1")]
   public void Iso7064Mod11_2Algorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
