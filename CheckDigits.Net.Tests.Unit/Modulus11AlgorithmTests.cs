namespace CheckDigits.Net.Tests.Unit;

public class Modulus11AlgorithmTests
{
   private readonly Modulus11Algorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11Algorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.Modulus11AlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11Algorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.Modulus11AlgorithmName);

   #endregion

   #region TryCalculateCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(null!, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Modulus11Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(String.Empty, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Fact]
   public void Modulus11Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputHasLengthGreaterThanEleven()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit("1234567890", out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   [Theory]
   [InlineData("000000001", '9')]
   [InlineData("000000010", '8')]
   [InlineData("000000100", '7')]
   [InlineData("000001000", '6')]
   [InlineData("000010000", '5')]
   [InlineData("000100000", '4')]
   [InlineData("001000000", '3')]
   [InlineData("010000000", '2')]
   [InlineData("100000000", '1')]
   public void Modulus11Algorithm_TryCalculateCheckDigit_ShouldCorrectlyWeightCharactersByPosition(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      expectedCheckDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("156865652", '1')]    // ISBN-10 Island in the Stream of Time, S. M. Sterling
   [InlineData("044100560", '8')]    // ISBN-10 The Warlock in Spite of Himself, Christopher Stasheff
   [InlineData("071410544", '9')]    // ISBN-10 The Sutton Hoo Ship Burial, Angela Care Evans
   [InlineData("050027293", 'X')]    // ISBN-10 Roman London, Peter Marsden
   //
   [InlineData("030640615", '2')]    // Worked example of ISBN-10 from Wikipedia https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
   [InlineData("0378595", '5')]      // Worked example of ISSN from Wikipedia https://en.wikipedia.org/wiki/ISSN
   [InlineData("2434561", 'X')]      // Example ISSN from Wikipedia
   [InlineData("0317847", '1')]      // Example ISSN from https://www.issn.org/understanding-the-issn/what-is-an-issn/
   [InlineData("1050124", 'X')]      // "
   public void Modulus11Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit(
      String value,
      Char expectedCheckDigit)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Fact]
   public void Modulus11Algorithm_TryCalculateCheckDigit_ShouldCalculateExpectedCheckDigit_WhenInputIsAllZeros()
   {
      // Arrange.
      var value = "00000";
      var expectedCheckDigit = CharConstants.DigitZero;

      // Act/assert.
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeTrue();
      checkDigit.Should().Be(expectedCheckDigit);
   }

   [Theory]
   [InlineData("100G000001")]
   [InlineData("100+000001")]
   public void Modulus11Algorithm_TryCalculateCheckDigit_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
   {
      _sut.TryCalculateCheckDigit(value, out var checkDigit).Should().BeFalse();
      checkDigit.Should().Be('\0');
   }

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Modulus11Algorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void Modulus11Algorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("0")]       // Zero would return true unless length is explicitly checked.
   [InlineData("1")]
   public void Modulus11Algorithm_Validate_ShouldReturnFalse_WhenInputHasLengthLessThanTwo(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus11Algorithm_Validate_ShouldReturnFalse_WhenInputHasLengthGreaterThanEleven()
      => _sut.Validate("12345678901").Should().BeFalse();

   [Theory]
   [InlineData("0000000019")]
   [InlineData("0000000108")]
   [InlineData("0000001007")]
   [InlineData("0000010006")]
   [InlineData("0000100005")]
   [InlineData("0001000004")]
   [InlineData("0010000003")]
   [InlineData("0100000002")]
   [InlineData("1000000001")]
   public void Modulus11Algorithm_Validate_ShouldCorrectlyWeightCharactersByPosition(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1568656521")]    // ISBN-10 Island in the Stream of Time, S. M. Sterling
   [InlineData("0441005608")]    // ISBN-10 The Warlock in Spite of Himself, Christopher Stasheff
   [InlineData("0714105449")]    // ISBN-10 The Sutton Hoo Ship Burial, Angela Care Evans
   [InlineData("050027293X")]    // ISBN-10 Roman London, Peter Marsden
   //
   [InlineData("0306406152")]    // Worked example of ISBN-10 from Wikipedia https://en.wikipedia.org/wiki/ISBN#ISBN-10_check_digits
   [InlineData("03785955")]      // Worked example of ISSN from Wikipedia https://en.wikipedia.org/wiki/ISSN
   [InlineData("2434561X")]      // Example ISSN from Wikipedia
   [InlineData("03178471")]      // Example ISSN from https://www.issn.org/understanding-the-issn/what-is-an-issn/
   [InlineData("1050124X")]      // "
   public void Modulus11Algorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("1568646521")]    // ISBN-10 with single digit transcription error 5 -> 4
   [InlineData("0441050608")]    // ISBN-10 with two digit transposition error 05 -> 50
   [InlineData("050029273X")]    // ISBN-10 with jump transposition 729 -> 927
   [InlineData("0551005608")]    // ISBN-10 with twin error 44 -> 55
   public void Modulus11Algorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void Modulus11Algorithm_Validate_ShouldReturnTrue_WhenInputIsAllZeros()
      => _sut.Validate("0000000000").Should().BeTrue();

   [Fact]
   public void Modulus11Algorithm_Validate_ShouldReturnTrue_WhenCheckDigitIsCalculatesAsX()
      => _sut.Validate("010000010X").Should().BeTrue();

   [Theory]
   [InlineData("1000G00005")]    // Value 1000300005 would have check digit = 5. G is 20 positions later in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
   [InlineData("1000)00005")]    // ) is 10 positions earlier in ASCII table than 3 and would also calculate check digit 5 unless code explicitly checks for non-digit
   public void Modulus11Algorithm_Validate_ShouldReturnFalse_WhenInputContainsNonDigitCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   #endregion
}
