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
}
