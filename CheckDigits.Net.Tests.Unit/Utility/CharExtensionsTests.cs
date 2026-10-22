namespace CheckDigits.Net.Tests.Unit.Utility;

public class CharExtensionsTests
{
   #region IsAsciiDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('0')]
   [InlineData('1')]
   [InlineData('2')]
   [InlineData('3')]
   [InlineData('4')]
   [InlineData('5')]
   [InlineData('6')]
   [InlineData('7')]
   [InlineData('8')]
   [InlineData('9')]
   public void CharExtensions_IsAsciiDigit_ShouldReturnTrue_WhenValueIsDigitCharacter(Char ch)
      => ch.IsAsciiDigit().Should().BeTrue();

   [Theory]
   [InlineData('/')]       // Immediately preceeding '0'
   [InlineData(':')]       // Immediately following '9'
   [InlineData('A')]
   [InlineData('z')]
   [InlineData('\u2153')]  // Non-digit character Unicode fraction 1/3
   [InlineData('\u0BE6')]  // Unicode Tamil digit 0
   public void CharExtensions_IsAsciiDigit_ShouldReturnFalse_WhenValueIsNotDigitCharacter(Char ch)
      => ch.IsAsciiDigit().Should().BeFalse();

   #endregion

   #region IsAsciiLetterUpper Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('A')]
   [InlineData('B')]
   [InlineData('C')]
   [InlineData('D')]
   [InlineData('E')]
   [InlineData('F')]
   [InlineData('G')]
   [InlineData('H')]
   [InlineData('I')]
   [InlineData('J')]
   [InlineData('K')]
   [InlineData('L')]
   [InlineData('M')]
   [InlineData('N')]
   [InlineData('O')]
   [InlineData('P')]
   [InlineData('Q')]
   [InlineData('R')]
   [InlineData('S')]
   [InlineData('T')]
   [InlineData('U')]
   [InlineData('V')]
   [InlineData('W')]
   [InlineData('X')]
   [InlineData('Y')]
   [InlineData('Z')]
   public void CharExtensions_IsAsciiLetterUpper_ShouldReturnTrue_WhenValueIsUppercaseLetter(Char ch)
      => ch.IsAsciiLetterUpper().Should().BeTrue();

   [Theory]
   [InlineData('/')]
   [InlineData('0')]
   [InlineData('9')]
   [InlineData(':')]
   [InlineData('a')]
   [InlineData('z')]
   [InlineData('\u2153')]  // Non-digit character Unicode fraction 1/3
   [InlineData('\u0BE6')]  // Unicode Tamil digit 0
   [InlineData('\u00D6')]  // Unicode O with umlaut
   public void CharExtensions_IsAsciiLetterUpper_ShouldReturnFalse_WhenValueIsNotUppercaseLetter(Char ch)
      => ch.IsAsciiLetterUpper().Should().BeFalse();

   #endregion

   #region MapAsciiUppercaseAlpha Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('A', 0)]
   [InlineData('B', 1)]
   [InlineData('C', 2)]
   [InlineData('D', 3)]
   [InlineData('E', 4)]
   [InlineData('F', 5)]
   [InlineData('G', 6)]
   [InlineData('H', 7)]
   [InlineData('I', 8)]
   [InlineData('J', 9)]
   [InlineData('K', 10)]
   [InlineData('L', 11)]
   [InlineData('M', 12)]
   [InlineData('N', 13)]
   [InlineData('O', 14)]
   [InlineData('P', 15)]
   [InlineData('Q', 16)]
   [InlineData('R', 17)]
   [InlineData('S', 18)]
   [InlineData('T', 19)]
   [InlineData('U', 20)]
   [InlineData('V', 21)]
   [InlineData('W', 22)]
   [InlineData('X', 23)]
   [InlineData('Y', 24)]
   [InlineData('Z', 25)]
   public void CharExtensions_MapAsciiUppercaseAlpha_ShouldReturnExpectedInteger_WhenInputIsUppercaseAsciiLetter(
      Char ch,
      Int32 expected)
      => ch.MapAsciiUppercaseAlpha().Should().Be(expected);

   [Theory]
   [InlineData('/')]
   [InlineData('0')]
   [InlineData('9')]
   [InlineData(':')]
   [InlineData('a')]
   [InlineData('z')]
   [InlineData('\u2153')]  // Non-digit character Unicode fraction 1/3
   [InlineData('\u0BE6')]  // Unicode Tamil digit 0
   [InlineData('\u00D6')]  // Unicode O with umlaut
   public void CharExtensions_MapAsciiUppercaseAlpha_ShouldReturnValueValueOutsideOfExpectedRange_WhenInputIsNotUppercaseAsciiLetter(Char ch)
      => ch.MapAsciiUppercaseAlpha().Should().NotBeInRange(0, 25);

