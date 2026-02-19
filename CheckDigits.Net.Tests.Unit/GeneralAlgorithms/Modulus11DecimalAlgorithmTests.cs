// Ignore Spelling: Nhs

namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class Modulus11DecimalAlgorithmTests
{
   private readonly Modulus11DecimalAlgorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11DecimalAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Modulus11DecimalAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11DecimalAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Modulus11DecimalAlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThanNine()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("1234567890", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("0", '0')]        // Sum = 0, mod = (11-(0%11))%11=0
   [InlineData("1", '9')]        // Sum = 2, mod = (11-(1%11))%11=9
   [InlineData("2", '7')]        // Sum = 4, mod = (11-(4%11))%11=7
   [InlineData("3", '5')]        // Sum = 6, mod = (11-(6%11))%11=5
   [InlineData("4", '3')]        // Sum = 8, mod = (11-(8%11))%11=3
   [InlineData("5", '1')]        // Sum = 10, mod = (11-(10%11))%11=1
   //[InlineData("6", 'X')]        // Sum = 12, mod = (11-(12%11))%11=X, X is not a valid check digit for this algorithm so test case commented out and would be expected to return false if included
   [InlineData("7", '8')]        // Sum = 14, mod = (11-(14%11))%11=8
   [InlineData("8", '6')]        // Sum = 16, mod = (11-(16%11))%11=6
   [InlineData("9", '4')]        // Sum = 18, mod = (11-(18%11))%11=4
   [InlineData("61", '2')]        // Sum = 20, mod = (11-(20%11))%11=2
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnAllPossibleValidCheckDigits(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("000000001", '9')]
   [InlineData("000000010", '8')]
   [InlineData("000000100", '7')]
   [InlineData("000001000", '6')]
   [InlineData("000010000", '5')]
   [InlineData("000100000", '4')]
   [InlineData("001000000", '3')]
   [InlineData("010000000", '2')]
   [InlineData("100000000", '1')]
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightCharactersByPosition(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   // Note ISBN and ISSN test cases exclude values that would result in check digit = 'X'
   [InlineData("156865652", '1')]   // ISBN-10 Island in the Stream of Time, S. M. Sterling
   [InlineData("044100560", '8')]   // ISBN-10 The Warlock in Spite of Himself, Christopher Stasheff
   [InlineData("071410544", '9')]   // ISBN-10 The Sutton Hoo Ship Burial, Angela Care Evans
   //
   [InlineData("030640615", '2')]   // Worked example of ISBN-10 from Wikipedia https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
   [InlineData("0378595", '5')]     // Worked example of ISSN from Wikipedia https://en.wikipedia.org/wiki/ISSN
   [InlineData("0317847", '1')]     // Example ISSN from https://www.issn.org/understanding-the-issn/what-is-an-issn/
   //
   [InlineData("530119491", '7')]   // Random NHS number from http://danielbayley.uk/nhs-number/
   [InlineData("851446824", '3')]   // "
   [InlineData("396748788", '1')]   // "
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "00000";
      var expectedCheckDigit = Chars.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("100G00001")]
   [InlineData("100+00001")]
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   // ISBN and ISSN test cases that would result in a check digit = 'X'
   [InlineData("050027293")]        // ISBN-10 Roman London, Peter Marsden
   //
   [InlineData("2434561")]          // Example ISSN from Wikipedia
   [InlineData("1050124")]          // Example ISSN from https://www.issn.org/understanding-the-issn/what-is-an-issn/
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenModulus11HasRemainderOf10(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("140")]
   [InlineData("140662")]
   [InlineData("140662538")]
   public void Modulus11DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Zero would return true unless length is explicitly checked.
   [InlineData("1")]
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTwo(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThan10()
      => _sut.Validate("00000000000").Should().BeFalse();

   [Theory]
   [InlineData("0000000019")]
   [InlineData("0000000108")]
   [InlineData("0000001007")]
   [InlineData("0000010006")]
   [InlineData("0000100005")]
   [InlineData("0001000004")]
   [InlineData("0010000003")]
   [InlineData("0100000002")]
   [InlineData("1000000001")]
   public void Modulus11DecimalAlgorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1568656521")]    // ISBN-10 Island in the Stream of Time, S. M. Sterling
   [InlineData("0441005608")]    // ISBN-10 The Warlock in Spite of Himself, Christopher Stasheff
   [InlineData("0714105449")]    // ISBN-10 The Sutton Hoo Ship Burial, Angela Care Evans
   //
   [InlineData("0306406152")]    // Worked example of ISBN-10 from Wikipedia https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
   [InlineData("03785955")]      // Worked example of ISSN from Wikipedia https://en.wikipedia.org/wiki/ISSN
   [InlineData("03178471")]      // Example ISSN from https://www.issn.org/understanding-the-issn/what-is-an-issn/
   //
   [InlineData("9434765919")]    // Worked example from Wikipedia https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters
   [InlineData("4505577104")]    // Example from https://www.clatterbridgecc.nhs.uk/patients/general-information/nhs-number#:~:text=Your%20NHS%20Number%20is%20printed,is%20an%20example%20number%20only).
   [InlineData("5301194917")]    // Random NHS number from http://danielbayley.uk/nhs-number/
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1568646521")]    // ISBN-10 with single digit transcription error 5 -> 4
   [InlineData("0441050608")]    // ISBN-10 with two digit transposition error 05 -> 50
   [InlineData("3946787881")]    // Valid NHS number (9876544321) with jump transposition 674 -> 467
   [InlineData("8515568243")]    // Valid NHS number (8514468243) with twin error 44 -> 55
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("0000000000").Should().BeTrue();

   [Theory]
   // ISBN and ISSN test cases that would result in a check digit = 'X', replaced with '0'
   [InlineData("0500272930")]        // ISBN-10 Roman London, Peter Marsden
   //
   [InlineData("24345610")]          // Example ISSN from Wikipedia
   [InlineData("10501240")]          // Example ISSN from https://www.issn.org/understanding-the-issn/what-is-an-issn/
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnFalse_WhenModulus11HasRemainderOf10(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("1000G00005")]    // Value 1000300005 would have a check digit = 5. G is 20 positions later in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
   [InlineData("1000)00005")]    // ) is 10 positions earlier in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnFalse_WhenCheckDigitIsNonDigitCharacter()
      => _sut.Validate("100030000?").Should().BeFalse();    // Actual check digit would be 5

   [Theory]
   [InlineData("1406")]
   [InlineData("1406620")]
   [InlineData("1406625388")]
   public void Modulus11DecimalAlgorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("9434765919")]    // Worked example from Wikipedia https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters
   [InlineData("4505577104")]    // Example from https://www.clatterbridgecc.nhs.uk/patients/general-information/nhs-number#:~:text=Your%20NHS%20Number%20is%20printed,is%20an%20example%20number%20only).
   [InlineData("5301194917")]    // Random NHS number from http://danielbayley.uk/nhs-number/
   //
   [InlineData("9434764919")]    // Valid NHS number (9434765919) with single digit transcription error 5 -> 4
   [InlineData("4550577104")]    // Valid NHS number (4505577104) with two digit transposition error 05 -> 50
   [InlineData("3946787881")]    // Valid NHS number (9876544321) with jump transposition 674 -> 467
   [InlineData("8515568243")]    // Valid NHS number (8514468243) with twin error 44 -> 55
   public void Modulus11ExtendedAlgorithm_Validate_ShouldProduceSameResultAsDepreciatedNhsAlgorithm(String value)
   {
      // Arrange.
#pragma warning disable CS0618 // Type or member is obsolete
      var deprecatedAlgorithm = new NhsAlgorithm();
#pragma warning restore CS0618 // Type or member is obsolete
      var extendedAlgorithm = new Modulus11ExtendedAlgorithm();

      // Act.
      var deprecatedResult = deprecatedAlgorithm.Validate(value);
      var extendedResult = extendedAlgorithm.Validate(value);

      // Assert.
      extendedResult.Should().Be(deprecatedResult);
   }

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("000 000 000 0", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Zero would return true unless length is explicitly checked.
   [InlineData("1")]
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputHasMoreThan9UnmaskedDigits()
      => _sut.Validate("00000000019", _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("000 000 001 9")]
   [InlineData("000 000 010 8")]
   [InlineData("000 000 100 7")]
   [InlineData("000 001 000 6")]
   [InlineData("000 010 000 5")]
   [InlineData("000 100 000 4")]
   [InlineData("001 000 000 3")]
   [InlineData("010 000 000 2")]
   [InlineData("100 000 000 1")]
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("156 865 652 1")]    // ISBN-10 Island in the Stream of Time, S. M. Sterling
   [InlineData("044 100 560 8")]    // ISBN-10 The Warlock in Spite of Himself, Christopher Stasheff
   [InlineData("071 410 544 9")]    // ISBN-10 The Sutton Hoo Ship Burial, Angela Care Evans
   //
   [InlineData("030 640 615 2")]    // Worked example of ISBN-10 from Wikipedia https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
   [InlineData("037 859 55")]       // Worked example of ISSN from Wikipedia https://en.wikipedia.org/wiki/ISSN
   [InlineData("031 784 71")]       // Example ISSN from https://www.issn.org/understanding-the-issn/what-is-an-issn/
   //
   [InlineData("943 476 591 9")]    // Worked example from Wikipedia https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters
   [InlineData("450 557 710 4")]    // Example from https://www.clatterbridgecc.nhs.uk/patients/general-information/nhs-number#:~:text=Your%20NHS%20Number%20is%20printed,is%20an%20example%20number%20only).
   [InlineData("530 119 491 7")]    // Random NHS number from http://danielbayley.uk/nhs-number/
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("1568656521")]    // ISBN-10 Island in the Stream of Time, S. M. Sterling
   [InlineData("0441005608")]    // ISBN-10 The Warlock in Spite of Himself, Christopher Stasheff
   [InlineData("0714105449")]    // ISBN-10 The Sutton Hoo Ship Burial, Angela Care Evans
   //
   [InlineData("0306406152")]    // Worked example of ISBN-10 from Wikipedia https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
   [InlineData("03785955")]      // Worked example of ISSN from Wikipedia https://en.wikipedia.org/wiki/ISSN
   [InlineData("03178471")]      // Example ISSN from https://www.issn.org/understanding-the-issn/what-is-an-issn/
   //
   [InlineData("9434765919")]    // Worked example from Wikipedia https://en.wikipedia.org/wiki/NHS_number#Format,_number_ranges,_and_check_characters
   [InlineData("4505577104")]    // Example from https://www.clatterbridgecc.nhs.uk/patients/general-information/nhs-number#:~:text=Your%20NHS%20Number%20is%20printed,is%20an%20example%20number%20only).
   [InlineData("5301194917")]    // Random NHS number from http://danielbayley.uk/nhs-number/
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]
   [InlineData("156 864 652 1")]    // ISBN-10 with single digit transcription error 5 -> 4
   [InlineData("044 105 060 8")]    // ISBN-10 with two digit transposition error 05 -> 50
   [InlineData("394 678 788 1")]    // Valid NHS number (9876544321) with jump transposition 674 -> 467
   [InlineData("851 556 824 3")]    // Valid NHS number (8514468243) with twin error 44 -> 55
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000 000 000 0", _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   // ISBN and ISSN test cases that would result in a check digit = 'X', replaced with '0'
   [InlineData("050 027 293 0")]    // ISBN-10 Roman London, Peter Marsden
   //
   [InlineData("243 456 10")]       // Example ISSN from Wikipedia
   [InlineData("105 012 40")]       // Example ISSN from https://www.issn.org/understanding-the-issn/what-is-an-issn/
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenModulus11HasRemainderOf10(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Theory]
   [InlineData("100 0G0 000 5")]    // Value 1000300005 would have a check digit = 5. G is 20 positions later in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
   [InlineData("100 0)0 000 5")]    // ) is 10 positions earlier in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
   [InlineData("5 0 119 4 1 7")]    // Random NHS number from http://danielbayley.uk/nhs-number/ with extraneous spaces
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenCheckDigitIsNonDigitCharacter()
      => _sut.Validate("100 030 000 X", _groupsOfThreeMask).Should().BeFalse();    // Actual check digit would be 5

   [Theory]
   [InlineData("140 6")]
   [InlineData("140 662 0")]
   [InlineData("140 662 538 8")]
   public void Modulus11DecimalAlgorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
