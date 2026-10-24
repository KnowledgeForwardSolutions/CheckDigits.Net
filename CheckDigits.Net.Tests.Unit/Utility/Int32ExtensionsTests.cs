namespace CheckDigits.Net.Tests.Unit.Utility;

public class Int32ExtensionsTests
{
   #region IsValidDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(0)]
   [InlineData(1)]
   [InlineData(2)]
   [InlineData(3)]
   [InlineData(4)]
   [InlineData(5)]
   [InlineData(6)]
   [InlineData(7)]
   [InlineData(8)]
   [InlineData(9)]
   public void Int32Extensions_IsValidDigit_ShouldReturnTrue_WhenValueIsBetweenZeroAndNine(Int32 num)
      => num.IsValidDigit().Should().BeTrue();

   [Theory]
   [InlineData(-1)]
   [InlineData(-10)]
   [InlineData(Int32.MinValue)]
   public void Int32Extensions_IsValidDigit_ShouldReturnFalse_WhenValueIsLessThanZero(Int32 num)
      => num.IsValidDigit().Should().BeFalse();

   [Theory]
   [InlineData(10)]
   [InlineData(100)]
   [InlineData(Int32.MaxValue)]
   public void Int32Extensions_IsValidDigit_ShouldReturnFalse_WhenValueIsGreaterThanNine(Int32 num)
      => num.IsValidDigit().Should().BeFalse();

   #endregion

   #region IsValidExtendedDecimalCheckDigit Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(0)]
   [InlineData(1)]
   [InlineData(2)]
   [InlineData(3)]
   [InlineData(4)]
   [InlineData(5)]
   [InlineData(6)]
   [InlineData(7)]
   [InlineData(8)]
   [InlineData(9)]
   [InlineData(10)]
   public void Int32Extensions_IsValidExtendedDecimalCheckDigit_ShouldReturnTrue_WhenValueIsBetweenZeroAndTen(Int32 num)
      => num.IsValidExtendedDecimalCheckDigit().Should().BeTrue();

   [Theory]
   [InlineData(-1)]
   [InlineData(-10)]
   [InlineData(Int32.MinValue)]
   public void Int32Extensions_IsValidExtendedDecimalCheckDigit_ShouldReturnFalse_WhenValueIsLessThanZero(Int32 num)
      => num.IsValidExtendedDecimalCheckDigit().Should().BeFalse();

   [Theory]
   [InlineData(11)]
   [InlineData(100)]
   [InlineData(Int32.MaxValue)]
   public void Int32Extensions_IsValidExtendedDecimalCheckDigit_ShouldReturnFalse_WhenValueIsGreaterThanTen(Int32 num)
      => num.IsValidExtendedDecimalCheckDigit().Should().BeFalse();

   #endregion

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
