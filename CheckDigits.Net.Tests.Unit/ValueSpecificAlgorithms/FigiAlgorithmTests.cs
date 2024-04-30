// Ignore Spelling: Figi

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class FigiAlgorithmTests
{
   private readonly FigiAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FigiAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.FigiAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FigiAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.FigiAlgorithmName);

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void FigiAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void FigiAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Fact]
   public void FigiAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTwelveCharacters()
      => _sut.Validate("00000000018").Should().BeFalse();

   [Fact]
   public void FigiAlgorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanTwelveCharacters()
      => _sut.Validate("0000000000018").Should().BeFalse();

   [Theory]
   // Odd position characters
   [InlineData("000000000000")]     // Odd digits are the simplest cases
   [InlineData("000000000019")]
   [InlineData("000000000028")]
   [InlineData("000000000037")]
   [InlineData("000000000046")]
   [InlineData("000000000055")]
   [InlineData("000000000064")]
   [InlineData("000000000073")]
   [InlineData("000000000082")]
   [InlineData("000000000091")]
   [InlineData("0000000000B8")]     // B => 11 => 1 + 1 =>  2 => 10 - ( 2 % 10) = 8
   [InlineData("0000000000C7")]     // C => 12 => 1 + 2 =>  3 => 10 - ( 3 % 10) = 7
   [InlineData("0000000000D6")]     // D => 13 => 1 + 3 =>  4 => 10 - ( 4 % 10) = 6
   [InlineData("0000000000F4")]     // F => 15 => 1 + 5 =>  6 => 10 - ( 6 % 10) = 4
   [InlineData("0000000000G3")]     // G => 16 => 1 + 6 =>  7 => 10 - ( 7 % 10) = 3
   [InlineData("0000000000H2")]     // H => 17 => 1 + 7 =>  8 => 10 - ( 8 % 10) = 2
   [InlineData("0000000000J0")]     // J => 19 => 1 + 9 => 10 => 10 - (10 % 10) = 0
   [InlineData("0000000000K8")]     // K => 20 => 2 + 0 =>  2 => 10 - ( 2 % 10) = 8
   [InlineData("0000000000L7")]     // L => 21 => 2 + 1 =>  3 => 10 - ( 3 % 10) = 7
   [InlineData("0000000000M6")]     // M => 22 => 2 + 2 =>  4 => 10 - ( 4 % 10) = 6
   [InlineData("0000000000N5")]     // N => 23 => 2 + 3 =>  5 => 10 - ( 5 % 10) = 5
   [InlineData("0000000000P3")]     // P => 25 => 2 + 5 =>  7 => 10 - ( 7 % 10) = 3
   [InlineData("0000000000Q2")]     // Q => 26 => 2 + 6 =>  8 => 10 - ( 8 % 10) = 2
   [InlineData("0000000000R1")]     // R => 27 => 2 + 7 =>  9 => 10 - ( 9 % 10) = 1
   [InlineData("0000000000S0")]     // S => 28 => 2 + 8 => 10 => 10 - (10 % 10) = 0
   [InlineData("0000000000T9")]     // T => 29 => 2 + 9 => 11 => 10 - (11 % 10) = 9
   [InlineData("0000000000V6")]     // V => 31 => 3 + 1 =>  4 => 10 - ( 4 % 10) = 6
   [InlineData("0000000000W5")]     // W => 32 => 3 + 2 =>  5 => 10 - ( 5 % 10) = 5
   [InlineData("0000000000X4")]     // X => 33 => 3 + 3 =>  6 => 10 - ( 6 % 10) = 4
   [InlineData("0000000000Y3")]     // Y => 34 => 3 + 4 =>  7 => 10 - ( 7 % 10) = 3
   [InlineData("0000000000Z2")]     // Z => 35 => 3 + 5 =>  8 => 10 - ( 8 % 10) = 2
   // Even position characters
   [InlineData("000000000108")]     // 1 =>  1 =>  1 * 2 =  2 => 0 + 2 =>  2 => 10 - ( 2 % 10) = 8
   [InlineData("000000000206")]     // 2 =>  2 =>  2 * 2 =  4 => 0 + 4 =>  4 => 10 - ( 4 % 10) = 6
   [InlineData("000000000304")]     // 3 =>  3 =>  3 * 2 =  6 => 0 + 6 =>  6 => 10 - ( 6 % 10) = 4
   [InlineData("000000000402")]     // 4 =>  4 =>  4 * 2 =  8 => 0 + 8 =>  8 => 10 - ( 8 % 10) = 2
   [InlineData("000000000509")]     // 5 =>  5 =>  5 * 2 = 10 => 1 + 0 =>  1 => 10 - ( 1 % 10) = 9
   [InlineData("000000000607")]     // 6 =>  6 =>  6 * 2 = 12 => 1 + 2 =>  3 => 10 - ( 3 % 10) = 7
   [InlineData("000000000705")]     // 7 =>  7 =>  7 * 2 = 14 => 1 + 4 =>  5 => 10 - ( 5 % 10) = 5
   [InlineData("000000000803")]     // 8 =>  8 =>  8 * 2 = 16 => 1 + 6 =>  7 => 10 - ( 7 % 10) = 3
   [InlineData("000000000901")]     // 9 =>  9 =>  9 * 2 = 18 => 1 + 8 =>  9 => 10 - ( 9 % 10) = 1
   [InlineData("000000000B06")]     // B => 11 => 11 * 2 = 22 => 2 + 2 =>  4 => 10 - ( 4 % 10) = 6
   [InlineData("000000000C04")]     // C => 12 => 12 * 2 = 24 => 2 + 4 =>  6 => 10 - ( 6 % 10) = 4
   [InlineData("000000000D02")]     // D => 13 => 13 * 2 = 26 => 2 + 6 =>  8 => 10 - ( 8 % 10) = 2
   [InlineData("000000000F07")]     // F => 15 => 15 * 2 = 30 => 3 + 0 =>  0 => 10 - ( 0 % 10) = 7
   [InlineData("000000000G05")]     // G => 16 => 16 * 2 = 32 => 3 + 2 =>  5 => 10 - ( 5 % 10) = 5
   [InlineData("000000000H03")]     // H => 17 => 17 * 2 = 34 => 3 + 4 =>  7 => 10 - ( 7 % 10) = 3
   [InlineData("000000000J09")]     // J => 19 => 19 * 2 = 36 => 3 + 6 =>  9 => 10 - ( 9 % 10) = 9
   [InlineData("000000000K06")]     // K => 20 => 20 * 2 = 38 => 3 + 8 => 11 => 10 - (11 % 10) = 6
   [InlineData("000000000L04")]     // L => 21 => 21 * 2 = 42 => 4 + 2 =>  6 => 10 - ( 6 % 10) = 4
   [InlineData("000000000M02")]     // M => 22 => 22 * 2 = 44 => 4 + 4 =>  8 => 10 - ( 8 % 10) = 2
   [InlineData("000000000N00")]     // N => 23 => 23 * 2 = 46 => 4 + 6 => 10 => 10 - (10 % 10) = 0
   [InlineData("000000000P05")]     // P => 25 => 25 * 2 = 50 => 5 + 0 =>  5 => 10 - ( 5 % 10) = 5
   [InlineData("000000000Q03")]     // Q => 26 => 26 * 2 = 52 => 5 + 2 =>  7 => 10 - ( 7 % 10) = 3
   [InlineData("000000000R01")]     // R => 27 => 27 * 2 = 54 => 5 + 4 =>  9 => 10 - ( 9 % 10) = 1
   [InlineData("000000000S09")]     // S => 28 => 28 * 2 = 56 => 5 + 6 => 11 => 10 - (11 % 10) = 9
   [InlineData("000000000T07")]     // T => 29 => 29 * 2 = 58 => 5 + 8 => 13 => 10 - (13 % 10) = 7
   [InlineData("000000000V02")]     // V => 31 => 31 * 2 = 62 => 6 + 2 =>  8 => 10 - ( 8 % 10) = 2
   [InlineData("000000000W00")]     // W => 32 => 32 * 2 = 64 => 6 + 4 => 10 => 10 - (10 % 10) = 0
   [InlineData("000000000X08")]     // X => 33 => 33 * 2 = 66 => 6 + 6 => 12 => 10 - (12 % 10) = 8
   [InlineData("000000000Y06")]     // Y => 34 => 34 * 2 = 68 => 6 + 8 => 14 => 10 - (14 % 10) = 6
   [InlineData("000000000Z03")]     // Z => 35 => 35 * 2 = 70 => 7 + 0 =>  7 => 10 - ( 7 % 10) = 3
   public void FigiAlgorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
       => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000000108")]
   [InlineData("000000010008")]
   [InlineData("000001000008")]
   [InlineData("000100000008")]
   [InlineData("010000000008")]
   public void FigiAlgorithm_Validate_ShouldCorrectlyDoubleEvenPositionCharacters(String value)
       => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000000019")]
   [InlineData("000000001009")]
   [InlineData("000000100009")]
   [InlineData("000010000009")]
   [InlineData("001000000009")]
   [InlineData("100000000009")]
   public void FigiAlgorithm_Validate_ShouldNotDoubleOddPositionCharacters(String value)
       => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("BBG000B9Y5X2")]     // Apple
   [InlineData("BBG000N9P426")]     // Tesla
   [InlineData("BBG000BLNQ16")]     // Example from https://www.openfigi.com/assets/content/figi-check-digit-2173341b2d.pdf
   [InlineData("NRG92C84SB39")]     // Example from https://www.omg.org/spec/FIGI/1.0/PDF
   [InlineData("BBG000BLNNH6")]     // Example from https://www.openfigi.com/assets/local/figi-allocation-rules.pdf
   [InlineData("BBG000HY4HW9")]     // "
   [InlineData("BBG002NFJNQ7")]     // "
   [InlineData("BBG00015FB32")]     // "
   [InlineData("BBG0013WHY28")]     // "
   [InlineData("BBG000FWG7T8")]     // "
   [InlineData("BBG0014H5GZ6")]     // "
   public void FigiAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("BBG901NFJNQ0")]     // BBG091NFJNQ0 with two digit transposition error 09 -> 90
   [InlineData("BBG022NFJKQ1")]     // BBG055NFJKQ1 with two digit twin error 55 -> 22
   [InlineData("BBG066NFJKQ8")]     // BBG033NFJKQ1 with two digit twin error 33 -> 66
   [InlineData("BBG044NFJKQ5")]     // BBG077NFJKQ1 with two digit twin error 77 -> 44
   [InlineData("BBG543NFJKQ6")]     // BBG345NFJKQ6 with two digit jump transposition error 345 -> 543
   [InlineData("BBG091NNJFQ0")]     // BBG091NFJNQ0 with two character jump transposition error FJN -> NJF
   [InlineData("2BG0014H5GZ6")]     // BBG0014H5GZ6 with one character transcription error B -> 2
   [InlineData("BBGL01NFJNQ3")]     // BBG301NFJNQ3 with one character transcription error 3 -> L
   [InlineData("BBP002NFJNQ7")]     // BBG002NFJNQ7 with one character transcription error G -> P
   public void FigiAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("BBG000B9Y4X2")]     // BBG000B9Y5X2 with single digit transcription error 5 -> 4
   [InlineData("BBG000M9P426")]     // BBG000N9P426 with single character transcription error N -> M
   [InlineData("BBG0041H5GZ6")]     // BBG0014H5GZ6 with two digit transposition error 14 -> 41
   [InlineData("BBG00015BF32")]     // BBG00015FB32 with two character transposition error FB -> BF
   [InlineData("BBG000H4YHW9")]     // BBG000HY4HW9 with two character transposition error Y4 -> 4Y
   public void FigiAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void FigiAlgorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000000000000").Should().BeTrue();

   [Fact]
   public void FigiAlgorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("BBG111111160").Should().BeTrue();

   [Theory]
   [InlineData("BBG0/0B9Y5X2")]
   [InlineData("BBG0:0B9Y5X2")]
   [InlineData("BBG0[0B9Y5X2")]
   public void FigiAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("BBG000B9Y5X/")]
   [InlineData("BBG000B9Y5X:")]
   [InlineData("BBG000B9Y5XA")]
   [InlineData("BBG000B9Y5X[")]
   public void FigiAlgorithm_Validate_ShouldReturnFalse_WhenCheckDigitIsNonDigCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
