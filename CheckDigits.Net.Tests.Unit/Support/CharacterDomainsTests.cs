namespace CheckDigits.Net.Tests.Unit.Support;

public class CharacterDomainsTests
{
   #region AlphanumericSupplementary Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CharacterDomains_AlphanumericSupplementary_ShouldNotBeNull()
      => CharacterDomains.AlphanumericSupplementary.Should().NotBeNull();

   [Fact]
   public void Algorithms_AlphanumericSupplementary_ShouldBeExpectedType()
      => CharacterDomains.AlphanumericSupplementary.Should().BeOfType<AlphanumericSupplementaryDomain>();

   #endregion

   #region DigitsSupplementary Property Tests
   // ==========================================================================
   // ==========================================================================

   [Fact]
   public void CharacterDomains_DigitsSupplementary_ShouldNotBeNull()
      => CharacterDomains.DigitsSupplementary.Should().NotBeNull();

   [Fact]
   public void Algorithms_DigitsSupplementary_ShouldBeExpectedType()
      => CharacterDomains.DigitsSupplementary.Should().BeOfType<DigitsSupplementaryDomain>();

   #endregion
}
