namespace CheckDigits.Net.Tests.Unit;

public class LineSeparatorTests
{
   #region IsDefined Tests
   // ==========================================================================
   // ==========================================================================

   public static TheoryData<LineSeparator> LineSeparatorValues
   {
      get {
         var data = new TheoryData<LineSeparator>();
         foreach(var value in Enum.GetValues(typeof(LineSeparator)))
         {
            data.Add((LineSeparator)value);
         }

         return data;
      }
   }

   [Theory]
   [MemberData(nameof(LineSeparatorValues))]
   public void LineSeparator_IsDefined_ShouldReturnTrue_WhenValueIsValid(LineSeparator value)
      => value.IsDefined().Should().BeTrue();

   [Fact]
   public void LineSeparator_IsDefined_ShouldReturnFalse_WhenValueIsNotValid()
      => ((LineSeparator)(-1)).IsDefined().Should().BeFalse();

   #endregion

   #region CharacterLength Tests
   // ==========================================================================
   // ==========================================================================

   [Theory]
   [InlineData(LineSeparator.None, 0)]
   [InlineData(LineSeparator.Crlf, 2)]
   [InlineData(LineSeparator.Lf, 1)]
   public void LineSeparator_CharacterLength_ShouldReturnExpectedValue_WhenValueIsValid(
      LineSeparator value,
      Int32 expected)
      => value.CharacterLength().Should().Be(expected);

   [Fact]
   public void LineSeparator_CharacterLength_ShouldThrowArgumentException_WhenValidIsNotValid()
   {
      // Arrange.
      var value = (LineSeparator)(-1);
      var act = () => _ = value.CharacterLength();
      var expectedMessage = Resources.LineSeparatorInvalidValueMessage;

      // Act/assert.
      act.Should().ThrowExactly<ArgumentOutOfRangeException>()
         .WithParameterName(nameof(value))
         .WithMessage(expectedMessage + "*")
         .And.ActualValue.Should().Be(value);
   }

   #endregion
}
