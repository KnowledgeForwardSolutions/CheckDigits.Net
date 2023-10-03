namespace CheckDigits.Net.Tests.Unit;

public class Modulus10_13AlgorithmTests
{
   private readonly Modulus10_13Algorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_13Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Modulus10_13AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_13Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Modulus10_13AlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_13Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Modulus10_13Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("00000000001", '7')]
   [InlineData("00000000100", '7')]
   [InlineData("00000010000", '7')]
   public void Modulus10_13Algorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightOddPositionCharacters(
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
   public void Modulus10_13Algorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightEvenPositionCharacters(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("03600029145", '2')]          // Worked UPC-A example from Wikipedia (https://en.wikipedia.org/wiki/Universal_Product_Code#Check_digit_calculation)
   [InlineData("42526", '1')]                // UPC-E example
   [InlineData("400638133393", '1')]         // Worked EAN-13 example from Wikipedia (https://en.wikipedia.org/wiki/International_Article_Number)
   [InlineData("7351353", '7')]              // Worked EAN-8 example from Wikipedia
   [InlineData("978050051695", '9')]         // ISBN-13, Islamic Geometric Design, Eric Broug
   [InlineData("01234567800004567", '8')]    // Example SSCC number
   public void Modulus10_13Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void Modulus10_13Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "00000";
      var expectedCheckDigit = CharConstants.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("42I26")]      // Value 42526 would have check digit = 1. I is 20 positions later in ASCII table than 5 so test will fail unless code explicitly checks for non-digit
   [InlineData("42+26")]      // + is 10 positions earlier in ASCII table than 5 so test will fail unless code explicitly checks for non-digit
   [InlineData("0 36000 29145")]
   public void Modulus10_13Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus10_13Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Modulus10_13Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]
   [InlineData("1")]
   public void Modulus10_13Algorithm_Validate_ShouldReturnFalse_WhenInputIsOneCharacterInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("000000000017")]
   [InlineData("000000001007")]
   [InlineData("000000100007")]
   public void Modulus10_13Algorithm_Validate_ShouldCorrectlyWeightOddPositionCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("000000000109")]
   [InlineData("000000010009")]
   [InlineData("000001000009")]
   public void Modulus10_13Algorithm_Validate_ShouldCorrectlyWeightEvenPositionCharacters(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("036000291452")]        // Worked UPC-A example from Wikipedia (https://en.wikipedia.org/wiki/Universal_Product_Code#Check_digit_calculation)
   [InlineData("425261")]              // UPC-E example
   [InlineData("4006381333931")]       // Worked EAN-13 example from Wikipedia (https://en.wikipedia.org/wiki/International_Article_Number)
   [InlineData("73513537")]            // Worked EAN-8 example from Wikipedia
   [InlineData("9780500516959")]       // ISBN-13, Islamic Geometric Design, Eric Broug
   [InlineData("012345678000045678")]  // Example SSCC number
   public void Modulus10_13Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("4006831333931")]       // EAN-13 with two digit transposition error (38 -> 83) where difference between digits is 5 
   [InlineData("9785000516959")]       // ISBN-13 with two digit transposition error (05 -> 50) where difference between digits is 5 
   [InlineData("73315537")]            // EAN-8 with jump transposition error (515 -> 315)
   [InlineData("012345876000045678")]  // SSCC number with jump transposition error (678 -> 876)
   public void Modulus10_13Algorithm_Validate_ShouldReturnTrue_WhenValueContainsUndetectableError(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("036000391452")]        // UPC-A with single digit transcription error (2 -> 3)
   [InlineData("427261")]              // UPC-E with single digit transcription error (5 -> 7)
   [InlineData("4006383133931")]       // EAN-13 with two digit transposition error (13 -> 31)
   [InlineData("9870500516959")]       // ISBN-13 with two digit transposition error (78 -> 87)
   public void Modulus10_13Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus10_13Algorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("000000000000").Should().BeTrue();

   [Fact]
   public void Modulus10_13Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsZero()
      => _sut.Validate("000000090100").Should().BeTrue();

   [Theory]
   [InlineData("42+261")]              // UPC-E example with 5 replaced with character 10 positions before in ASCII table
   [InlineData("42I261")]              // UPC-E example with 5 replaced with character 20 positions later in ASCII table
   [InlineData("0 36000 29145 2")]
   public void Modulus10_13Algorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
