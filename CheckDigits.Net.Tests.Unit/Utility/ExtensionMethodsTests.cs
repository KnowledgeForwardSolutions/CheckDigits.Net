namespace CheckDigits.Net.Tests.Unit.Utility;

public class ExtensionMethodsTests
{
   #region ToDigitChar Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(0, '0')]
   [InlineData(1, '1')]
   [InlineData(2, '2')]
   [InlineData(3, '3')]
   [InlineData(4, '4')]
   [InlineData(5, '5')]
   [InlineData(6, '6')]
   [InlineData(7, '7')]
   [InlineData(8, '8')]
   [InlineData(9, '9')]
   public void ExtensionMethods_ToIntegerDigit_ShouldReturnExpectedCharacter_WhenInputIsZeroToNine(Int32 num, Char expected)
      => num.ToDigitChar().Should().Be(expected);

   [Theory]
   [InlineData(-5)]
   [InlineData(15)]
   public void ExtensionMethods_ToIntegerDigit_ShouldReturnNonDigitCharacter_WhenInputIsNotZeroToNine(Int32 num)
      => num.ToDigitChar().Should().NotBeInRange('0', '9');

   #endregion

   #region ToIntegerDigit Tests
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
   public void ExtensionMethods_ToIntegerDigit_ShouldReturnExpectedInteger_WhenInputIsDigitCharacter(Char ch, Int32 expected)
      => ch.ToIntegerDigit().Should().Be(expected);

   [Theory]
   [InlineData('+')]
   [InlineData('A')]
   public void ExtensionMethods_ToIntegerDigit_ShouldReturnValueNotBetweenZeroAndNine_WhenInputIsNotDigitCharacter(Char ch)
      => ch.ToIntegerDigit().Should().NotBeInRange(0, 9);

   #endregion
}
