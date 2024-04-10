// Ignore Spelling: Cusip

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class CusipAlgorithmTests
{
   private readonly CusipAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CusipAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.CusipAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CusipAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.CusipAlgorithmName);

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CusipAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void CusipAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Fact]
   public void CusipAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanNineCharacters()
      => _sut.Validate("00000018").Should().BeFalse();

   [Fact]
   public void CusipAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanNineCharacters()
      => _sut.Validate("0000000018").Should().BeFalse();

   [Theory]
   [InlineData("000000018")]
   [InlineData("000001008")]
   [InlineData("000100008")]
   [InlineData("010000008")]
   public void CusipAlgorithm_Validate_ShouldCorrectlyWeightOddPositionLetters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000109")]
   [InlineData("000010009")]
   [InlineData("001000009")]
   [InlineData("100000009")]
   public void CusipAlgorithm_Validate_ShouldCorrectlyWeightEvenPositionLetters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000000")]  //  0 =>  0 * 2 =  0 => 0 + 0 =  0 => 0
   [InlineData("000000018")]  //  1 =>  1 * 2 =  2 => 0 + 2 =  2 => 8
   [InlineData("000000026")]  //  2 =>  2 * 2 =  4 => 0 + 4 =  4 => 6
   [InlineData("000000034")]  //  3 =>  3 * 2 =  6 => 0 + 6 =  6 => 4
   [InlineData("000000042")]  //  4 =>  4 * 2 =  8 => 0 + 8 =  8 => 2
   [InlineData("000000059")]  //  5 =>  5 * 2 = 10 => 1 + 0 =  1 => 9
   [InlineData("000000067")]  //  6 =>  6 * 2 = 12 => 1 + 2 =  3 => 7
   [InlineData("000000075")]  //  7 =>  7 * 2 = 14 => 1 + 4 =  5 => 5
   [InlineData("000000083")]  //  8 =>  8 * 2 = 16 => 1 + 6 =  7 => 3
   [InlineData("000000091")]  //  9 =>  9 * 2 = 18 => 1 + 8 =  9 => 1
   [InlineData("0000000A8")]  // 10 =>  0 * 2 = 20 => 2 + 0 =  2 => 8
   [InlineData("0000000B6")]  // 11 => 11 * 2 = 22 => 2 + 2 =  4 => 6
   [InlineData("0000000C4")]  // 12 => 12 * 2 = 24 => 2 + 4 =  6 => 4
   [InlineData("0000000D2")]  // 13 => 13 * 2 = 26 => 2 + 6 =  8 => 2
   [InlineData("0000000E0")]  // 14 => 14 * 2 = 28 => 2 + 8 = 10 => 0
   [InlineData("0000000F7")]  // 15 => 15 * 2 = 30 => 3 + 0 =  3 => 7
   [InlineData("0000000G5")]  // 16 => 16 * 2 = 32 => 3 + 2 =  5 => 5
   [InlineData("0000000H3")]  // 17 => 17 * 2 = 34 => 3 + 4 =  7 => 3
   [InlineData("0000000I1")]  // 18 => 18 * 2 = 36 => 3 + 6 =  9 => 1
   [InlineData("0000000J9")]  // 19 => 19 * 2 = 38 => 3 + 8 = 11 => 9
   [InlineData("0000000K6")]  // 20 => 20 * 2 = 40 => 4 + 0 =  4 => 6
   [InlineData("0000000L4")]  // 21 => 21 * 2 = 42 => 4 + 2 =  6 => 4
   [InlineData("0000000M2")]  // 22 => 22 * 2 = 44 => 4 + 4 =  8 => 2
   [InlineData("0000000N0")]  // 23 => 23 * 2 = 46 => 4 + 6 = 10 => 0
   [InlineData("0000000O8")]  // 24 => 24 * 2 = 48 => 4 + 8 = 12 => 8
   [InlineData("0000000P5")]  // 25 => 25 * 2 = 50 => 5 + 0 =  5 => 5
   [InlineData("0000000Q3")]  // 26 => 26 * 2 = 52 => 5 + 2 =  7 => 3
   [InlineData("0000000R1")]  // 27 => 27 * 2 = 54 => 5 + 4 =  9 => 1
   [InlineData("0000000S9")]  // 28 => 28 * 2 = 56 => 5 + 6 = 11 => 9
   [InlineData("0000000T7")]  // 29 => 29 * 2 = 58 => 5 + 8 = 13 => 7
   [InlineData("0000000U4")]  // 30 => 30 * 2 = 60 => 6 + 0 =  6 => 4
   [InlineData("0000000V2")]  // 31 => 31 * 2 = 62 => 6 + 2 =  8 => 2
   [InlineData("0000000W0")]  // 32 => 32 * 2 = 64 => 6 + 4 = 10 => 0
   [InlineData("0000000X8")]  // 33 => 33 * 2 = 66 => 6 + 6 = 12 => 8
   [InlineData("0000000Y6")]  // 34 => 34 * 2 = 68 => 6 + 8 = 14 => 6
   [InlineData("0000000Z3")]  // 35 => 35 * 2 = 70 => 7 + 0 =  7 => 3
   [InlineData("0000000*1")]  // 36 => 36 * 2 = 72 => 7 + 2 =  9 => 1
   [InlineData("0000000@9")]  // 37 => 37 * 2 = 74 => 7 + 4 = 11 => 9
   [InlineData("0000000#7")]  // 38 => 38 * 2 = 76 => 7 + 6 = 13 => 7
   public void CusipAlgorithm_Validate_ShouldCalculateCorrectDoubleForOddPositionCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000000")]  //  0 => 0 + 0 =  0 => 0
   [InlineData("000000109")]  //  1 => 0 + 1 =  1 => 9
   [InlineData("000000208")]  //  2 => 0 + 2 =  2 => 8
   [InlineData("000000307")]  //  3 => 0 + 3 =  3 => 7
   [InlineData("000000406")]  //  4 => 0 + 4 =  4 => 6
   [InlineData("000000505")]  //  5 => 0 + 5 =  5 => 5
   [InlineData("000000604")]  //  6 => 0 + 6 =  6 => 4
   [InlineData("000000703")]  //  7 => 0 + 7 =  7 => 3
   [InlineData("000000802")]  //  8 => 0 + 8 =  8 => 2
   [InlineData("000000901")]  //  9 => 0 + 9 =  9 => 1
   [InlineData("000000A09")]  // 10 => 1 + 0 =  1 => 9
   [InlineData("000000B08")]  // 11 => 1 + 1 =  2 => 8
   [InlineData("000000C07")]  // 12 => 1 + 2 =  3 => 7
   [InlineData("000000D06")]  // 13 => 1 + 3 =  4 => 6
   [InlineData("000000E05")]  // 14 => 1 + 4 =  5 => 5
   [InlineData("000000F04")]  // 15 => 1 + 5 =  6 => 4
   [InlineData("000000G03")]  // 16 => 1 + 6 =  7 => 3
   [InlineData("000000H02")]  // 17 => 1 + 7 =  8 => 2
   [InlineData("000000I01")]  // 18 => 1 + 8 =  9 => 1
   [InlineData("000000J00")]  // 19 => 1 + 9 = 10 => 0
   [InlineData("000000K08")]  // 20 => 2 + 0 =  2 => 8
   [InlineData("000000L07")]  // 21 => 2 + 1 =  3 => 7
   [InlineData("000000M06")]  // 22 => 2 + 2 =  4 => 6
   [InlineData("000000N05")]  // 23 => 2 + 3 =  5 => 5
   [InlineData("000000O04")]  // 24 => 2 + 4 =  6 => 4
   [InlineData("000000P03")]  // 25 => 2 + 5 =  7 => 3
   [InlineData("000000Q02")]  // 26 => 2 + 6 =  8 => 2
   [InlineData("000000R01")]  // 27 => 2 + 7 =  9 => 1
   [InlineData("000000S00")]  // 28 => 2 + 8 = 10 => 0
   [InlineData("000000T09")]  // 29 => 2 + 9 = 11 => 9
   [InlineData("000000U07")]  // 30 => 3 + 0 =  3 => 7
   [InlineData("000000V06")]  // 31 => 3 + 1 =  4 => 6
   [InlineData("000000W05")]  // 32 => 3 + 2 =  5 => 5
   [InlineData("000000X04")]  // 33 => 3 + 3 =  6 => 4
   [InlineData("000000Y03")]  // 34 => 3 + 4 =  7 => 3
   [InlineData("000000Z02")]  // 35 => 3 + 5 =  8 => 2
   [InlineData("000000*01")]  // 36 => 3 + 6 =  9 => 1
   [InlineData("000000@00")]  // 37 => 3 + 7 = 10 => 0
   [InlineData("000000#09")]  // 38 => 3 + 8 = 11 => 9
   public void CusipAlgorithm_Validate_ShouldCalculateCorrectValueForEvenPositionCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("037833100")]     // Apple
   [InlineData("88160R101")]     // Tesla
   [InlineData("38143VAA7")]     // Goldman Sachs GS Capital I 
   [InlineData("30303M102")]     // Meta (Facebook)
   [InlineData("38259P508")]     // Google
   [InlineData("68389X105")]     // Oracle Corporation
   [InlineData("912797GC5")]     // US Treasury Bill
   [InlineData("91282CJL6")]     // US Treasury Note
   public void CusipAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("037837100")]     // 037833100 with single digit transcription error 3 -> 7
   [InlineData("88160S101")]     // 88160R101 with single character transcription error R -> S
   [InlineData("39303M102")]     // 30303M102 with single digit transcription error 0 -> 9
   [InlineData("912779GC5")]     // 912797GC5 with two digit transposition error 97 -> 79 
   [InlineData("91282JCL6")]     // 91282CJL6 with two character transposition error CJ -> JC
   [InlineData("037844100")]     // 037833100 with two digit twin error 33 -> 44
   [InlineData("38143VBB7")]     // 38143VAA7 with two letter twin error AA -> BB
   public void CusipAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void CusipAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000000000").Should().BeTrue();

   [Fact]
   public void CusipAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("000000@00").Should().BeTrue();

   [Theory]
   [InlineData("0378!7100")]
   [InlineData("0378.7100")]
   [InlineData("0378:7100")]
   [InlineData("0378^7100")]
   public void CusipAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
