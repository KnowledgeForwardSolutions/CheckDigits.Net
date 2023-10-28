namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class Iso7064Mod97_10AlgorithmTests
{
   private readonly Iso7064Mod97_10Algorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod97_10Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Iso7064Mod97_10AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod97_10Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Iso7064Mod97_10AlgorithmName);

   #endregion

   #region TryCalculateCheckDigits Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(null!, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Fact]
   public void Iso7064Mod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(String.Empty, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("0", '9', '8')]
   [InlineData("1", '9', '5')]
   [InlineData("2", '9', '2')]
   [InlineData("3", '8', '9')]
   [InlineData("4", '8', '6')]
   [InlineData("5", '8', '3')]
   [InlineData("6", '8', '0')]
   [InlineData("7", '7', '7')]
   [InlineData("8", '7', '4')]
   [InlineData("9", '7', '1')]
   public void Iso7064Mod97_10Algorithm_TryCalculateCheckDigits_ShouldCorrectlyMapCharacterValues(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("123456", '7', '6')]
   [InlineData("1632175818351910", '3', '8')]
   [InlineData("10113393912554329261011442299914333", '3', '8')]    // Example from https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#e7e616a4bd15acce7589cbedc4fd01fcc9623f60e4263be834c9e438
   public void Iso7064Mod97_10AlgorithmAlgorithm_TryCalculateCheckDigits_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedFirst,
      Char expectedSecond)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeTrue();
      first.Should().Be(expectedFirst);
      second.Should().Be(expectedSecond);
   }

   [Theory]
   [InlineData("123!56")]
   [InlineData("123^56")]
   [InlineData("123X56")]
   public void Iso7064Mod97_10Algorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   [InlineData("00")]
   [InlineData("01")]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputIsLessThanTwoCharactersInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("098")]
   [InlineData("195")]
   [InlineData("292")]
   [InlineData("389")]
   [InlineData("486")]
   [InlineData("583")]
   [InlineData("680")]
   [InlineData("777")]
   [InlineData("874")]
   [InlineData("971")]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("12345676")]
   [InlineData("163217581835191038")]
   [InlineData("1011339391255432926101144229991433338")]    // Example from https://www.consumerfinance.gov/rules-policy/regulations/1003/c/#e7e616a4bd15acce7589cbedc4fd01fcc9623f60e4263be834c9e438
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("163217541835191038")]     // 163217581835191038 with single char transcription error 8 -> 4
   [InlineData("163217581835191138")]     // 163217581835191038 with single char transcription error 0 -> 1
   [InlineData("12455676")]               // 12345676 with two char transposition error 34 -> 45 
   [InlineData("1022339391255432926101144229991433338")] // 1011339391255432926101144229991433338 with two char twin error 11 -> 22
   [InlineData("12365476")]               // 12345676 with jump transposition error 456 -> 654
   [InlineData("163217581538191038")]     // 163217581835191038 with jump transposition error 835 -> 538
   [InlineData("632175818351910381")]     // 163217581835191038 with circular shift error
   [InlineData("816321758183519103")]     // 163217581835191038 with circular shift error
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("123!56")]
   [InlineData("123^56")]
   [InlineData("123X56")]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter()
      => _sut.Validate("1234#0").Should().BeFalse();

   [Fact]
   public void Iso7064Mod97_10Algorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter()
      => _sut.Validate("12345#").Should().BeFalse();

   #endregion
}
