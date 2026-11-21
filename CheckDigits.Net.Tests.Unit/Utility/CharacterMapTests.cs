namespace CheckDigits.Net.Tests.Unit.Utility;

public class CharacterMapTests      // Note CharacterMap is an internal class of CheckDigits.Net
{
   #region CreateCharacterMap Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CharacterMap_CreateCharacterMap_ShouldThrowArgumentNullException_WhenMapParameterIsNull()
   {
      // Arrange.
      var start = Chars.DigitZero;
      var end = Chars.DigitNine;
      Func<Char, int> map = null!;

      // Act/assert.
      FluentActions
         .Invoking(() => CharacterMap.CreateCharacterMap(start, end, map))
         .Should().ThrowExactly<ArgumentNullException>()
         .WithParameterName(nameof(map));
   }

   [Fact]
   public void CharacterMap_CreateCharacterMap_ShouldReturnExpectedArray_WhenRangeIsAsciiDigits()
   {
      // Arrange.
      var start = Chars.DigitZero;
      var end = Chars.DigitNine;
      var map = (Char ch) => ch - Chars.DigitZero;

      Int32[] expected = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ];

      // Act.
      var result = CharacterMap.CreateCharacterMap(start, end, map);

      // Assert.
      result.Should().BeEquivalentTo(expected);
   }

   [Fact]
   public void CharacterMap_CreateCharacterMap_ShouldNormalizeBounds_WhenEndIsLessThanStart()
   {
      // Arrange.
      var start = Chars.DigitNine;
      var end = Chars.DigitZero;
      var map = (Char ch) => ch - Chars.DigitZero;

      Int32[] expected = [ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ];

      // Act.
      var result = CharacterMap.CreateCharacterMap(start, end, map);

      // Assert.
      result.Should().BeEquivalentTo(expected);
   }

   #endregion
}
