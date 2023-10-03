// Ignore Spelling: Luhn

namespace CheckDigits.Net.Tests.Unit;

public class AlgorithmsTests
{
   #region Luhn Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_LuhnAlgorithm_ShouldNotBeNull()
      => Algorithms.LuhnAlgorithm.Should().NotBeNull();

   [Fact]
   public void Algorithms_LuhnAlgorithm_ShouldBeExpectedType()
      => Algorithms.LuhnAlgorithm.Should().BeOfType<LuhnAlgorithm>();

   #endregion
}
