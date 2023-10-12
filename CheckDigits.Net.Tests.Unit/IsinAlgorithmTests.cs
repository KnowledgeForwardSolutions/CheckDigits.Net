// Ignore Spelling: Isin

namespace CheckDigits.Net.Tests.Unit;

public class IsinAlgorithmTests
{
   private readonly IsinAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IsinAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.IsinAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IsinAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.IsinAlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthLessThanElevenCharacters()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("1234567890", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThanElevenCharacters()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("123456789012", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("00000000001", '8')]
   [InlineData("00000000100", '8')]
   [InlineData("00000010000", '8')]
   [InlineData("00001000000", '8')]
   [InlineData("00100000000", '8')]
   [InlineData("10000000000", '8')]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightOddPositionDigits(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("00000000010", '9')]
   [InlineData("00000001000", '9')]
   [InlineData("00000100000", '9')]
   [InlineData("00010000000", '9')]
   [InlineData("01000000000", '9')]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightEvenPositionDigits(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("0000000000A", '9')]
   [InlineData("00000000A00", '9')]
   [InlineData("000000A0000", '9')]
   [InlineData("0000A000000", '9')]
   [InlineData("00A00000000", '9')]
   [InlineData("A0000000000", '9')]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightOddPositionLetters(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("000000000A0", '8')]
   [InlineData("0000000A000", '8')]
   [InlineData("00000A00000", '8')]
   [InlineData("000A0000000", '8')]
   [InlineData("0A000000000", '8')]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightEvenPositionLetters(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("00000000000", '0')]
   [InlineData("00000000001", '8')]
   [InlineData("00000000002", '6')]
   [InlineData("00000000003", '4')]
   [InlineData("00000000004", '2')]
   [InlineData("00000000005", '9')]
   [InlineData("00000000006", '7')]
   [InlineData("00000000007", '5')]
   [InlineData("00000000008", '3')]
   [InlineData("00000000009", '1')]

   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldCalculateCorrectDoubleForOddPositionCharacters(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("0000000000A", '9')]  //10 = 1 + (2 * 0) => 9
   [InlineData("0000000000B", '7')]  //11 = 1 + (2 * 1) => 7
   [InlineData("0000000000C", '5')]  //12 = 1 + (2 * 2) => 5
   [InlineData("0000000000D", '3')]  //13 = 1 + (2 * 3) => 3
   [InlineData("0000000000E", '1')]  //14 = 1 + (2 * 4) => 1
   [InlineData("0000000000F", '8')]  //15 = 1 + (2 * 5) => 8
   [InlineData("0000000000G", '6')]  //16 = 1 + (2 * 6) => 6
   [InlineData("0000000000H", '4')]  //17 = 1 + (2 * 7) => 4
   [InlineData("0000000000I", '2')]  //18 = 1 + (2 * 8) => 2
   [InlineData("0000000000J", '0')]  //19 = 1 + (2 * 9) => 0
   [InlineData("0000000000K", '8')]  //20 = 2 + (2 * 0) => 8
   [InlineData("0000000000L", '6')]  //21 = 2 + (2 * 1) => 6
   [InlineData("0000000000M", '4')]  //22 = 2 + (2 * 2) => 4
   [InlineData("0000000000N", '2')]  //23 = 2 + (2 * 3) => 2
   [InlineData("0000000000O", '0')]  //24 = 2 + (2 * 4) => 0
   [InlineData("0000000000P", '7')]  //25 = 2 + (2 * 5) => 7
   [InlineData("0000000000Q", '5')]  //26 = 2 + (2 * 6) => 5
   [InlineData("0000000000R", '3')]  //27 = 2 + (2 * 7) => 3
   [InlineData("0000000000S", '1')]  //28 = 2 + (2 * 8) => 1
   [InlineData("0000000000T", '9')]  //29 = 2 + (2 * 9) => 9
   [InlineData("0000000000U", '7')]  //30 = 3 + (2 * 0) => 7
   [InlineData("0000000000V", '5')]  //31 = 3 + (2 * 1) => 5
   [InlineData("0000000000W", '3')]  //32 = 3 + (2 * 2) => 3
   [InlineData("0000000000X", '1')]  //33 = 3 + (2 * 3) => 1
   [InlineData("0000000000Y", '9')]  //34 = 3 + (2 * 4) => 9
   [InlineData("0000000000Z", '6')]  //35 = 3 + (2 * 5) => 6
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyCalculateOddPositionLetterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("000000000A0", '8')]  //10 = (2 * 1) + 0 => 8
   [InlineData("000000000B0", '7')]  //11 = (2 * 1) + 1 => 7
   [InlineData("000000000C0", '6')]  //12 = (2 * 1) + 2 => 6
   [InlineData("000000000D0", '5')]  //13 = (2 * 1) + 3 => 5
   [InlineData("000000000E0", '4')]  //14 = (2 * 1) + 4 => 4
   [InlineData("000000000F0", '3')]  //15 = (2 * 1) + 5 => 3
   [InlineData("000000000G0", '2')]  //16 = (2 * 1) + 6 => 2
   [InlineData("000000000H0", '1')]  //17 = (2 * 1) + 7 => 1
   [InlineData("000000000I0", '0')]  //18 = (2 * 1) + 8 => 0
   [InlineData("000000000J0", '9')]  //19 = (2 * 1) + 9 => 9
   [InlineData("000000000K0", '6')]  //20 = (2 * 2) + 0 => 6
   [InlineData("000000000L0", '5')]  //21 = (2 * 2) + 1 => 5
   [InlineData("000000000M0", '4')]  //22 = (2 * 2) + 2 => 4
   [InlineData("000000000N0", '3')]  //23 = (2 * 2) + 3 => 3
   [InlineData("000000000O0", '2')]  //24 = (2 * 2) + 4 => 2
   [InlineData("000000000P0", '1')]  //25 = (2 * 2) + 5 => 1
   [InlineData("000000000Q0", '0')]  //26 = (2 * 2) + 6 => 0
   [InlineData("000000000R0", '9')]  //27 = (2 * 2) + 7 => 9
   [InlineData("000000000S0", '8')]  //28 = (2 * 2) + 8 => 8
   [InlineData("000000000T0", '7')]  //29 = (2 * 2) + 9 => 7
   [InlineData("000000000U0", '4')]  //30 = (2 * 3) + 0 => 4
   [InlineData("000000000V0", '3')]  //31 = (2 * 3) + 1 => 3
   [InlineData("000000000W0", '2')]  //32 = (2 * 3) + 2 => 2
   [InlineData("000000000X0", '1')]  //33 = (2 * 3) + 3 => 1
   [InlineData("000000000Y0", '0')]  //34 = (2 * 3) + 4 => 0
   [InlineData("000000000Z0", '9')]  //35 = (2 * 3) + 5 => 9
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldCorrectlyCalculateEvenPositionLetterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("US037833100", '5')]     // Apple
   [InlineData("AU0000XVGZA", '3')]     // Treasury Corporation of Victoria
   [InlineData("GB000263494", '6')]     // BAE Systems
   [InlineData("US30303M102", '7')]     // Meta (Facebook)
   [InlineData("US02079K107", '9')]     // Google Class C
   [InlineData("GB003134865", '8')]     // Barclays
   [InlineData("US88160R101", '4')]     // Tesla
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "00000000000";
      var expectedCheckDigit = CharConstants.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("US1122)3445")]
   public void IsinAlgorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IsinAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void IsinAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Fact]
   public void IsinAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTwelveCharacters()
      => _sut.Validate("12345678901").Should().BeFalse();

   [Fact]
   public void IsinAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanTwelveCharacters()
      => _sut.Validate("1234567890123").Should().BeFalse();

   [Theory]
   [InlineData("000000000018")]
   [InlineData("000000001008")]
   [InlineData("000000100008")]
   [InlineData("000010000008")]
   [InlineData("001000000008")]
   [InlineData("100000000008")]
   public void IsinAlgorithm_Validate_ShouldCorrectlyWeightOddPositionDigits(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000000109")]
   [InlineData("000000010009")]
   [InlineData("000001000009")]
   [InlineData("000100000009")]
   [InlineData("010000000009")]
   public void IsinAlgorithm_Validate_ShouldCorrectlyWeightEvenPositionDigits(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("0000000000A9")]
   [InlineData("00000000A009")]
   [InlineData("000000A00009")]
   [InlineData("0000A0000009")]
   [InlineData("00A000000009")]
   [InlineData("A00000000009")]
   public void IsinAlgorithm_Validate_ShouldCorrectlyWeightOddPositionLetters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000000A08")]
   [InlineData("0000000A0008")]
   [InlineData("00000A000008")]
   [InlineData("000A00000008")]
   [InlineData("0A0000000008")]
   public void IsinAlgorithm_Validate_ShouldCorrectlyWeightEvenPositionLetters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000000000")]
   [InlineData("000000000018")]
   [InlineData("000000000026")]
   [InlineData("000000000034")]
   [InlineData("000000000042")]
   [InlineData("000000000059")]
   [InlineData("000000000067")]
   [InlineData("000000000075")]
   [InlineData("000000000083")]
   [InlineData("000000000091")]

   public void IsinAlgorithm_Validate_ShouldCalculateCorrectDoubleForOddPositionCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("0000000000A9")]  //10 = 1 + (2 * 0) => 9
   [InlineData("0000000000B7")]  //11 = 1 + (2 * 1) => 7
   [InlineData("0000000000C5")]  //12 = 1 + (2 * 2) => 5
   [InlineData("0000000000D3")]  //13 = 1 + (2 * 3) => 3
   [InlineData("0000000000E1")]  //14 = 1 + (2 * 4) => 1
   [InlineData("0000000000F8")]  //15 = 1 + (2 * 5 = 10 => 1 + 0 = 1) => 8
   [InlineData("0000000000G6")]  //16 = 1 + (2 * 6 = 12 => 1 + 2 = 3) => 6
   [InlineData("0000000000H4")]  //17 = 1 + (2 * 7 = 13 => 1 + 3 = 4) => 4
   [InlineData("0000000000I2")]  //18 = 1 + (2 * 8 = 16 => 1 + 6 = 7) => 2
   [InlineData("0000000000J0")]  //19 = 1 + (2 * 9 = 18 => 1 + 8 = 9) => 0
   [InlineData("0000000000K8")]  //20 = 2 + (2 * 0) => 8
   [InlineData("0000000000L6")]  //21 = 2 + (2 * 1) => 6
   [InlineData("0000000000M4")]  //22 = 2 + (2 * 2) => 4
   [InlineData("0000000000N2")]  //23 = 2 + (2 * 3) => 2
   [InlineData("0000000000O0")]  //24 = 2 + (2 * 4) => 0
   [InlineData("0000000000P7")]  //25 = 2 + (2 * 5 = 10 => 1 + 0 = 1) => 7
   [InlineData("0000000000Q5")]  //26 = 2 + (2 * 6 = 12 => 1 + 2 = 3) => 5
   [InlineData("0000000000R3")]  //27 = 2 + (2 * 7 = 13 => 1 + 3 = 4) => 3
   [InlineData("0000000000S1")]  //28 = 2 + (2 * 8 = 16 => 1 + 6 = 7) => 1
   [InlineData("0000000000T9")]  //29 = 2 + (2 * 9 = 18 => 1 + 8 = 9) => 9
   [InlineData("0000000000U7")]  //30 = 3 + (2 * 0) => 7
   [InlineData("0000000000V5")]  //31 = 3 + (2 * 1) => 5
   [InlineData("0000000000W3")]  //32 = 3 + (2 * 2) => 3
   [InlineData("0000000000X1")]  //33 = 3 + (2 * 3) => 1
   [InlineData("0000000000Y9")]  //34 = 3 + (2 * 4) => 9
   [InlineData("0000000000Z6")]  //35 = 3 + (2 * 5 = 10 => 1 + 0 = 1) => 6
   public void IsinAlgorithm_Validate_ShouldCorrectlyCalculateOddPositionLetterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000000A08")]  //10 = (2 * 1) + 0 => 8
   [InlineData("000000000B07")]  //11 = (2 * 1) + 1 => 7
   [InlineData("000000000C06")]  //12 = (2 * 1) + 2 => 6
   [InlineData("000000000D05")]  //13 = (2 * 1) + 3 => 5
   [InlineData("000000000E04")]  //14 = (2 * 1) + 4 => 4
   [InlineData("000000000F03")]  //15 = (2 * 1) + 5 => 3
   [InlineData("000000000G02")]  //16 = (2 * 1) + 6 => 2
   [InlineData("000000000H01")]  //17 = (2 * 1) + 7 => 1
   [InlineData("000000000I00")]  //18 = (2 * 1) + 8 => 0
   [InlineData("000000000J09")]  //19 = (2 * 1) + 9 => 9
   [InlineData("000000000K06")]  //20 = (2 * 2) + 0 => 6
   [InlineData("000000000L05")]  //21 = (2 * 2) + 1 => 5
   [InlineData("000000000M04")]  //22 = (2 * 2) + 2 => 4
   [InlineData("000000000N03")]  //23 = (2 * 2) + 3 => 3
   [InlineData("000000000O02")]  //24 = (2 * 2) + 4 => 2
   [InlineData("000000000P01")]  //25 = (2 * 2) + 5 => 1
   [InlineData("000000000Q00")]  //26 = (2 * 2) + 6 => 0
   [InlineData("000000000R09")]  //27 = (2 * 2) + 7 => 9
   [InlineData("000000000S08")]  //28 = (2 * 2) + 8 => 8
   [InlineData("000000000T07")]  //29 = (2 * 2) + 9 => 7
   [InlineData("000000000U04")]  //30 = (2 * 3) + 0 => 4
   [InlineData("000000000V03")]  //31 = (2 * 3) + 1 => 3
   [InlineData("000000000W02")]  //32 = (2 * 3) + 2 => 2
   [InlineData("000000000X01")]  //33 = (2 * 3) + 3 => 1
   [InlineData("000000000Y00")]  //34 = (2 * 3) + 4 => 0
   [InlineData("000000000Z09")]  //35 = (2 * 3) + 5 => 9
   public void IsinAlgorithm_Validate_ShouldCorrectlyCalculateEvenPositionLetterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("US0378331005")]     // Apple
   [InlineData("AU0000XVGZA3")]     // Treasury Corporation of Victoria
   [InlineData("GB0002634946")]     // BAE Systems
   [InlineData("US30303M1027")]     // Meta (Facebook)
   [InlineData("US02079K1079")]     // Google Class C
   [InlineData("GB0031348658")]     // Barclays
   [InlineData("US88160R1014")]     // Tesla
   public void IsinAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]                         // Dummy ISIN values from https://www.isindb.com/fix-isin-calculate-isin-check-digit/
   [InlineData("AU0000VXGZA3")]     // AU0000XVGZA3 with two character transposition XV -> VX
   [InlineData("US0000000QB4")]     // US0000000BQ4 with two character transposition BQ -> QB
   [InlineData("GB123909ABC8")]     // GB123099ABC8 with two digit transposition 09 -> 90
   [InlineData("GB8091XYZ349")]     // GB8901XYZ349 with two digit transposition 90 -> 09
   [InlineData("US1155334451")]     // US1122334451 with two digit twin error 22 -> 55
   [InlineData("US1122337751")]     // US1122334451 with two digit twin error 44 -> 77
   [InlineData("US9988773340")]     // US9988776640 with two digit twin error 66 -> 33
   [InlineData("US3030M31027")]     // US30303M1027 with two character transposition 3M -> M3
   [InlineData("US303031M027")]     // US30303M1027 with two character transposition M1 -> 1M
   [InlineData("AU000X0VGZA3")]     // AU0000XVGZA3 with two character transposition 0X -> X0
   [InlineData("G0B002634946")]     // GB0002634946 with two character transposition B0 -> 0B
   public void IsinAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("US30703M1027")]     // US30303M1027 with single digit transcription error 3 -> 7
   [InlineData("US02079J1079")]     // US02079K1079 with single character transcription error K -> J
   [InlineData("GB0031338658")]     // GB0031348658 with single digit transcription error 4 -> 3
   [InlineData("US0387331005")]     // US0378331005 with two digit transposition error 78 -> 87 
   [InlineData("US020791K079")]     // US02079K1079 with two character transposition error K1 -> 1K
   [InlineData("US99160R1014")]     // US88160R1014 with two digit twin error 88 -> 99
   [InlineData("GB0112634946")]     // GB0002634946 with two digit twin error 00 -> 11
   [InlineData("US12BB3DD566")]     // US12AA3DD566 with two letter twin error AA -> BB
   public void IsinAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void IsinAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000000000000").Should().BeTrue();

   [Fact]
   public void IsinAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("CA120QWERTY0").Should().BeTrue();

   [Theory]
   [InlineData("US1122)34451")]
   public void IsinAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
