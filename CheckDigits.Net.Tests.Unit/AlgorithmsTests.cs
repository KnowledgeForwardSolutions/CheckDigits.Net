// Ignore Spelling: Aba Damm Luhn Npi Rtn Verhoeff

namespace CheckDigits.Net.Tests.Unit;

public class AlgorithmsTests
{
   #region AbaRtn Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_AbaRtnAlgorithm_ShouldNotBeNull()
      => Algorithms.AbaRtn.Should().NotBeNull();

   [Fact]
   public void Algorithms_AbaRtnAlgorithm_ShouldBeExpectedType()
      => Algorithms.AbaRtn.Should().BeOfType<AbaRtnAlgorithm>();

   #endregion

   #region Damm Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_DammAlgorithm_ShouldNotBeNull()
      => Algorithms.Damm.Should().NotBeNull();

   [Fact]
   public void Algorithms_DammAlgorithm_ShouldBeExpectedType()
      => Algorithms.Damm.Should().BeOfType<DammAlgorithm>();

   #endregion

   #region Luhn Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_LuhnAlgorithm_ShouldNotBeNull()
      => Algorithms.Luhn.Should().NotBeNull();

   [Fact]
   public void Algorithms_LuhnAlgorithm_ShouldBeExpectedType()
      => Algorithms.Luhn.Should().BeOfType<LuhnAlgorithm>();

   #endregion

   #region Modulus10_13 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Modulus10_13Algorithm_ShouldNotBeNull()
      => Algorithms.Modulus10_13.Should().NotBeNull();

   [Fact]
   public void Algorithms_Modulus10_13Algorithm_ShouldBeExpectedType()
      => Algorithms.Modulus10_13.Should().BeOfType<Modulus10_13Algorithm>();

   #endregion

   #region Modulus11 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Modulus11Algorithm_ShouldNotBeNull()
      => Algorithms.Modulus11.Should().NotBeNull();

   [Fact]
   public void Algorithms_Modulus11Algorithm_ShouldBeExpectedType()
      => Algorithms.Modulus11.Should().BeOfType<Modulus11Algorithm>();

   #endregion

   #region Npi Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_NpiAlgorithm_ShouldNotBeNull()
      => Algorithms.Npi.Should().NotBeNull();

   [Fact]
   public void Algorithms_NpiAlgorithm_ShouldBeExpectedType()
      => Algorithms.Npi.Should().BeOfType<NpiAlgorithm>();

   #endregion

   #region Verhoeff Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_VerhoeffAlgorithm_ShouldNotBeNull()
      => Algorithms.Verhoeff.Should().NotBeNull();

   [Fact]
   public void Algorithms_VerhoeffAlgorithm_ShouldBeExpectedType()
      => Algorithms.Verhoeff.Should().BeOfType<VerhoeffAlgorithm>();

   #endregion

   #region Vin Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_VinAlgorithm_ShouldNotBeNull()
      => Algorithms.Vin.Should().NotBeNull();

   [Fact]
   public void Algorithms_VinAlgorithm_ShouldBeExpectedType()
      => Algorithms.Vin.Should().BeOfType<VinAlgorithm>();

   #endregion
}
