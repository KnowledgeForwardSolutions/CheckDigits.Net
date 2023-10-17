namespace CheckDigits.Net.Iso7064;

public class Iso7064Mod37_2Algorithm : Iso7064PureSystemSingleCharacterAlgorithm, ISingleCheckDigitAlgorithm
{
   /// <summary>
   ///   Initialize a new <see cref="Iso7064Mod11_2Algorithm"/>
   /// </summary>
   public Iso7064Mod37_2Algorithm() : base(37, 2, CharacterDomains.AlphanumericSupplementary) { }

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod37_2AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod37_2AlgorithmName;
}
