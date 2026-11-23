// Ignore Spelling: Iban

namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class IbanAlgorithmTests
{
   private readonly IbanAlgorithm _sut = new();

   #region AlgorithmDescription Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IbanAlgorithm_AlgorithmDescription_ShouldReturnExpectedValue()
      => _sut.AlgorithmDescription.Should().Be(Resources.IbanAlgorithmDescription);

   #endregion

   #region AlgorithmName Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IbanAlgorithm_AlgorithmName_ShouldReturnExpectedValue()
      => _sut.AlgorithmName.Should().Be(Resources.IbanAlgorithmName);

   #endregion

   #region TryCalculateCheckDigits Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IbanAlgorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsNull()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(null!, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Fact]
   public void IbanAlgorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsEmpty()
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(String.Empty, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("A")]
   [InlineData("AB")]
   [InlineData("AB4")]
   [InlineData("AB56")]
   public void IbanAlgorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputIsLessThanFiveCharactersInLength(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("AA000", '7', '5')]      // After moving first 4 chars to end and converting alpha to num, the value is 01010 + check digits
   [InlineData("AA001", '4', '8')]      // " => 11010 + check digits
   [InlineData("AA003", '9', '1')]      // " => 21010 + check digits
   [InlineData("AA004", '6', '4')]
   [InlineData("AA005", '3', '7')]
   [InlineData("AA006", '1', '0')]
   [InlineData("AA007", '8', '0')]
   [InlineData("AA008", '5', '3')]
   [InlineData("AA009", '2', '6')]
   [InlineData("AA00A", '9', '6')]      // " => 101010 + check digits
   [InlineData("AA00B", '6', '9')]      // " => 111010 + check digits
   [InlineData("AA00C", '4', '2')]      // " => 121010 + check digits
   [InlineData("AA00D", '1', '5')]
   [InlineData("AA00E", '8', '5')]
   [InlineData("AA00F", '5', '8')]
   [InlineData("AA00G", '3', '1')]
   [InlineData("AA00H", '0', '4')]
   [InlineData("AA00I", '7', '4')]
   [InlineData("AA00J", '4', '7')]
   [InlineData("AA00K", '2', '0')]
   [InlineData("AA00L", '9', '0')]
   [InlineData("AA00M", '6', '3')]
   [InlineData("AA00N", '3', '6')]
   [InlineData("AA00O", '0', '9')]
   [InlineData("AA00P", '7', '9')]
   [InlineData("AA00Q", '5', '2')]
   [InlineData("AA00R", '2', '5')]
   [InlineData("AA00S", '9', '5')]
   [InlineData("AA00T", '6', '8')]
   [InlineData("AA00U", '4', '1')]
   [InlineData("AA00V", '1', '4')]
   [InlineData("AA00W", '8', '4')]
   [InlineData("AA00X", '5', '7')]
   [InlineData("AA00Y", '3', '0')]
   [InlineData("AA00Z", '0', '3')]
   public void IbanAlgorithm_TryCalculateCheckDigits_ShouldCorrectlyMapCharacterValues(
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
   [InlineData("GB00WEST12345698765432", '8', '2')]                // Worked example from Wikipedia https://en.wikipedia.org/wiki/International_Bank_Account_Number
   [InlineData("AL00202111090000000001234567", '3', '5')]          // Example from https://www.iban.com/structure (Albania)
   [InlineData("BE00096123456769", '7', '1')]                      // " (Belgium)
   [InlineData("DO00ACAU00000000000123456789", '2', '2')]          // " (Dominican Republic)
   [InlineData("EG000002000156789012345180002", '8', '0')]         // " (Egypt)
   [InlineData("LU000010001234567891", '1', '2')]                  // " (Luxembourg)
   [InlineData("MU00BOMM0101123456789101000MUR", '4', '3')]        // " (Mauritius)
   [InlineData("SC00MCBL01031234567890123456USD", '7', '4')]       // " (Seychelles)
   public void IbanAlgorithm_TryCalculateCheckDigits_ShouldCalculateExpectedCheckDigit(
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
   [InlineData("AA00123!56")]
   [InlineData("AA00123^56")]
   [InlineData("AA00123=56")]
   [InlineData("AA00123a56")]
   [InlineData("AA00123\u215356")]        // Unicode fraction 1/3
   [InlineData("AA00123\u0BE656")]        // Unicode Tamil digit 0
   [InlineData("AA00123\u00D656")]        // Unicode uppercase O with umlaut
   [InlineData("AA00123\u00F656")]        // Unicode lowercase o with umlaut
   public void IbanAlgorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
   {
      // Act/assert.
      _sut.TryCalculateCheckDigits(value, out var first, out var second).Should().BeFalse();
      first.Should().Be('\0');
      second.Should().Be('\0');
   }

   [Theory]
   [InlineData("GB!0WEST12345698765432", '8', '2')]
   [InlineData("GB^0WEST12345698765432", '8', '2')]
   [InlineData("GB=0WEST12345698765432", '8', '2')]
   [InlineData("GBa0WEST12345698765432", '8', '2')]
   [InlineData("GB\u21530WEST12345698765432", '8', '2')]    // Unicode fraction 1/3
   [InlineData("GB\u0BE60WEST12345698765432", '8', '2')]    // Unicode Tamil digit 0
   [InlineData("GB\u00D60WEST12345698765432", '8', '2')]    // Unicode uppercase O with umlaut
   [InlineData("GB\u00F60WEST12345698765432", '8', '2')]    // Unicode lowercase o with umlaut

   [InlineData("GB0!WEST12345698765432", '8', '2')]
   [InlineData("GB0^WEST12345698765432", '8', '2')]
   [InlineData("GB0=WEST12345698765432", '8', '2')]
   [InlineData("GB0aWEST12345698765432", '8', '2')]
   [InlineData("GB0\u2153WEST12345698765432", '8', '2')]    // Unicode fraction 1/3
   [InlineData("GB0\u0BE6WEST12345698765432", '8', '2')]    // Unicode Tamil digit 0
   [InlineData("GB0\u00D6WEST12345698765432", '8', '2')]    // Unicode uppercase O with umlaut
   [InlineData("GB0\u00F6WEST12345698765432", '8', '2')]    // Unicode lowercase o with umlaut
   public void IbanAlgorithm_TryCalculateCheckDigits_ShouldIgnoreCheckDigitPositionsEvenWhenInvalid(
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
   [InlineData("BE00096123456769")]
   [InlineData("GB00WEST12345698765432")]
   [InlineData("SC00MCBL01031234567890123456USD")]
   public void IbanAlgorithm_TryCalculateCheckDigits_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.TryCalculateCheckDigits(value, out _, out _).Should().BeTrue();

   #endregion

   #region Validate Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenInputIsNull()
      => _sut.Validate(null!).Should().BeFalse();

   [Fact]
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenInputIsEmpty()
      => _sut.Validate(String.Empty).Should().BeFalse();

   [Theory]
   [InlineData("A")]
   [InlineData("AB")]
   [InlineData("AB4")]
   [InlineData("AB56")]
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenInputIsLessThanFiveCharactersInLength(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("AA750")]      // After moving first 4 chars to end and converting alpha to num, the value is 01010 + check digits
   [InlineData("AA481")]      // " => 11010 + check digits
   [InlineData("AA913")]      // " => 21010 + check digits
   [InlineData("AA644")]
   [InlineData("AA375")]
   [InlineData("AA106")]
   [InlineData("AA807")]
   [InlineData("AA538")]
   [InlineData("AA269")]
   [InlineData("AA96A")]      // " => 101010 + check digits
   [InlineData("AA69B")]      // " => 111010 + check digits
   [InlineData("AA42C")]      // " => 121010 + check digits
   [InlineData("AA15D")]
   [InlineData("AA85E")]
   [InlineData("AA58F")]
   [InlineData("AA31G")]
   [InlineData("AA04H")]
   [InlineData("AA74I")]
   [InlineData("AA47J")]
   [InlineData("AA20K")]
   [InlineData("AA90L")]
   [InlineData("AA63M")]
   [InlineData("AA36N")]
   [InlineData("AA09O")]
   [InlineData("AA79P")]
   [InlineData("AA52Q")]
   [InlineData("AA25R")]
   [InlineData("AA95S")]
   [InlineData("AA68T")]
   [InlineData("AA41U")]
   [InlineData("AA14V")]
   [InlineData("AA84W")]
   [InlineData("AA57X")]
   [InlineData("AA30Y")]
   [InlineData("AA03Z")]
   public void IbanAlgorithm_Validate_ShouldCorrectlyMapCharacterValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("GB82WEST12345698765432")]                // Worked example from Wikipedia https://en.wikipedia.org/wiki/International_Bank_Account_Number
   [InlineData("AL35202111090000000001234567")]          // Example from https://www.iban.com/structure (Albania)
   [InlineData("BE71096123456769")]                      // " (Belgium)
   [InlineData("DO22ACAU00000000000123456789")]          // " (Dominican Republic)
   [InlineData("EG800002000156789012345180002")]         // " (Egypt)
   [InlineData("LU120010001234567891")]                  // " (Luxembourg)
   [InlineData("MU43BOMM0101123456789101000MUR")]        // " (Mauritius)
   [InlineData("SC74MCBL01031234567890123456USD")]       // " (Seychelles)
   public void IbanAlgorithm_Validate_ShouldReturnTrue_WhenValueContainsValidCheckDigit(String value)
      => _sut.Validate(value).Should().BeTrue();

   [Theory]
   [InlineData("GB82WEST12345608765432")]                // GB82WEST12345698765432 with single digit transcription error 9 -> 0
   [InlineData("GB82WCST12345698765432")]                // GB82WEST12345698765432 with single char transcription error E -> C
   [InlineData("AL32502111090000000001234567")]          // AL35202111090000000001234567 with two digit transposition error 52 -> 25
   [InlineData("DO22ACUA00000000000123456789")]          // DO22ACAU00000000000123456789 with two char transposition error AU -> UA 
   [InlineData("EG800112000156789012345180002")]         // EG800002000156789012345180002 with two digit twin error 00 -> 11
   [InlineData("MU43BONN0101123456789101000MUR")]        // MU43BOMM0101123456789101000MUR with two char twin error MM -> NN
   [InlineData("SC74MCBL01021334567890123456USD")]       // SC74MCBL01031234567890123456USD with jump transposition error 312 -> 213
   [InlineData("SC74MC0LB1031234567890123456USD")]       // SC74MCBL01031234567890123456USD with jump transposition error BL0 -> 0LB
   [InlineData("SC74BCML01031234567890123456USD")]       // SC74MCBL01031234567890123456USD with jump transposition error MCB -> BCM
   [InlineData("U120010001234567891L")]                  // LU120010001234567891 with circular shift error
   [InlineData("1LU12001000123456789")]                  // LU120010001234567891 with circular shift error
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenInputContainsDetectableError(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("AA13!56")]
   [InlineData("AA13^56")]
   [InlineData("AA13=56")]
   [InlineData("AA13a56")]
   [InlineData("AA13\u215356")]     // Unicode fraction 1/3
   [InlineData("AA13\u0BE656")]     // Unicode Tamil digit 0
   [InlineData("AA13\u00D656")]     // Unicode uppercase O with umlaut
   [InlineData("AA13\u00F656")]     // Unicode lowercase o with umlaut
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("AA!0123")]
   [InlineData("AA^0123")]
   [InlineData("AA=0123")]
   [InlineData("AAa0123")]
   [InlineData("AA\u21530123")]     // Unicode fraction 1/3
   [InlineData("AA\u0BE60123")]     // Unicode Tamil digit 0
   [InlineData("AA\u00D60123")]     // Unicode uppercase O with umlaut
   [InlineData("AA\u00F60123")]     // Unicode lowercase o with umlaut
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("AA0!123")]
   [InlineData("AA0^123")]
   [InlineData("AA0=123")]
   [InlineData("AA0a123")]
   [InlineData("AA0\u2153123")]     // Unicode fraction 1/3
   [InlineData("AA0\u0BE6123")]     // Unicode Tamil digit 0
   [InlineData("AA0\u00D6123")]     // Unicode uppercase O with umlaut
   [InlineData("AA0\u00F6123")]     // Unicode lowercase o with umlaut
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Theory]
   [InlineData("BE71096123456769")]
   [InlineData("GB82WEST12345698765432")]
   [InlineData("SC74MCBL01031234567890123456USD")]
   public void IbanAlgorithm_Validate_ShouldReturnTrue_ForBenchmarkValues(String value)
      => _sut.Validate(value).Should().BeTrue();

   #endregion
}