   #endregion

   #region MapAsciiUppercaseAlphanumeric Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('0', 0)]
   [InlineData('1', 1)]
   [InlineData('2', 2)]
   [InlineData('3', 3)]
   [InlineData('4', 4)]
   [InlineData('5', 5)]
   [InlineData('6', 6)]
   [InlineData('7', 7)]
   [InlineData('8', 8)]
   [InlineData('9', 9)]
   [InlineData('A', 10)]
   [InlineData('B', 11)]
   [InlineData('C', 12)]
   [InlineData('D', 13)]
   [InlineData('E', 14)]
   [InlineData('F', 15)]
   [InlineData('G', 16)]
   [InlineData('H', 17)]
   [InlineData('I', 18)]
   [InlineData('J', 19)]
   [InlineData('K', 20)]
   [InlineData('L', 21)]
   [InlineData('M', 22)]
   [InlineData('N', 23)]
   [InlineData('O', 24)]
   [InlineData('P', 25)]
   [InlineData('Q', 26)]
   [InlineData('R', 27)]
   [InlineData('S', 28)]
   [InlineData('T', 29)]
   [InlineData('U', 30)]
   [InlineData('V', 31)]
   [InlineData('W', 32)]
   [InlineData('X', 33)]
   [InlineData('Y', 34)]
   [InlineData('Z', 35)]
   public void CharExtensions_MapAsciiUppercaseAlphanumeric_ShouldReturnExpectedInteger_WhenInputIsUppercaseAsciiAlphanunmeric(
      Char ch,
      Int32 expected)
      => ch.MapAsciiUppercaseAlphanumeric().Should().Be(expected);

   [Theory]
   [InlineData('/')]
   [InlineData(':')]
   [InlineData('a')]
   [InlineData('z')]
   [InlineData('\u2153')]  // Non-digit character Unicode fraction 1/3
   [InlineData('\u0BE6')]  // Unicode Tamil digit 0
   [InlineData('\u00D6')]  // Unicode O with umlaut
   public void CharExtensions_MapAsciiUppercaseAlphanumeric_ShouldMinusOne_WhenInputIsNotUppercaseAsciiAlphanunmeric(Char ch)
      => ch.MapAsciiUppercaseAlphanumeric().Should().Be(-1);

   #endregion

   #region MapDecimalDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('0', 0)]
   [InlineData('1', 1)]
   [InlineData('2', 2)]
   [InlineData('3', 3)]
   [InlineData('4', 4)]
   [InlineData('5', 5)]
   [InlineData('6', 6)]
   [InlineData('7', 7)]
   [InlineData('8', 8)]
   [InlineData('9', 9)]
   public void CharExtensions_MapDecimalDigit_ShouldReturnExpectedInteger_WhenInputIsDigitCharacter(Char ch, Int32 expected)
      => ch.MapDecimalDigit().Should().Be(expected);

   [Theory]
   [InlineData('+')]
   [InlineData('A')]
   public void CharExtensions_MapDecimalDigit_ShouldReturnValueNotBetweenZeroAndNine_WhenInputIsNotDigitCharacter(Char ch)
      => ch.MapDecimalDigit().Should().NotBeInRange(0, 9);

   #endregion

   #region MapExtendedDecimalDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('0', 0)]
   [InlineData('1', 1)]
   [InlineData('2', 2)]
   [InlineData('3', 3)]
   [InlineData('4', 4)]
   [InlineData('5', 5)]
   [InlineData('6', 6)]
   [InlineData('7', 7)]
   [InlineData('8', 8)]
   [InlineData('9', 9)]
   [InlineData('X', 10)]
   public void CharExtensions_MapExtendedDecimalDigit_ShouldReturnExpectedInteger_WhenInputIsDigitCharacter(Char ch, Int32 expected)
      => ch.MapExtendedDecimalDigit().Should().Be(expected);

   [Theory]
   [InlineData('+')]
   [InlineData('A')]
   public void CharExtensions_MapExtendedDecimalDigit_ShouldReturnValueNotBetweenZeroAndNine_WhenInputIsNotDigitCharacter(Char ch)
      => ch.MapExtendedDecimalDigit().Should().NotBeInRange(0, 10);

   #endregion
}
