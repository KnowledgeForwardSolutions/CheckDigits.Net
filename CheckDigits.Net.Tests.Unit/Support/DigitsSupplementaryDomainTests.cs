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

   #region TryGetValue Tests
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
   public void DigitsSupplementaryDomain_TryGetValue_ShouldReturnTrue_WhenCharacterIsValid(
      Char ch, 
      Int32 expected)
   {
      // Act/assert.
      _sut.TryGetValue(ch, out var value).Should().BeTrue();
      value.Should().Be(expected);
   }

   [Theory]
   [InlineData('\0')]
   [InlineData('/')]
   [InlineData(':')]
   public void DigitsSupplementaryDomain_TryGetValue_ShouldReturnFalse_WhenCharacterIsNotValid(
      Char ch)
   {
      // Act/assert.
      _sut.TryGetValue(ch, out var value).Should().BeFalse();
      value.Should().Be(-1);
   }

   #endregion
}
