// Ignore Spelling: Icao

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class IcaoAlgorithmTests
{
   private readonly IcaoAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_731Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.IcaoAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_731Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.IcaoAlgorithmName);

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_731Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Modulus10_731Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
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
   public void Modulus10_731Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
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
   public void Modulus10_731Algorithm_Validate_ShouldCorrectlyWeightByCharacterPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   public void Modulus10_731Algorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("7408122")]             // Example from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf
   [InlineData("L898902C36")]          // "
   public void Modulus10_731Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("L8989A2C36")]          // L898902C36 with single char transcription error (0 -> A)
   [InlineData("L89890C236")]          // L898902C36 with two char transposition error (2C -> C2)
   public void Modulus10_731Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("7438122")]             // 7408122 with single digit transcription error (0 -> 3)
   [InlineData("L898902D36")]          // L898902C36 with single char transcription error (C -> D)
   [InlineData("7480122")]             // 7408122 with two digit transposition error (08 -> 80)
   [InlineData("L8989023C6")]          // L898902C36 with two char transposition error (3C -> C3)
   public void Modulus10_731Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus10_731Algorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("0000").Should().BeTrue();

   [Fact]
   public void Modulus10_731Algorithm_Validate_ShouldReturnTrue_WhenInputIsAllFillerCharacters()
      => _sut.Validate("<<<0").Should().BeTrue();

   [Fact]
   public void Modulus10_731Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("1030").Should().BeTrue();

   [Theory]
   [InlineData("74)8122")]             // 7438122 with 3 replaced with character 10 positions before in ASCII table
   [InlineData("74=8122")]             // 7438122 with 3 replaced with character 10 positions later in ASCII table
   [InlineData("`898902C36")]          // L898902C36 with L replaced with character 20 positions later in ASCII table
   public void Modulus10_731Algorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
