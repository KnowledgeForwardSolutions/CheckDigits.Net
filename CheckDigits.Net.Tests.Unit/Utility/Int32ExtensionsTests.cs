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

   #region ToAsciiUppercaseAlpha Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(0, 'A')]
   [InlineData(1, 'B')]
   [InlineData(2, 'C')]
   [InlineData(3, 'D')]
   [InlineData(4, 'E')]
   [InlineData(5, 'F')]
   [InlineData(6, 'G')]
   [InlineData(7, 'H')]
   [InlineData(8, 'I')]
   [InlineData(9, 'J')]
   [InlineData(10, 'K')]
   [InlineData(11, 'L')]
   [InlineData(12, 'M')]
   [InlineData(13, 'N')]
   [InlineData(14, 'O')]
   [InlineData(15, 'P')]
   [InlineData(16, 'Q')]
   [InlineData(17, 'R')]
   [InlineData(18, 'S')]
   [InlineData(19, 'T')]
   [InlineData(20, 'U')]
   [InlineData(21, 'V')]
   [InlineData(22, 'W')]
   [InlineData(23, 'X')]
   [InlineData(24, 'Y')]
   [InlineData(25, 'Z')]
   public void Int32Extensions_ToAsciiUppercaseAlpha_ShouldReturnExpectedCharacter_WhenInputIsZeroTo25(Int32 num, Char expected)
      => num.ToAsciiUppercaseAlpha().Should().Be(expected);

   [Theory]
   [InlineData(-5)]
   [InlineData(26)]
   public void Int32Extensions_ToAsciiUppercaseAlpha_ShouldReturnNonDigitCharacter_WhenInputIsNotZeroTo25(Int32 num)
      => num.ToAsciiUppercaseAlpha().Should().NotBeInRange('0', '9')
         .And.NotBe('X');

   #endregion
}
