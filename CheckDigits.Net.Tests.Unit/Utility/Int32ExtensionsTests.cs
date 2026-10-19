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
}
