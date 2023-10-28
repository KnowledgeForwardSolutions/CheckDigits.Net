// Ignore Spelling: Iban

using System.Runtime.CompilerServices;

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
      _sut.TryCalculateCheckDigits(null!, out var first, out var second).Should().BeFalse();
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
   [InlineData("AA__0", '7', '5')]      // After moving first 4 chars to end and converting alpha to num, the value is 01010 + check digits
   [InlineData("AA__1", '4', '8')]      // " => 11010 + check digits
   [InlineData("AA__3", '9', '1')]      // " => 21010 + check digits
   [InlineData("AA__4", '6', '4')]
   [InlineData("AA__5", '3', '7')]
   [InlineData("AA__6", '1', '0')]
   [InlineData("AA__7", '8', '0')]
   [InlineData("AA__8", '5', '3')]
   [InlineData("AA__9", '2', '6')]
   [InlineData("AA__A", '9', '6')]      // " => 101010 + check digits
   [InlineData("AA__B", '6', '9')]      // " => 111010 + check digits
   [InlineData("AA__C", '4', '2')]      // " => 121010 + check digits
   [InlineData("AA__D", '1', '5')]
   [InlineData("AA__E", '8', '5')]
   [InlineData("AA__F", '5', '8')]
   [InlineData("AA__G", '3', '1')]
   [InlineData("AA__H", '0', '4')]
   [InlineData("AA__I", '7', '4')]
   [InlineData("AA__J", '4', '7')]
   [InlineData("AA__K", '2', '0')]
   [InlineData("AA__L", '9', '0')]
   [InlineData("AA__M", '6', '3')]
   [InlineData("AA__N", '3', '6')]
   [InlineData("AA__O", '0', '9')]
   [InlineData("AA__P", '7', '9')]
   [InlineData("AA__Q", '5', '2')]
   [InlineData("AA__R", '2', '5')]
   [InlineData("AA__S", '9', '5')]
   [InlineData("AA__T", '6', '8')]
   [InlineData("AA__U", '4', '1')]
   [InlineData("AA__V", '1', '4')]
   [InlineData("AA__W", '8', '4')]
   [InlineData("AA__X", '5', '7')]
   [InlineData("AA__Y", '3', '0')]
   [InlineData("AA__Z", '0', '3')]
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
   [InlineData("GB__WEST12345698765432", '8', '2')]                // Worked example from Wikipedia https://en.wikipedia.org/wiki/International_Bank_Account_Number
   [InlineData("AL__202111090000000001234567", '3', '5')]          // Example from https://www.iban.com/structure (Albania)
   [InlineData("BE__096123456769", '7', '1')]                      // " (Belgium)
   [InlineData("DO__ACAU00000000000123456789", '2', '2')]          // " (Dominican Republic)
   [InlineData("EG__0002000156789012345180002", '8', '0')]         // " (Egypt)
   [InlineData("LU__0010001234567891", '1', '2')]                  // " (Luxembourg)
   [InlineData("MU__BOMM0101123456789101000MUR", '4', '3')]        // " (Mauritius)
   [InlineData("SC__MCBL01031234567890123456USD", '7', '4')]       // " (Seychelles)
   public void IbanAlgorithmAlgorithm_TryCalculateCheckDigits_ShouldCalculateExpectedCheckDigit(
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
   [InlineData("AA__123!56")]
   [InlineData("AA__123^56")]
   [InlineData("AA__123=56")]
   public void IbanAlgorithm_TryCalculateCheckDigits_ShouldReturnFalse_WhenInputContainsInvalidCharacter(String value)
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
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidCharacter(String value)
      => _sut.Validate(value).Should().BeFalse();

   [Fact]
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidFirstCheckCharacter()
      => _sut.Validate("AA#0123").Should().BeFalse();

   [Fact]
   public void IbanAlgorithm_Validate_ShouldReturnFalse_WhenValueContainsInvalidSecondCheckCharacter()
      => _sut.Validate("AA0#123").Should().BeFalse();

   #endregion
}
