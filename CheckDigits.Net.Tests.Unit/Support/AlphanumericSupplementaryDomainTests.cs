namespace CheckDigits.Net.Tests.Unit.Support;

public class AlphanumericSupplementaryDomainTests
{
   private readonly AlphanumericSupplementaryDomain _sut = new();

   #region CheckCharacters Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AlphanumericSupplementaryDomain_CheckCharacters_ShouldReturnExpectedValue()
      => _sut.CheckCharacters.Should().Be("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*");

   #endregion

   #region ValidCharacters Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void AlphanumericSupplementaryDomain_ValidCharacters_ShouldReturnExpectedValue()
      => _sut.ValidCharacters.Should().Be("0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ");

   #endregion

   #region GetCheckCharacter Tests
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
   [InlineData(10, 'A')]
   [InlineData(11, 'B')]
   [InlineData(12, 'C')]
   [InlineData(13, 'D')]
   [InlineData(14, 'E')]
   [InlineData(15, 'F')]
   [InlineData(16, 'G')]
   [InlineData(17, 'H')]
   [InlineData(18, 'I')]
   [InlineData(19, 'J')]
   [InlineData(20, 'K')]
   [InlineData(21, 'L')]
   [InlineData(22, 'M')]
   [InlineData(23, 'N')]
   [InlineData(24, 'O')]
   [InlineData(25, 'P')]
   [InlineData(26, 'Q')]
   [InlineData(27, 'R')]
   [InlineData(28, 'S')]
   [InlineData(29, 'T')]
   [InlineData(30, 'U')]
   [InlineData(31, 'V')]
   [InlineData(32, 'W')]
   [InlineData(33, 'X')]
   [InlineData(34, 'Y')]
   [InlineData(35, 'Z')]
   [InlineData(36, '*')]
   public void AlphanumericSupplementaryDomain_GetCheckCharacter_ShouldReturnExpectedValue_WhenCheckDigitIsInRange(
      Int32 value,
      Char expected)
      => _sut.GetCheckCharacter(value).Should().Be(expected);

   [Theory]
   [InlineData(-1)]
   [InlineData(38)]
   public void AlphanumericSupplementaryDomain_GetCheckCharacter_ShouldThrowArgumentOutOfRangeException_WhenCheckDigitIsOutOfRange(Int32 checkDigit)
   {
      // Arrange.
      var act = () => _sut.GetCheckCharacter(checkDigit);
      var expectedMessage = Resources.GetCheckCharacterValueOutOfRangeMessage;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentOutOfRangeException>()
         .WithParameterName(nameof(checkDigit))
         .WithMessage(expectedMessage + "*");
   }

   #endregion

   #region MapCharacterToNumber Tests
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
   public void AlphanumericSupplementaryDomain_MapCharacterToNumber_ShouldReturnExpectedValue_WhenCharacterIsValid(
      Char ch,
      Int32 expected)
      => _sut.MapCharacterToNumber(ch).Should().Be(expected);

   [Theory]
   [InlineData('\0')]
   [InlineData(')')]
   [InlineData('+')]
   [InlineData('/')]
   [InlineData(':')]
   [InlineData('@')]
   [InlineData('[')]
   public void AlphanumericSupplementaryDomain_TryGetCheckCharacterValue_ShouldReturnMinusOne_WhenCharacterIsNotValid(Char ch)
      => _sut.MapCharacterToNumber(ch).Should().Be(-1);

   #endregion

   #region MapCheckCharacterToNumber Tests
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
   [InlineData('*', 36)]
   public void AlphanumericSupplementaryDomain_MapCheckCharacterToNumber_ShouldReturnExpectedValue_WhenCharacterIsValid(
      Char ch,
      Int32 expected)
      => _sut.MapCheckCharacterToNumber(ch).Should().Be(expected);

   [Theory]
   [InlineData('\0')]
   [InlineData(')')]
   [InlineData('+')]
   [InlineData('/')]
   [InlineData(':')]
   [InlineData('@')]
   [InlineData('[')]
   public void AlphanumericSupplementaryDomain_MapCheckCharacterToNumber_ShouldReturnMinusOne_WhenCharacterIsNotValid(Char ch)
      => _sut.MapCheckCharacterToNumber(ch).Should().Be(-1);

   #endregion
}
