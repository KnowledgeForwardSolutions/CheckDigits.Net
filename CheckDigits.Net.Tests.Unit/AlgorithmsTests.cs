// Ignore Spelling: Aba Damm Isin Luhn Nhs Npi Rtn Verhoeff

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

   #region Isin Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_IsinAlgorithm_ShouldNotBeNull()
      => Algorithms.Isin.Should().NotBeNull();

   [Fact]
   public void Algorithms_IsinAlgorithm_ShouldBeExpectedType()
      => Algorithms.Isin.Should().BeOfType<IsinAlgorithm>();

   #endregion

   #region Iso7064Mod11_2 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Iso7064Mod11_2_ShouldNotBeNull()
      => Algorithms.Iso7064Mod11_2.Should().NotBeNull();

   [Fact]
   public void Algorithms_Iso7064Mod11_2_ShouldBeExpectedType()
      => Algorithms.Iso7064Mod11_2.Should().BeOfType<Iso7064Mod11_2Algorithm>();

   #endregion

   #region Iso7064Mod1271_36 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Iso7064Mod1271_36_ShouldNotBeNull()
      => Algorithms.Iso7064Mod1271_36.Should().NotBeNull();

   [Fact]
   public void Algorithms_Iso7064Mod1271_36_ShouldBeExpectedType()
      => Algorithms.Iso7064Mod1271_36.Should().BeOfType<Iso7064Mod1271_36Algorithm>();

   #endregion

   #region Iso7064Mod37_2 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Iso7064Mod37_2_ShouldNotBeNull()
      => Algorithms.Iso7064Mod37_2.Should().NotBeNull();

   [Fact]
   public void Algorithms_Iso7064Mod37_2_ShouldBeExpectedType()
      => Algorithms.Iso7064Mod37_2.Should().BeOfType<Iso7064Mod37_2Algorithm>();

   #endregion

   #region Iso7064Mod661_26 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Iso7064Mod661_26_ShouldNotBeNull()
      => Algorithms.Iso7064Mod661_26.Should().NotBeNull();

   [Fact]
   public void Algorithms_Iso7064Mod661_26_ShouldBeExpectedType()
      => Algorithms.Iso7064Mod661_26.Should().BeOfType<Iso7064Mod661_26Algorithm>();

   #endregion

   #region Iso7064Mod97_10 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Iso7064Mod97_10_ShouldNotBeNull()
      => Algorithms.Iso7064Mod97_10.Should().NotBeNull();

   [Fact]
   public void Algorithms_Iso7064Mod97_10_ShouldBeExpectedType()
      => Algorithms.Iso7064Mod97_10.Should().BeOfType<Iso7064Mod97_10Algorithm>();

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

   #region Modulus10_1 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Modulus10_1Algorithm_ShouldNotBeNull()
      => Algorithms.Modulus10_1.Should().NotBeNull();

   [Fact]
   public void Algorithms_Modulus10_1Algorithm_ShouldBeExpectedType()
      => Algorithms.Modulus10_1.Should().BeOfType<Modulus10_1Algorithm>();

   #endregion

   #region Modulus10_2 Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Modulus10_2Algorithm_ShouldNotBeNull()
      => Algorithms.Modulus10_2.Should().NotBeNull();

   [Fact]
   public void Algorithms_Modulus10_2Algorithm_ShouldBeExpectedType()
      => Algorithms.Modulus10_2.Should().BeOfType<Modulus10_2Algorithm>();

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

   #region Nhs Algorithm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_NhsAlgorithm_ShouldNotBeNull()
      => Algorithms.Nhs.Should().NotBeNull();

   [Fact]
   public void Algorithms_NhsAlgorithm_ShouldBeExpectedType()
      => Algorithms.Nhs.Should().BeOfType<NhsAlgorithm>();

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
