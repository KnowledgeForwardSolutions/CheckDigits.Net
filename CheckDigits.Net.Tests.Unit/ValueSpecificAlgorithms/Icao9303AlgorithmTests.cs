// Ignore Spelling: Icao Mrp

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303AlgorithmTests
{
   private readonly Icao9303Algorithm _sut = new();

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

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Icao9303Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Icao9303Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("000", '0')]
   [InlineData("001", '1')]
   [InlineData("002", '2')]
   [InlineData("003", '3')]
   [InlineData("004", '4')]
   [InlineData("005", '5')]
   [InlineData("006", '6')]
   [InlineData("007", '7')]
   [InlineData("008", '8')]
   [InlineData("009", '9')]
   [InlineData("00A", '0')]
   [InlineData("00B", '1')]
   [InlineData("00C", '2')]
   [InlineData("00D", '3')]
   [InlineData("00E", '4')]
   [InlineData("00F", '5')]
   [InlineData("00G", '6')]
   [InlineData("00H", '7')]
   [InlineData("00I", '8')]
   [InlineData("00J", '9')]
   [InlineData("00K", '0')]
   [InlineData("00L", '1')]
   [InlineData("00M", '2')]
   [InlineData("00N", '3')]
   [InlineData("00O", '4')]
   [InlineData("00P", '5')]
   [InlineData("00Q", '6')]
   [InlineData("00R", '7')]
   [InlineData("00S", '8')]
   [InlineData("00T", '9')]
   [InlineData("00U", '0')]
   [InlineData("00V", '1')]
   [InlineData("00W", '2')]
   [InlineData("00X", '3')]
   [InlineData("00Y", '4')]
   [InlineData("00Z", '5')]
   [InlineData("00<", '0')]
   public void Icao9303Algorithm_TryCalculateCheckDigit_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("100", '7')]
   [InlineData("010", '3')]
   [InlineData("001", '1')]
   [InlineData("000100", '7')]
   [InlineData("000010", '3')]
   [InlineData("000001", '1')]
   [InlineData("000000100", '7')]
   [InlineData("000000010", '3')]
   [InlineData("000000001", '1')]
   public void Icao9303Algorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightByCharacterPosition(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("740812", '2')]             // Example from https://www.icao.int/publications/Documents/9303_p4_cons_en.pdf
   [InlineData("L898902C3", '6')]          // "
   [InlineData("ZE184226B<<<<<", '1')]     // "
   public void Icao9303Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void Icao9303Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "000000";
      var expectedCheckDigit = Chars.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void Icao9303Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllFillerCharacters()
   {
      // Arrange.
      var value = "<<<<<<";
      var expectedCheckDigit = Chars.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("74)812")]              // 7408122 with 0 replaced with character 10 positions before in ASCII table
   [InlineData("74:812")]              // 7408122 with 0 replaced with character 10 positions later in ASCII table
   [InlineData("`898902C3")]           // L898902C36 with L replaced with character 20 positions later in ASCII table
   public void Icao9303Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be(Chars.NUL);
   }

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
   [InlineData("74)8122")]             // 7408122 with 0 replaced with character 10 positions before in ASCII table
   [InlineData("74:8122")]             // 7408122 with 0 replaced with character 10 positions later in ASCII table
   [InlineData("`898902C36")]          // L898902C36 with L replaced with character 20 positions later in ASCII table
   public void Icao9303Algorithm_Validate_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
