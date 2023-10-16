namespace CheckDigits.Net.Tests.Unit.Support;

public class DigitsSupplementaryDomainTests
{
   private readonly DigitsSupplementaryDomain _sut = new();

   #region CheckCharacters Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DigitsSupplementaryDomain_CheckCharacters_ShouldReturnExpectedValue()
      => _sut.CheckCharacters.Should().Be("0123456789X");

   #endregion

   #region ValidCharacters Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void DigitsSupplementaryDomain_ValidCharacters_ShouldReturnExpectedValue()
      => _sut.ValidCharacters.Should().Be("0123456789");

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
   [InlineData(10, 'X')]
   public void DigitsSupplementaryDomain_GetCheckCharacter_ShouldReturnExpectedValue_WhenCheckDigitIsInRange(
      Int32 value,
      Char expected)
      => _sut.GetCheckCharacter(value).Should().Be(expected);

   [Theory]
   [InlineData(-1)]
   [InlineData(11)]
   public void DigitsSupplementaryDomain_GetCheckCharacter_ShouldThrowArgumentOutOfRangeException_WhenCheckDigitIsOutOfRange(Int32 checkDigit)
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
   public void DigitsSupplementaryDomain_MapCharacterToNumber_ShouldReturnExpectedValue_WhenCharacterIsValid(
      Char ch,
      Int32 expected)
      => _sut.MapCharacterToNumber(ch).Should().Be(expected);

   [Theory]
   [InlineData('\0')]
   [InlineData('/')]
   [InlineData(':')]
   [InlineData('X')]
   [InlineData('W')]
   [InlineData('Y')]
   public void DigitsSupplementaryDomain_TryGetCheckCharacterValue_ShouldReturnMinusOne_WhenCharacterIsNotValid(Char ch)
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
   [InlineData('X', 10)]
   public void DigitsSupplementaryDomain_MapCheckCharacterToNumber_ShouldReturnExpectedValue_WhenCharacterIsValid(
      Char ch,
      Int32 expected)
      => _sut.MapCheckCharacterToNumber(ch).Should().Be(expected);

   [Theory]
   [InlineData('\0')]
   [InlineData('/')]
   [InlineData(':')]
   [InlineData('W')]
   [InlineData('Y')]
   public void DigitsSupplementaryDomain_MapCheckCharacterToNumber_ShouldReturnMinusOne_WhenCharacterIsNotValid(Char ch)
      => _sut.MapCheckCharacterToNumber(ch).Should().Be(-1);

   #endregion
}
