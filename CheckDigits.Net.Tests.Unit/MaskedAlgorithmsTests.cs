// Ignore Spelling: Luhn

namespace CheckDigits.Net.Tests.Unit;

public class MaskedAlgorithmsTests
{
   #region Luhn Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Luhn_ShouldNotBeNull()
      => MaskedAlgorithms.Luhn.Should().NotBeNull();

   [Fact]
   public void Algorithms_Luhn_ShouldBeExpectedType()
      => MaskedAlgorithms.Luhn.Should().BeOfType<LuhnAlgorithm>();

   #endregion

   #region Modulus10_13 Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void Algorithms_Modulus10_13_ShouldNotBeNull()
      => MaskedAlgorithms.Modulus10_13.Should().NotBeNull();

   [Fact]
   public void Algorithms_Modulus10_13_ShouldBeExpectedType()
      => MaskedAlgorithms.Modulus10_13.Should().BeOfType<Modulus10_13Algorithm>();

   #endregion
}
