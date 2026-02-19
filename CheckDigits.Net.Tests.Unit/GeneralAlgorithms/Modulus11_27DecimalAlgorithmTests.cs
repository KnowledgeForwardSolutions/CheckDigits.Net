namespace CheckDigits.Net.Tests.Unit.GeneralAlgorithms;

public class Modulus11_27DecimalAlgorithmTests
{
   private readonly Modulus11_27DecimalAlgorithm _sut = new();
   private readonly ICheckDigitMask _acceptAllMask = new AcceptAllMask();
   private readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();
   private readonly ICheckDigitMask _rejectAllMask = new RejectAllMask();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11_27DecimalAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Modulus11_27DecimalAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11_27DecimalAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Modulus11_27DecimalAlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11_27DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Modulus11_27DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
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
   public void Modulus11_27DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnAllPossibleValidCheckDigits(
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
   [InlineData("001000000", '9')]
   [InlineData("010000000", '8')]
   [InlineData("100000000", '7')]
   public void Modulus11_27DecimalAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightCharactersByPosition(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("1510869508", '8')]   // https://arthurdejong.org/python-stdnum/doc/1.19/stdnum.no.fodselsnummer
   [InlineData("5662186", '5')]      // https://www.ibm.com/docs/en/rbd/9.6.0?topic=syslib-calculatechkdigitmod11
   [InlineData("13739", '1')]        // https://studylib.net/doc/5880755/ibm-mod-10-and-11-check-digits
   [InlineData("198932145", '1')]    // https://secure.fidelityifs.com/bookshelf/beta/doc/11080.htm
   [InlineData("10172232", '5')]     // ""
   [InlineData("036532", '7')]       //http://www.pgrocer.net/Cis51/mod11.html
   [InlineData("111111111111111111", '7')]    // Weights 2-7 repeated 3x
   public void Modulus11_27DecimalAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void Modulus11_27DecimalAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
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
   public void Modulus11_27DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("000040")]
   [InlineData("110111111")]
   public void Modulus11_27DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenModulus11HasRemainderOf10(String value)
   {
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
   public void Modulus11_27DecimalAlgorithm_TryCalculateCheckDigit_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigit(value, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Zero would return true unless length is explicitly checked.
   [InlineData("1")]
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTwo(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("0000000019")]
   [InlineData("0000000108")]
   [InlineData("0000001007")]
   [InlineData("0000010006")]
   [InlineData("0000100005")]
   [InlineData("0001000004")]
   [InlineData("0010000009")]
   [InlineData("0100000008")]
   [InlineData("1000000007")]
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("15108695088")]   // https://arthurdejong.org/python-stdnum/doc/1.19/stdnum.no.fodselsnummer
   [InlineData("56621865")]      // https://www.ibm.com/docs/en/rbd/9.6.0?topic=syslib-calculatechkdigitmod11
   [InlineData("137391")]        // https://studylib.net/doc/5880755/ibm-mod-10-and-11-check-digits
   [InlineData("1989321451")]    // https://secure.fidelityifs.com/bookshelf/beta/doc/11080.htm
   [InlineData("101722325")]     // ""
   [InlineData("0365327")]       //http://www.pgrocer.net/Cis51/mod11.html
   [InlineData("1111111111111111117")]    // Weights 2-7 repeated 3x
   public void Modulus11Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("15108694088")]   // 15108695088 with single digit transcription error 5 -> 4
   [InlineData("1983921451")]    // 1989321451 with two digit transposition error 93 -> 39
   [InlineData("56126865")]      // 56621865 with jump transposition 621 -> 126
   [InlineData("101733325")]     // 101722325 with twin error 22 -> 33
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("0000000000").Should().BeTrue();

   [Theory]
   [InlineData("400")]           // 4 with weight 3 = 12, 12 % 11 = 1, 11 - 1 = 10
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldReturnFalse_WhenModulus11HasRemainderOf10(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("0000G00005")]    // 'G' - '0' = 18
   [InlineData("0000+00005")]    // '+' - '0' = -10
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldReturnFalse_WhenCheckDigitIsNonDigitCharacter()
      => _sut.Validate("000010000?").Should().BeFalse();    // Actual check digit would be 5

