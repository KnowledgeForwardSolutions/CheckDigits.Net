namespace CheckDigits.Net.Support;

/// <summary>
///   Lazy instantiated singleton instances of standard character domains.
/// </summary>
public static class CharacterDomains
{
   private static readonly Lazy<ICharacterDomain> _alphanumericSupplementary =
     new(() => new AlphanumericSupplementaryDomain());

   private static readonly Lazy<ICharacterDomain> _digitsSupplementary =
     new(() => new DigitsSupplementaryDomain());

   /// <summary>
   ///   Character domain of valid alphanumeric characters with supplementary 
   ///   check character '*'.
   /// </summary>
   public static ICharacterDomain AlphanumericSupplementary => _alphanumericSupplementary.Value;

   /// <summary>
   ///   Character domain of valid digits with supplementary check character 'X'.
   /// </summary>
   public static ICharacterDomain DigitsSupplementary => _digitsSupplementary.Value;
}
