namespace CheckDigits.Net.Tests.Unit.Utility;

public class CharExtensionsTests
{
   #region ToDecimalDigit Tests
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
   public void CharExtensions_ToDecimalDigit_ShouldReturnExpectedInteger_WhenInputIsDigitCharacter(Char ch, Int32 expected)
      => ch.ToDecimalDigit().Should().Be(expected);

   [Theory]
   [InlineData('+')]
   [InlineData('A')]
   public void CharExtensions_ToDecimalDigit_ShouldReturnValueNotBetweenZeroAndNine_WhenInputIsNotDigitCharacter(Char ch)
      => ch.ToDecimalDigit().Should().NotBeInRange(0, 9);

   #endregion

   #region ToExtendedDecimalDigit Tests
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
   public void CharExtensions_ToExtendedDecimalDigit_ShouldReturnExpectedInteger_WhenInputIsDigitCharacter(Char ch, Int32 expected)
      => ch.ToExtendedDecimalDigit().Should().Be(expected);

   [Theory]
   [InlineData('+')]
   [InlineData('A')]
   public void CharExtensions_ToExtendedDecimalDigit_ShouldReturnValueNotBetweenZeroAndNine_WhenInputIsNotDigitCharacter(Char ch)
      => ch.ToExtendedDecimalDigit().Should().NotBeInRange(0, 10);

   #endregion
}
