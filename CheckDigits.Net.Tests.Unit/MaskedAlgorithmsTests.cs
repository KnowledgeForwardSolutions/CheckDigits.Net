// Ignore Spelling: Luhn

namespace CheckDigits.Net.Tests.Unit;

public class MaskedAlgorithmsTests
{
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
}
