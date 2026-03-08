namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class Modulus10_2AlgorithmTests
{
   private readonly Modulus10_2Algorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _imoNumberMask = new ImoNumberCheckDigitMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

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
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldReturnTrue_WhenInputHasLengthExactlyEqualNine()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("000000001", out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be('2');
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
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("907472", '9')]       // Worked example of IMO Number from Wikipedia https://en.wikipedia.org/wiki/IMO_number
   [InlineData("970779", '2')]       // IMO Number from https://www.marinetraffic.com/en/ais/details/ships/shipid:155821/mmsi:219018788/imo:9707792/vessel:CARRIER#:~:text=CARRIER%20(IMO%3A%209707792)%20is,under%20the%20flag%20of%20Denmark.
   [InlineData("101056", '9')]       // IMO Number from Wikipedia https://commons.wikimedia.org/wiki/Category:Ships_by_IMO_number
   public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
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
      var expectedCheckDigit = Chars.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]                      // Modulus 10 means that non-digit characters that are a multiple of 10 positions away
                                 // from a digit character in the ASCII table could result in the same check digit value
                                 // unless non-digit characters are explicitly rejected by the code.
   [InlineData("9D7472")]        // D is 20 positions later in ASCII table than 0
   [InlineData("9&7472")]        // & is 10 positions earlier in ASCII table than 0
   [InlineData("9#7472")]        // # is not a multiple of 10 positions away in ASCII table than 0, but is still a non-digit character that should be rejected
   public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("140")]
   [InlineData("140662")]
   [InlineData("140662538")]
   public void Modulus10_2Algorithm_TryCalculateCheckDigit_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Zero would return true unless length is explicitly checked.
   [InlineData("1")]
   public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTwo(String value)
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
   public void Modulus10_2Algorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("9074729")]       // Worked example of IMO Number from Wikipedia https://en.wikipedia.org/wiki/IMO_number
   [InlineData("9707792")]       // IMO Number from https://www.marinetraffic.com/en/ais/details/ships/shipid:155821/mmsi:219018788/imo:9707792/vessel:CARRIER#:~:text=CARRIER%20(IMO%3A%209707792)%20is,under%20the%20flag%20of%20Denmark.
   [InlineData("1010569")]       // IMO Number from Wikipedia https://commons.wikimedia.org/wiki/Category:Ships_by_IMO_number
   public void Modulus10_2Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1020569")]       // IMO Number 1010569 with single digit transcription error 1 -> 2
   [InlineData("9704729")]       // IMO Number 9074729 with two digit transposition error 07 -> 70
   [InlineData("9470729")]       // IMO Number 9074729 with jump transposition 074 -> 470
   [InlineData("9706692")]       // IMO Number 9707792 with twin error 77 -> 66
   public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus10_2Algorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("0000000000").Should().BeTrue();

   [Fact]
   public void Modulus10_2Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("1010480").Should().BeTrue();

   [Theory]                      // Modulus 10 means that non-digit characters that are a multiple of 10 positions away
                                 // from a digit character in the ASCII table could result in the same check digit value
                                 // unless non-digit characters are explicitly rejected by the code.
   [InlineData("9D74729")]       // D is 20 positions later in ASCII table than 0
   [InlineData("9&74729")]       // & is 10 positions earlier in ASCII table than 0
   [InlineData("9#74729")]       // # is not a multiple of 10 positions away in ASCII table than 0, but is still a non-digit character that should be rejected
   public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus10_2Algorithm_Validate_ShouldReturnFalse_WhenCheckDigitIsNonDigitCharacter()
      => _sut.Validate("000000001X").Should().BeFalse();    // Actual check digit would be 2

   [Theory]
   [InlineData("1406")]
   [InlineData("1406627")]
   [InlineData("1406625389")]
   public void Modulus10_2Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
      => _sut
         .Invoking(x => x.Validate("12345", null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mask")
         .WithMessage(Resources.NullMaskMessage + "*");

   [Fact]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("000 000 000 0", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Zero would return true unless length is explicitly checked.
   [InlineData("1")]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
   {
      // Requires at least one unmasked character plus the check digit character.
      // This should be rejected even if check digit would otherwise be valid.
      _sut.Validate(value, _acceptAllMask).Should().BeFalse();
   }

   [Fact]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnTrue_WhenInputHasExactly9UnmaskedDigits()
   {
      // Nine unmasked digits plus check digit = 10 total, which is the maximum
      // allowed for this algorithm. This should be accepted if check digit is valid.
      _sut.Validate("0000000012", _acceptAllMask).Should().BeTrue();
   }

   [Fact]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputHasMoreThan9UnmaskedDigits()
   {
      // Exceeds maximum: 10 unmasked digits + check digit = 11 total
      // This should be rejected even if check digit would otherwise be valid
      _sut.Validate("00000000012", _acceptAllMask).Should().BeFalse();
   }

   [Theory]
   [InlineData("000 000 001 2")]
   [InlineData("000 000 010 3")]
   [InlineData("000 000 100 4")]
   [InlineData("000 001 000 5")]
   [InlineData("000 010 000 6")]
   [InlineData("000 100 000 7")]
   [InlineData("001 000 000 8")]
   [InlineData("010 000 000 9")]
   [InlineData("100 000 000 0")]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("IMO9074729")]       // Worked example of IMO Number from Wikipedia https://en.wikipedia.org/wiki/IMO_number
   [InlineData("IMO9707792")]       // IMO Number from https://www.marinetraffic.com/en/ais/details/ships/shipid:155821/mmsi:219018788/imo:9707792/vessel:CARRIER#:~:text=CARRIER%20(IMO%3A%209707792)%20is,under%20the%20flag%20of%20Denmark.
   [InlineData("IMO1010569")]       // IMO Number from Wikipedia https://commons.wikimedia.org/wiki/Category:Ships_by_IMO_number
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _imoNumberMask).Should().BeTrue();

   [Theory]
   [InlineData("9074729")]       // Worked example of IMO Number from Wikipedia https://en.wikipedia.org/wiki/IMO_number
   [InlineData("9707792")]       // IMO Number from https://www.marinetraffic.com/en/ais/details/ships/shipid:155821/mmsi:219018788/imo:9707792/vessel:CARRIER#:~:text=CARRIER%20(IMO%3A%209707792)%20is,under%20the%20flag%20of%20Denmark.
   [InlineData("1010569")]       // IMO Number from Wikipedia https://commons.wikimedia.org/wiki/Category:Ships_by_IMO_number
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]
   [InlineData("IMO1020569")]       // IMO Number 1010569 with single digit transcription error 1 -> 2
   [InlineData("IMO9704729")]       // IMO Number 9074729 with two digit transposition error 07 -> 70
   [InlineData("IMO9470729")]       // IMO Number 9074729 with jump transposition 074 -> 470
   [InlineData("IMO9706692")]       // IMO Number 9707792 with twin error 77 -> 66
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _imoNumberMask).Should().BeFalse();

   [Fact]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000 000 000 0", _groupsOfThreeMask).Should().BeTrue();

   [Fact]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("101 048 0", _groupsOfThreeMask).Should().BeTrue();

   [Theory]                      // Modulus 10 means that non-digit characters that are a multiple of 10 positions away
                                 // from a digit character in the ASCII table could result in the same check digit value
                                 // unless non-digit characters are explicitly rejected by the code.
   [InlineData("9D7 472 9")]     // D is 20 positions later in ASCII table than 0
   [InlineData("9&7 472 9")]     // & is 10 positions earlier in ASCII table than 0
   [InlineData("9#7 472 9")]     // # is not a multiple of 10 positions away in ASCII table than 0, but is still a non-digit character that should be rejected
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnFalse_WhenCheckDigitIsNonDigitCharacter()
      => _sut.Validate("000 000 001 X", _groupsOfThreeMask).Should().BeFalse();    // Actual check digit would be 2

   [Theory]
   [InlineData("140 6")]
   [InlineData("140 662 7")]
   [InlineData("140 662 538 9")]
   public void Modulus10_2Algorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
