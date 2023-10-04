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

   #region Modulus10_13 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Modulus10_13Algorithm_ShouldNotBeNull()
      => Algorithms.Modulus10_13Algorithm.Should().NotBeNull();

   [Fact]
   public void Algorithms_Modulus10_13Algorithm_ShouldBeExpectedType()
      => Algorithms.Modulus10_13Algorithm.Should().BeOfType<Modulus10_13Algorithm>();

   #endregion
}
