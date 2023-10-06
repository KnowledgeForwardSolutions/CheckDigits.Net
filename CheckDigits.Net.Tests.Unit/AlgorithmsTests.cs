// Ignore Spelling: Aba Damm Luhn Rtn Verhoeff

namespace CheckDigits.Net.Tests.Unit;

public class AlgorithmsTests
{
   #region AbaRtn Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_AbaRtnAlgorithm_ShouldNotBeNull()
      => Algorithms.AbaRtnAlgorithm.Should().NotBeNull();

   [Fact]
   public void Algorithms_AbaRtnAlgorithm_ShouldBeExpectedType()
      => Algorithms.AbaRtnAlgorithm.Should().BeOfType<AbaRtnAlgorithm>();

   #endregion

   #region Damm Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_DammAlgorithm_ShouldNotBeNull()
      => Algorithms.DammAlgorithm.Should().NotBeNull();

   [Fact]
   public void Algorithms_DammAlgorithm_ShouldBeExpectedType()
      => Algorithms.DammAlgorithm.Should().BeOfType<DammAlgorithm>();

   #endregion

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

   #region Verhoeff Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_VerhoeffAlgorithm_ShouldNotBeNull()
      => Algorithms.VerhoeffAlgorithm.Should().NotBeNull();

   [Fact]
   public void Algorithms_VerhoeffAlgorithm_ShouldBeExpectedType()
      => Algorithms.VerhoeffAlgorithm.Should().BeOfType<VerhoeffAlgorithm>();

   #endregion
}
