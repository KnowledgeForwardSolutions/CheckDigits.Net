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
