// Ignore Spelling: Icao Mrp

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303AlgorithmTests
{
   private readonly Icao9303Algorithm _sut = new();
   private const String _mrpLine2Value = "L898902C36UTO7408122F1204159ZE184226B<<<<<10";  // Example machine readable passport (MRP) line 2 value from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Icao9303AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Icao9303AlgorithmName);

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Icao9303Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0000")]
   [InlineData("0011")]
   [InlineData("0022")]
   [InlineData("0033")]
   [InlineData("0044")]
   [InlineData("0055")]
   [InlineData("0066")]
   [InlineData("0077")]
   [InlineData("0088")]
   [InlineData("0099")]
   [InlineData("00A0")]
   [InlineData("00B1")]
   [InlineData("00C2")]
   [InlineData("00D3")]
   [InlineData("00E4")]
   [InlineData("00F5")]
   [InlineData("00G6")]
   [InlineData("00H7")]
   [InlineData("00I8")]
   [InlineData("00J9")]
   [InlineData("00K0")]
   [InlineData("00L1")]
   [InlineData("00M2")]
   [InlineData("00N3")]
   [InlineData("00O4")]
   [InlineData("00P5")]
   [InlineData("00Q6")]
   [InlineData("00R7")]
   [InlineData("00S8")]
   [InlineData("00T9")]
   [InlineData("00U0")]
   [InlineData("00V1")]
   [InlineData("00W2")]
   [InlineData("00X3")]
   [InlineData("00Y4")]
   [InlineData("00Z5")]
   [InlineData("00<0")]
   public void Icao9303Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1007")]
   [InlineData("0103")]
   [InlineData("0011")]
   [InlineData("0001007")]
   [InlineData("0000103")]
   [InlineData("0000011")]
   [InlineData("0000001007")]
   [InlineData("0000000103")]
   [InlineData("0000000011")]
   public void Icao9303Algorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   public void Icao9303Algorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("7408122")]             // Example from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf
   [InlineData("L898902C36")]          // "
   [InlineData("ZE184226B<<<<<1")]     // "
   public void Icao9303Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("L8989A2C36")]          // L898902C36 with single char transcription error (0 -> A) with delta 10
   [InlineData("L89890C236")]          // L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData("8812728")]             // 8812278 with two char transposition error (27 -> 72) with delta 5
   public void Icao9303Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("7438122")]             // 7408122 with single digit transcription error (0 -> 3)
   [InlineData("L898902D36")]          // L898902C36 with single char transcription error (C -> D)
   [InlineData("7480122")]             // 7408122 with two digit transposition error (08 -> 80)
   [InlineData("L8989023C6")]          // L898902C36 with two char transposition error (3C -> C3)
   public void Icao9303Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Icao9303Algorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("0000").Should().BeTrue();

   [Fact]
   public void Icao9303Algorithm_Validate_ShouldReturnTrue_WhenInputIsAllFillerCharacters()
      => _sut.Validate("<<<0").Should().BeTrue();

   [Fact]
   public void Icao9303Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("1030").Should().BeTrue();

   [Theory]
   [InlineData("74)8122")]             // 7438122 with 3 replaced with character 10 positions before in ASCII table
   [InlineData("74=8122")]             // 7438122 with 3 replaced with character 10 positions later in ASCII table
   [InlineData("`898902C36")]          // L898902C36 with L replaced with character 20 positions later in ASCII table
   public void Icao9303Algorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion

   #region Validate (Field) Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303Algorithm_ValidateField_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!, 1, 10).Should().BeFalse();

   [Fact]
   public void Icao9303Algorithm_ValidateField_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty, 13, 7).Should().BeFalse();

   [Fact]
   public void Icao9303Algorithm_ValidateField_ShouldReturnFalse_WhenStartIsLessThanZero()
      => _sut.Validate("7408122", -1, 7).Should().BeFalse();

   [Fact]
   public void Icao9303Algorithm_ValidateField_ShouldReturnFalse_WhenStartExceedsValueLength()
      => _sut.Validate("7408122", 21, 7).Should().BeFalse();

   [Fact]
   public void Icao9303Algorithm_ValidateField_ShouldReturnFalse_WhenStartPlusLengthExceedsValueLength()
      => _sut.Validate("7408122", 0, 14).Should().BeFalse();

   [Fact]
   public void Icao9303Algorithm_ValidateField_ShouldReturnFalse_WhenLengthIsLessThanTwo()
      => _sut.Validate("7408122", 0, 1).Should().BeFalse();

   [Theory]
   [InlineData("<0000+")]
   [InlineData("<0011+")]
   [InlineData("<0022+")]
   [InlineData("<0033+")]
   [InlineData("<0044+")]
   [InlineData("<0055+")]
   [InlineData("<0066+")]
   [InlineData("<0077+")]
   [InlineData("<0088+")]
   [InlineData("<0099+")]
   [InlineData("<00A0+")]
   [InlineData("<00B1+")]
   [InlineData("<00C2+")]
   [InlineData("<00D3+")]
   [InlineData("<00E4+")]
   [InlineData("<00F5+")]
   [InlineData("<00G6+")]
   [InlineData("<00H7+")]
   [InlineData("<00I8+")]
   [InlineData("<00J9+")]
   [InlineData("<00K0+")]
   [InlineData("<00L1+")]
   [InlineData("<00M2+")]
   [InlineData("<00N3+")]
   [InlineData("<00O4+")]
   [InlineData("<00P5+")]
   [InlineData("<00Q6+")]
   [InlineData("<00R7+")]
   [InlineData("<00S8+")]
   [InlineData("<00T9+")]
   [InlineData("<00U0+")]
   [InlineData("<00V1+")]
   [InlineData("<00W2+")]
   [InlineData("<00X3+")]
   [InlineData("<00Y4+")]
   [InlineData("<00Z5+")]
   [InlineData("<00<0+")]
   public void Icao9303Algorithm_ValidateField_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value, 1, 4).Should().BeTrue();

   [Theory]
   [InlineData("<1007+", 4)]
   [InlineData("<0103+", 4)]
   [InlineData("<0011+", 4)]
   [InlineData("<0001007+", 7)]
   [InlineData("<0000103+", 7)]
   [InlineData("<0000011+", 7)]
   [InlineData("<0000001007+", 10)]
   [InlineData("<0000000103+", 10)]
   [InlineData("<0000000011+", 10)]
   public void Icao9303Algorithm_ValidateField_ShouldCorrectlyWeightByCharacterPosition(String value, Int32 length)
      => _sut.Validate(value, 1, length).Should().BeTrue();

   public static TheoryData<String, Int32, Int32> ValidMrpLine2Data => new()
   {
      { _mrpLine2Value, 0, 10 },    // Passport number field
      { _mrpLine2Value, 13, 7 },    // Date of birth field
      { _mrpLine2Value, 21, 7 },    // Date of expiry field
      { _mrpLine2Value, 28, 15 }    // Other personal data field
   };

   [Theory]
   [MemberData(nameof(ValidMrpLine2Data))]
   public void Icao9303Algorithm_ValidateField_ShouldReturnTrue_WhenFieldContainsValidCheckDigit(
      String value,
      Int32 start,
      Int32 length)
      => _sut.Validate(value, start, length).Should().BeTrue();

   [Theory]
   [InlineData("+L8989A2C36>", 10)]      // L898902C36 with single char transcription error (0 -> A) with delta 10
   [InlineData("+L89890C236>", 10)]      // L898902C36 with two char transposition error (2C -> C2) with delta 10
   [InlineData("+8812728>", 7)]          // 8812278 with two char transposition error (27 -> 72) with delta 5
   public void Icao9303Algorithm_ValidateField_ShouldReturnTrue_WhenValueContainsUndetectableError(
      String value,
      Int32 length)
      => _sut.Validate(value, 1, length).Should().BeTrue();

   [Theory]
   [InlineData("+7438122", 7)]          // 7408122 with single digit transcription error (0 -> 3)
   [InlineData("+L898902D36", 10)]      // L898902C36 with single char transcription error (C -> D)
   [InlineData("+7480122", 7)]          // 7408122 with two digit transposition error (08 -> 80)
   [InlineData("+L8989023C6", 10)]      // L898902C36 with two char transposition error (3C -> C3)
   public void Icao9303Algorithm_ValidateField_ShouldReturnFalse_WhenInputContainsDetectableError(
      String value,
      Int32 length)
      => _sut.Validate(value, 1, length).Should().BeFalse();

   [Fact]
   public void Icao9303Algorithm_ValidateField_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("+0000<", 1, 4).Should().BeTrue();

   [Fact]
   public void Icao9303Algorithm_ValidateField_ShouldReturnTrue_WhenInputIsAllFillerCharacters()
      => _sut.Validate("+<<<0<", 1, 4).Should().BeTrue();

   [Fact]
   public void Icao9303Algorithm_ValidateField_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("+1030+", 1, 4).Should().BeTrue();

   [Theory]
   [InlineData("+74)8122+", 7)]         // 7438122 with 3 replaced with character 10 positions before in ASCII table
   [InlineData("+74=8122+", 7)]         // 7438122 with 3 replaced with character 10 positions later in ASCII table
   [InlineData("+`898902C36+", 10)]     // L898902C36 with L replaced with character 20 positions later in ASCII table
   public void Icao9303Algorithm_ValidateField_ShouldReturnFalse_WhenInputContainsInvalidCharacter(
      String value,
      Int32 length)
      => _sut.Validate(value, 1, length).Should().BeFalse();

   #endregion
}
