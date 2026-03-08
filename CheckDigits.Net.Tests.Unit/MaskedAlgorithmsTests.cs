// Ignore Spelling: Damm Luhn Verhoeff

namespace CheckDigits.Net.Tests.Unit;

public class MaskedAlgorithmsTests
{
   #region Damm Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Damm_ShouldNotBeNull()
      => MaskedAlgorithms.Damm.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Damm_ShouldBeExpectedType()
      => MaskedAlgorithms.Damm.Should().BeOfType<DammAlgorithm>();

   #endregion

   #region Luhn Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Luhn_ShouldNotBeNull()
      => MaskedAlgorithms.Luhn.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Luhn_ShouldBeExpectedType()
      => MaskedAlgorithms.Luhn.Should().BeOfType<LuhnAlgorithm>();

   #endregion

   #region Modulus10_1 Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Modulus10_1_ShouldNotBeNull()
      => MaskedAlgorithms.Modulus10_1.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Modulus10_1_ShouldBeExpectedType()
      => MaskedAlgorithms.Modulus10_1.Should().BeOfType<Modulus10_1Algorithm>();

   #endregion

   #region Modulus10_13 Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Modulus10_13_ShouldNotBeNull()
      => MaskedAlgorithms.Modulus10_13.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Modulus10_13_ShouldBeExpectedType()
      => MaskedAlgorithms.Modulus10_13.Should().BeOfType<Modulus10_13Algorithm>();

   #endregion

   #region Modulus10_2 Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Modulus10_2_ShouldNotBeNull()
      => MaskedAlgorithms.Modulus10_2.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Modulus10_2_ShouldBeExpectedType()
      => MaskedAlgorithms.Modulus10_2.Should().BeOfType<Modulus10_2Algorithm>();

   #endregion

   #region Modulus11_27Decimal Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Modulus11_27Decimal_ShouldNotBeNull()
      => MaskedAlgorithms.Modulus11_27Decimal.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Modulus11_27Decimal_ShouldBeExpectedType()
      => MaskedAlgorithms.Modulus11_27Decimal.Should().BeOfType<Modulus11_27DecimalAlgorithm>();

   #endregion

   #region Modulus11_27Extended Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Modulus11_27Extended_ShouldNotBeNull()
      => MaskedAlgorithms.Modulus11_27Extended.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Modulus11_27Extended_ShouldBeExpectedType()
      => MaskedAlgorithms.Modulus11_27Extended.Should().BeOfType<Modulus11_27ExtendedAlgorithm>();

   #endregion

   #region Modulus11Decimal Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Modulus11Decimal_ShouldNotBeNull()
      => MaskedAlgorithms.Modulus11Decimal.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Modulus11Decimal_ShouldBeExpectedType()
      => MaskedAlgorithms.Modulus11Decimal.Should().BeOfType<Modulus11DecimalAlgorithm>();

   #endregion

   #region Modulus11Extended Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Modulus11Extended_ShouldNotBeNull()
      => MaskedAlgorithms.Modulus11Extended.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Modulus11Extended_ShouldBeExpectedType()
      => MaskedAlgorithms.Modulus11Extended.Should().BeOfType<Modulus11ExtendedAlgorithm>();

   #endregion

   #region Verhoeff Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void MaskedAlgorithms_Verhoeff_ShouldNotBeNull()
      => MaskedAlgorithms.Verhoeff.Should().NotBeNull();

   [Fact]
   public void MaskedAlgorithms_Verhoeff_ShouldBeExpectedType()
      => MaskedAlgorithms.Verhoeff.Should().BeOfType<VerhoeffAlgorithm>();

   #endregion
}
