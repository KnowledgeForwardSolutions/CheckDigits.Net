namespace CheckDigits.Net.Tests.Unit.Utility;

public class Int32ExtensionsTests
{
   #region ToAsciiDigit Tests
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
   public void Int32Extensions_ToAsciiDigit_ShouldReturnExpectedCharacter_WhenInputIsZeroToNine(Int32 num, Char expected)
      => num.ToAsciiDigit().Should().Be(expected);

   [Theory]
   [InlineData(-5)]
   [InlineData(15)]
   public void Int32Extensions_ToAsciiDigit_ShouldReturnNonDigitCharacter_WhenInputIsNotZeroToNine(Int32 num)
      => num.ToAsciiDigit().Should().NotBeInRange('0', '9');

   #endregion

   #region ToAsciiExtendedDecimalDigit Tests
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
   [InlineData(10, 'X')]
   public void Int32Extensions_ToAsciiExtendedDecimalDigit_ShouldReturnExpectedCharacter_WhenInputIsZeroToTen(Int32 num, Char expected)
      => num.ToAsciiExtendedDecimalDigit().Should().Be(expected);

   [Theory]
   [InlineData(-5)]
   [InlineData(15)]
   public void Int32Extensions_ToAsciiExtendedDecimalDigit_ShouldReturnNonDigitCharacter_WhenInputIsNotZeroToTen(Int32 num)
      => num.ToAsciiExtendedDecimalDigit().Should().NotBeInRange('0', '9')
            .And.NotBe('X');

   #endregion
}
