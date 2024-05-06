// Ignore Spelling: Betanumeric

namespace CheckDigits.Net.Tests.Unit.Utility;

public class CharsTests
{
   #region MapAlphanumericCharacter Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('\0', -1)]
   [InlineData('/', -1)]
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
   [InlineData(':', -1)]
   [InlineData(';', -1)]
   [InlineData('<', -1)]
   [InlineData('=', -1)]
   [InlineData('>', -1)]
   [InlineData('?', -1)]
   [InlineData('@', -1)]
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
   [InlineData('[', -1)]
   public void Chars_MapAlphanumericCharacter_ShouldReturnExpectedValue(
      Char ch,
      Int32 expected)
      => Chars.MapAlphanumericCharacter(ch).Should().Be(expected);

   #endregion

   #region MapBetanumericCharacter Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData('\0', -1)]
   [InlineData('/', -1)]
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
   [InlineData(':', -1)]
   [InlineData(';', -1)]
   [InlineData('<', -1)]
   [InlineData('=', -1)]
   [InlineData('>', -1)]
   [InlineData('?', -1)]
   [InlineData('@', -1)]
   [InlineData('A', -1)]
   [InlineData('B', 11)]
   [InlineData('C', 12)]
   [InlineData('D', 13)]
   [InlineData('E', -1)]
   [InlineData('F', 15)]
   [InlineData('G', 16)]
   [InlineData('H', 17)]
   [InlineData('I', -1)]
   [InlineData('J', 19)]
   [InlineData('K', 20)]
   [InlineData('L', 21)]
   [InlineData('M', 22)]
   [InlineData('N', 23)]
   [InlineData('O', -1)]
   [InlineData('P', 25)]
   [InlineData('Q', 26)]
   [InlineData('R', 27)]
   [InlineData('S', 28)]
   [InlineData('T', 29)]
   [InlineData('U', -1)]
   [InlineData('V', 31)]
   [InlineData('W', 32)]
   [InlineData('X', 33)]
   [InlineData('Y', 34)]
   [InlineData('Z', 35)]
   [InlineData('[', -1)]
   public void Chars_MapBetanumericCharacter_ShouldReturnExpectedValue(
      Char ch,
      Int32 expected)
      => Chars.MapBetanumericCharacter(ch).Should().Be(expected);

   #endregion

   #region Range Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(Chars.DigitZero, Chars.DigitNine, "0123456789")]
   [InlineData(Chars.UpperCaseA, Chars.UpperCaseZ, "ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
   [InlineData(Chars.DigitZero, Chars.UpperCaseZ, "0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
   public void Chars_Range_ShouldReturnExpectedCollection(
      Char start,
      Char end,
      String expected)
   {
      // Arrange.
      var expectedChars = expected.ToCharArray();

      // Act.
      var range = Chars.Range(start, end).ToArray();

      // Assert.
      range.Should().HaveCount(expectedChars.Length);
      range.Should().ContainInConsecutiveOrder(expectedChars);
   }

   [Fact]
   public void Chars_Range_ShouldNormalizeStartAndEndIfNecessary()
   {
      // Arrange.
      var start = Chars.UpperCaseC;
      var end = Chars.UpperCaseA;
      var expectedChars = "ABC".ToCharArray();

      // Act.
      var range = Chars.Range(start, end).ToArray();

      // Assert.
      range.Should().HaveCount(expectedChars.Length);
      range.Should().ContainInConsecutiveOrder(expectedChars);
   }

   #endregion
}
