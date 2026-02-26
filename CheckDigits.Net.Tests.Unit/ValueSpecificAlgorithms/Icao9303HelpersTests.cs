namespace CheckDigits.Net.Tests.Unit.ValueSpecificAlgorithms;

public class Icao9303HelpersTests
{
   #region MapCharacter Tests
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
   [InlineData('[', -1)]
   public void Icao9303Algorithm_MapCharacter_ShouldReturnExpectedValue(
      Char ch,
      Int32 expected)
      => Icao9303Helpers.MapCharacter(ch).Should().Be(expected);

   #endregion

}
