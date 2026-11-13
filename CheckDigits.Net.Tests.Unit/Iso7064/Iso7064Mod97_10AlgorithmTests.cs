namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod97_10AlgorithmTests
{
   private readonly Iso7064Mod97_10Algorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod97_10Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso7064Mod97_10AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod97_10Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso7064Mod97_10AlgorithmName);

   #endregion

   #region TryCalculateCheckDigits Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(null!, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(String.Empty, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("0", '9', '8')]
   [InlineData("1", '9', '5')]
   [InlineData("2", '9', '2')]
   [InlineData("3", '8', '9')]
   [InlineData("4", '8', '6')]
   [InlineData("5", '8', '3')]
   [InlineData("6", '8', '0')]
   [InlineData("7", '7', '7')]
   [InlineData("8", '7', '4')]
   [InlineData("9", '7', '1')]
   public void Iso7064Mod97_10Algorithm_TryCalculateCheckDigits_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("123456", '7', '6')]
   [InlineData("1632175818351910", '3', '8')]
   [InlineData("10113393912554329261011442299914333", '3', '8')]    // Example from https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#e7e616a4bd15acce7589cbedc4fd01fcc9623f60e4263be834c9e438
   public void Iso7064Mod97_10AlgorithmAlgorithm_TryCalculateCheckDigits_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("!234")]
   [InlineData("1^34")]
   [InlineData("12A4")]
   [InlineData("123a")]
   public void Iso7064Mod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("140")]
   [InlineData("140662")]
   [InlineData("140662538")]
   [InlineData("140662538042")]
   [InlineData("140662538042551")]
   [InlineData("140662538042551028")]
   [InlineData("140662538042551028265")]
   public void Iso7064Mod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigits(value, out _, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   [InlineData("00")]
   [InlineData("01")]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsLessThanTwoCharactersInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("098")]
   [InlineData("195")]
   [InlineData("292")]
   [InlineData("389")]
   [InlineData("486")]
   [InlineData("583")]
   [InlineData("680")]
   [InlineData("777")]
   [InlineData("874")]
   [InlineData("971")]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("12345676")]
   [InlineData("163217581835191038")]
   [InlineData("1011339391255432926101144229991433338")]    // Example from https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#e7e616a4bd15acce7589cbedc4fd01fcc9623f60e4263be834c9e438
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("163217541835191038")]     // 163217581835191038 with single char transcription error 8 -> 4
   [InlineData("163217581835191138")]     // 163217581835191038 with single char transcription error 0 -> 1
   [InlineData("12455676")]               // 12345676 with two char transposition error 34 -> 45 
   [InlineData("1022339391255432926101144229991433338")] // 1011339391255432926101144229991433338 with two char twin error 11 -> 22
   [InlineData("12365476")]               // 12345676 with jump transposition error 456 -> 654
   [InlineData("163217581538191038")]     // 163217581835191038 with jump transposition error 835 -> 538
   [InlineData("632175818351910381")]     // 163217581835191038 with circular shift error
   [InlineData("816321758183519103")]     // 163217581835191038 with circular shift error
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("!23456")]
   [InlineData("1^3456")]
   [InlineData("12A456")]
   [InlineData("123a56")]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("1234!6")]
   [InlineData("1234^6")]
   [InlineData("1234A6")]
   [InlineData("1234a6")]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("12345!")]
   [InlineData("12345^")]
   [InlineData("12345A")]
   [InlineData("12345a")]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("14066")]
   [InlineData("14066262")]
   [InlineData("14066253823")]
   [InlineData("14066253804250")]
   [InlineData("14066253804255112")]
   [InlineData("14066253804255102853")]
   [InlineData("14066253804255102826587")]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
      => _sut
         .Invoking(x => x.Validate("07945", null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mask")
         .WithMessage(Resources.NullMaskMessage + "*");

   [Fact]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   [InlineData("00")]
   [InlineData("01")]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("12345676", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("098")]
   [InlineData("195")]
   [InlineData("292")]
   [InlineData("389")]
   [InlineData("486")]
   [InlineData("583")]
   [InlineData("680")]
   [InlineData("777")]
   [InlineData("874")]
   [InlineData("971")]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("123 456 76")]
   [InlineData("163 217 581 835 191 038")]
   [InlineData("101 133 939 125 543 292 610 114 422 999 143 3338")]  // Example from https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#e7e616a4bd15acce7589cbedc4fd01fcc9623f60e4263be834c9e438
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("12345676")]
   [InlineData("163217581835191038")]
   [InlineData("1011339391255432926101144229991433338")]    // Example from https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#e7e616a4bd15acce7589cbedc4fd01fcc9623f60e4263be834c9e438
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]
   [InlineData("163 217 541 835 191 038")]   // 163217581835191038 with single char transcription error 8 -> 4
   [InlineData("163 217 581 835 191 138")]   // 163217581835191038 with single char transcription error 0 -> 1
   [InlineData("124 556 76")]                // 12345676 with two char transposition error 34 -> 45 
   [InlineData("102 233 939 125 543 292 610 114 422 999 143 3338")]  // 1011339391255432926101144229991433338 with two char twin error 11 -> 22
   [InlineData("123 654 76")]                // 12345676 with jump transposition error 456 -> 654
   [InlineData("163 217 581 538 191 038")]   // 163217581835191038 with jump transposition error 835 -> 538
   [InlineData("632 175 818 351 910 381")]   // 163217581835191038 with circular shift error
   [InlineData("816 321 758 183 519 103")]   // 163217581835191038 with circular shift error
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("!23 456")]
   [InlineData("1^3 456")]
   [InlineData("12A 456")]
   [InlineData("123 a56")]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("123 4!6")]
   [InlineData("123 4^6")]
   [InlineData("123 4A6")]
   [InlineData("123 4a6")]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("123 45!")]
   [InlineData("123 45^")]
   [InlineData("123 45A")]
   [InlineData("123 45a")]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("140 66")]
   [InlineData("140 662 62")]
   [InlineData("140 662 538 23")]
   [InlineData("140 662 538 042 50")]
   [InlineData("140 662 538 042 551 12")]
   [InlineData("140 662 538 042 551 028 53")]
   [InlineData("140 662 538 042 551 028 265 87")]
   public void Iso7064Mod97_10Algorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
