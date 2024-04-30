// Ignore Spelling: Figi

namespace CheckDigits.Net.Tests.Unit.Utility;

public class CharacterMapUtilityTests
{
   #region GetFigiCharacterMap Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CharacterMapUtility_GetFigiCharacterMap_ShouldReturnNonNull()
      => CharacterMapUtility.GetFigiCharacterMap().Should().NotBeNull();

   private const Int32 _figiMapCount = 43;

   [Fact]
   public void CharacterMapUtility_GetFigiCharacterMap_ShouldContainExpectedNumberOfItems()
      => CharacterMapUtility.GetFigiCharacterMap().Should().HaveCount(_figiMapCount);

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
   public void CharacterMapUtility_GetFigiCharacterMap_ShouldCorrectlyPopulateArray(
      Char ch,
      Int32 expected)
   {
      // Arrange.
      var sut = CharacterMapUtility.GetFigiCharacterMap();
      var index = ch - CharConstants.DigitZero;

      // Act/assert.
      sut[index].Should().Be(expected);
   }

   #endregion
}
