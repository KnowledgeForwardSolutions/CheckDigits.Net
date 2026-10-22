namespace CheckDigits.Net.Tests.Unit.Utility;

public class CharExtensionsTests
{
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
