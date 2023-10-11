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
   [InlineData("0000000000F8")]  //15 = 1 + (2 * 5) => 8
   [InlineData("0000000000G6")]  //16 = 1 + (2 * 6) => 6
   [InlineData("0000000000H4")]  //17 = 1 + (2 * 7) => 4
   [InlineData("0000000000I2")]  //18 = 1 + (2 * 8) => 2
   [InlineData("0000000000J0")]  //19 = 1 + (2 * 9) => 0
   [InlineData("0000000000K8")]  //20 = 2 + (2 * 0) => 8
   [InlineData("0000000000L6")]  //21 = 2 + (2 * 1) => 6
   [InlineData("0000000000M4")]  //22 = 2 + (2 * 2) => 4
   [InlineData("0000000000N2")]  //23 = 2 + (2 * 3) => 2
   [InlineData("0000000000O0")]  //24 = 2 + (2 * 4) => 0
   [InlineData("0000000000P7")]  //25 = 2 + (2 * 5) => 7
   [InlineData("0000000000Q5")]  //26 = 2 + (2 * 6) => 5
   [InlineData("0000000000R3")]  //27 = 2 + (2 * 7) => 3
   [InlineData("0000000000S1")]  //28 = 2 + (2 * 8) => 1
   [InlineData("0000000000T9")]  //29 = 2 + (2 * 9) => 9
   [InlineData("0000000000U7")]  //30 = 3 + (2 * 0) => 7
   [InlineData("0000000000V5")]  //31 = 3 + (2 * 1) => 5
   [InlineData("0000000000W3")]  //32 = 3 + (2 * 2) => 3
   [InlineData("0000000000X1")]  //33 = 3 + (2 * 3) => 1
   [InlineData("0000000000Y9")]  //34 = 3 + (2 * 4) => 9
   [InlineData("0000000000Z6")]  //35 = 3 + (2 * 5) => 6
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
   [InlineData("GB123909ABC8")]     // GB123099ABC8 with two digit transposition 09 -> 90
   [InlineData("GB8091XYZ349")]     // GB8901XYZ349 with two digit transposition 90 -> 09
   [InlineData("US1155334451")]     // US1122334451 with two digit twin error 22 -> 55
   [InlineData("US1122337751")]     // US1122334451 with two digit twin error 44 -> 77
   [InlineData("US9988773340")]     // US9988776640 with two digit twin error 66 -> 33
   public void IsinAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("US30703M1027")]     // US30303M1027 with single digit transcription error 3 -> 7
   [InlineData("US02079J1079")]     // US02079K1079 with single character transcription error K -> J
   [InlineData("GB0031338658")]     // GB0031348658 with single digit transcription error 4 -> 3
   [InlineData("US0387331005")]     // US0378331005 with two digit transposition error 78 -> 87 
   [InlineData("US020791K079")]     // US02079K1079 with two character transposition error K1 -> 1K
   //[InlineData("US3030M31027")]     // US30303M1027 with two character transposition error 3M -> M3
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
   [InlineData("US1122G34451")]     // Value US1122334451 would have check digit = 1. G is 20 positions later in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
   [InlineData("US1122)34451")]     // ) is 10 positions earlier in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
   public void IsinAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
