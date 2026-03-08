namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class Modulus10_1AlgorithmTests
{
   private readonly Modulus10_1Algorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

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
   public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldReturnTrue_WhenInputHasLengthExactlyEqualNine()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("000000001", out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be('1');
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
      var expectedCheckDigit = Chars.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]                      // Modulus 10 means that non-digit characters that are a multiple of 10 positions away
                                 // from a digit character in the ASCII table could result in the same check digit value
                                 // unless non-digit characters are explicitly rejected by the code.
   [InlineData("58D8")]          // D is 20 positions later in ASCII table than 0
   [InlineData("58&8")]          // & is 10 positions earlier in ASCII table than 0
   [InlineData("58#8")]          // # is not a multiple of 10 positions away in ASCII table than 0, but is still a non-digit character that should be rejected
   public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("140")]
   [InlineData("140662")]
   [InlineData("140662538")]
   public void Modulus10_1Algorithm_TryCalculateCheckDigit_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();

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

   [Theory]                      // Modulus 10 means that non-digit characters that are a multiple of 10 positions away
                                 // from a digit character in the ASCII table could result in the same check digit value
                                 // unless non-digit characters are explicitly rejected by the code.
   [InlineData("58D82")]         // D is 20 positions later in ASCII table than 0
   [InlineData("58&82")]         // & is 10 positions earlier in ASCII table than 0
   [InlineData("58#82")]         // # is not a multiple of 10 positions away in ASCII table than 0, but is still a non-digit character that should be rejected
   public void Modulus10_1Algorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus10_1Algorithm_Validate_ShouldReturnFalse_WhenCheckDigitIsNonDigitCharacter()
      => _sut.Validate("000000001X").Should().BeFalse();    // Actual check digit would be 1

   [Theory]
   [InlineData("1401")]
   [InlineData("1406628")]
   [InlineData("1406625384")]
   public void Modulus10_1Algorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldThrowArgumentNullException_WhenMaskIsNull()
      => _sut
         .Invoking(x => x.Validate("12345", null!))
         .Should()
         .ThrowExactly<ArgumentNullException>()
         .WithParameterName("mask")
         .WithMessage(Resources.NullMaskMessage + "*");

   [Fact]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("000 000 000 0", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Zero would return true unless length is explicitly checked.
   [InlineData("1")]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
   {
      // Requires at least one unmasked character plus the check digit character.
      // This should be rejected even if check digit would otherwise be valid.
      _sut.Validate(value, _acceptAllMask).Should().BeFalse();
   }

   [Fact]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnTrue_WhenInputHasExactly9UnmaskedDigits()
   {
      // Nine unmasked digits plus check digit = 10 total, which is the maximum
      // allowed for this algorithm. This should be accepted if check digit is valid.
      _sut.Validate("0000000011", _acceptAllMask).Should().BeTrue();
   }

   [Fact]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputHasMoreThan9UnmaskedDigits()
   {
      // Exceeds maximum: 10 unmasked digits + check digit = 11 total
      // This should be rejected even if check digit would otherwise be valid
      _sut.Validate("00000000011", _acceptAllMask).Should().BeFalse();
   }

   [Theory]
   [InlineData("000 000 001 1")]
   [InlineData("000 000 010 2")]
   [InlineData("000 000 100 3")]
   [InlineData("000 001 000 4")]
   [InlineData("000 010 000 5")]
   [InlineData("000 100 000 6")]
   [InlineData("001 000 000 7")]
   [InlineData("010 000 000 8")]
   [InlineData("100 000 000 9")]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("773 218 5")]     // Worked example of CAS Registry Number from Wikipedia https://en.wikipedia.org/wiki/CAS_Registry_Number
   [InlineData("580 82")]        // CAS Registry Number for caffeine
   [InlineData("287 285 54")]    // CAS Registry Number for Hexadimethrine bromide
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("7732185")]       // Worked example of CAS Registry Number from Wikipedia https://en.wikipedia.org/wiki/CAS_Registry_Number
   [InlineData("58082")]         // CAS Registry Number for caffeine
   [InlineData("28728554")]      // CAS Registry Number for Hexadimethrine bromide
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]
   [InlineData("774 218 5")]     // CAS Registry Number 7732185 with single digit transcription error 3 -> 4
   [InlineData("508 82")]        // CAS Registry Number 58082 with two digit transposition error 80 -> 08
   [InlineData("288 275 54")]    // CAS Registry Number 28728554 with jump transposition 728 -> 827
   [InlineData("663 218 5")]     // CAS Registry Number 7732185 with twin error 77 -> 66
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000 000 000 0", _groupsOfThreeMask).Should().BeTrue();

   [Fact]
   public void ModulusAlgorithm_ValidateMasked_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("100 000 001 0", _groupsOfThreeMask).Should().BeTrue();

   [Theory]                      // Modulus 10 means that non-digit characters that are a multiple of 10 positions away
                                 // from a digit character in the ASCII table could result in the same check digit value
                                 // unless non-digit characters are explicitly rejected by the code.
   [InlineData("58D 82")]        // D is 20 positions later in ASCII table than 0
   [InlineData("58& 82")]        // & is 10 positions earlier in ASCII table than 0
   [InlineData("58# 82")]        // # is not a multiple of 10 positions away in ASCII table than 0, but is still a non-digit character that should be rejected
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnFalse_WhenCheckDigitIsNonDigitCharacter()
      => _sut.Validate("000 000 001 X", _groupsOfThreeMask).Should().BeFalse();    // Actual check digit would be 1

   [Theory]
   [InlineData("140 1")]
   [InlineData("140 662 8")]
   [InlineData("140 662 538 4")]
   public void Modulus10_1Algorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
