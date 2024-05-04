// Ignore Spelling: Figi Icao

namespace CheckDigits.Net.Tests.Unit.Utility;

public class CharacterMapUtilityTests
{
   private const Int32 _alphanumericCount = CharConstants.UpperCaseZ - CharConstants.DigitZero + 1;

   #region GetAlphanumericCharacterMap Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CharacterMapUtility_GetAlphanumericCharacterMap_ShouldReturnNonNull()
      => CharacterMapUtility.GetAlphanumericCharacterMap().Should().NotBeNull();

   [Fact]
   public void CharacterMapUtility_GetAlphanumericCharacterMap_ShouldContainExpectedNumberOfItems()
      => CharacterMapUtility.GetAlphanumericCharacterMap().Should().HaveCount(_alphanumericCount);

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
   public void CharacterMapUtility_GetAlphanumericCharacterMap_ShouldCorrectlyPopulateArray(
      Char ch,
      Int32 expected)
   {
      // Arrange.
      var sut = CharacterMapUtility.GetAlphanumericCharacterMap();
      var index = ch - CharConstants.DigitZero;

      // Act/assert.
      sut[index].Should().Be(expected);
   }

   #endregion

   #region GetIcao9303CharacterMap Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CharacterMapUtility_GetIcao9303CharacterMap_ShouldReturnNonNull()
      => CharacterMapUtility.GetIcao9303CharacterMap().Should().NotBeNull();

   [Fact]
   public void CharacterMapUtility_GetIcao9303CharacterMap_ShouldContainExpectedNumberOfItems()
      => CharacterMapUtility.GetIcao9303CharacterMap().Should().HaveCount(_alphanumericCount);

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
   [InlineData('<', 0)]
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
   public void CharacterMapUtility_GetIcao9303CharacterMap_ShouldCorrectlyPopulateArray(
      Char ch,
      Int32 expected)
   {
      // Arrange.
      var sut = CharacterMapUtility.GetIcao9303CharacterMap();
      var index = ch - CharConstants.DigitZero;

      // Act/assert.
      sut[index].Should().Be(expected);
   }
   #endregion

   #region GetFigiCharacterMap Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CharacterMapUtility_GetFigiCharacterMap_ShouldReturnNonNull()
      => CharacterMapUtility.GetFigiCharacterMap().Should().NotBeNull();

   [Fact]
   public void CharacterMapUtility_GetFigiCharacterMap_ShouldContainExpectedNumberOfItems()
      => CharacterMapUtility.GetFigiCharacterMap().Should().HaveCount(_alphanumericCount);

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

   #region GetVinCharacterMap Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CharacterMapUtility_GetVinCharacterMap_ShouldReturnNonNull()
      => CharacterMapUtility.GetVinCharacterMap().Should().NotBeNull();

   [Fact]
   public void CharacterMapUtility_GetVinCharacterMap_ShouldContainExpectedNumberOfItems()
      => CharacterMapUtility.GetVinCharacterMap().Should().HaveCount(_alphanumericCount);

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
   [InlineData('A', 1)]
   [InlineData('B', 2)]
   [InlineData('C', 3)]
   [InlineData('D', 4)]
   [InlineData('E', 5)]
   [InlineData('F', 6)]
   [InlineData('G', 7)]
   [InlineData('H', 8)]
   [InlineData('I', -1)]
   [InlineData('J', 1)]
   [InlineData('K', 2)]
   [InlineData('L', 3)]
   [InlineData('M', 4)]
   [InlineData('N', 5)]
   [InlineData('O', -1)]
   [InlineData('P', 7)]
   [InlineData('Q', -1)]
   [InlineData('R', 9)]
   [InlineData('S', 2)]
   [InlineData('T', 3)]
   [InlineData('U', 4)]
   [InlineData('V', 5)]
   [InlineData('W', 6)]
   [InlineData('X', 7)]
   [InlineData('Y', 8)]
   [InlineData('Z', 9)]
   public void CharacterMapUtility_GetVinCharacterMap_ShouldCorrectlyPopulateArray(
      Char ch,
      Int32 expected)
   {
      // Arrange.
      var sut = CharacterMapUtility.GetVinCharacterMap();
      var index = ch - CharConstants.DigitZero;

      // Act/assert.
      sut[index].Should().Be(expected);
   }

   #endregion
}