   [Theory]
   [InlineData("1406")]
   [InlineData("1406620")]
   [InlineData("1406625385")]
   [InlineData("1406625380421")]
   [InlineData("1406625380425510")]
   [InlineData("1406625380425510288")]
   [InlineData("1406625380425510282650")]
   public void Modulus11_27DecimalAlgorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion

   #region Validate (ICheckDigitMask Overload) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, _acceptAllMask).Should().BeFalse();

   [Fact]
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenAllNonCheckDigitCharactersAreMaskedOut()
      => _sut.Validate("000 000 000 0", _rejectAllMask).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Zero would return true unless length is explicitly checked.
   [InlineData("1")]
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInsufficientUnmaskedCharactersToCalculateCheckDigit(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("000 000 001 9")]
   [InlineData("000 000 010 8")]
   [InlineData("000 000 100 7")]
   [InlineData("000 001 000 6")]
   [InlineData("000 010 000 5")]
   [InlineData("000 100 000 4")]
   [InlineData("001 000 000 9")]
   [InlineData("010 000 000 8")]
   [InlineData("100 000 000 7")]
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("151 086 950 88")]  // https://arthurdejong.org/python-stdnum/doc/1.19/stdnum.no.fodselsnummer
   [InlineData("566 218 65")]      // https://www.ibm.com/docs/en/rbd/9.6.0?topic=syslib-calculatechkdigitmod11
   [InlineData("137 391")]         // https://studylib.net/doc/5880755/ibm-mod-10-and-11-check-digits
   [InlineData("198 932 145 1")]   // https://secure.fidelityifs.com/bookshelf/beta/doc/11080.htm
   [InlineData("101 722 325")]     // ""
   [InlineData("036 532 7")]       //http://www.pgrocer.net/Cis51/mod11.html
   [InlineData("111 111 111 111 111 111 7")]    // Weights 2-7 repeated 3x
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("15108695088")]   // https://arthurdejong.org/python-stdnum/doc/1.19/stdnum.no.fodselsnummer
   [InlineData("56621865")]      // https://www.ibm.com/docs/en/rbd/9.6.0?topic=syslib-calculatechkdigitmod11
   [InlineData("137391")]        // https://studylib.net/doc/5880755/ibm-mod-10-and-11-check-digits
   [InlineData("1989321451")]    // https://secure.fidelityifs.com/bookshelf/beta/doc/11080.htm
   [InlineData("101722325")]     // ""
   [InlineData("0365327")]       //http://www.pgrocer.net/Cis51/mod11.html
   [InlineData("1111111111111111117")]    // Weights 2-7 repeated 3x
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnTrue_WhenValueContainsValidCheckDigitAndMaskAcceptsAllCharacters(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeTrue();

   [Theory]
   [InlineData("151 086 940 88")]  // 15108695088 with single digit transcription error 5 -> 4
   [InlineData("198 392 145 1")]   // 1989321451 with two digit transposition error 93 -> 39
   [InlineData("561 268 65")]      // 56621865 with jump transposition 621 -> 126
   [InlineData("101 733 325")]     // 101722325 with twin error 22 -> 33
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000 000 000 0", _groupsOfThreeMask).Should().BeTrue();

   [Theory]
   [InlineData("400")]           // 4 with weight 3 = 12, 12 % 11 = 1, 11 - 1 = 10
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenModulus11HasRemainderOf10(String value)
      => _sut.Validate(value, _acceptAllMask).Should().BeFalse();

   [Theory]
   [InlineData("000 0G0 000 5")]    // 'G' - '0' = 18
   [InlineData("000 0+0 000 5")]    // '+' - '0' = -10
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeFalse();

   [Fact]
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnFalse_WhenCheckDigitIsNonDigitCharacter()
      => _sut.Validate("000 010 000 ?", _groupsOfThreeMask).Should().BeFalse();    // Actual check digit would be 5

   [Theory]
   [InlineData("140 6")]
   [InlineData("140 662 0")]
   [InlineData("140 662 538 5")]
   [InlineData("140 662 538 042 1")]
   [InlineData("140 662 538 042 551 0")]
   [InlineData("140 662 538 042 551 028 8")]
   [InlineData("140 662 538 042 551 028 265 0")]
   public void Modulus11_27DecimalAlgorithm_ValidateMasked_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value, _groupsOfThreeMask).Should().BeTrue();

   #endregion
}
