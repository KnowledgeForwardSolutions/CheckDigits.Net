namespace CheckDigits.Net.DataAnnotations.Tests.Unit.TestData;

public class Iso7064CustomDanishAlgorithm : 
   Iso7064PureSystemDoubleCharacterAlgorithm
{
   public Iso7064CustomDanishAlgorithm()
      : base("Danish", "Danish, modulus = 29, radix = 2", 29, 2, new DanishAlphabet())
   { }
}

public class Iso7064CustomLettersAlgorithm :
   Iso7064HybridSystemAlgorithm
{
   public Iso7064CustomLettersAlgorithm()
      : base("Alphabetic", "Alphabetic, modulus = 26", 26, new LettersAlphabet())
   { }
}

public class Iso7064CustomNumericSupplementalAlgorithm :
   Iso7064PureSystemSingleCharacterAlgorithm
{
   public Iso7064CustomNumericSupplementalAlgorithm()
      : base("Numeric", "Numeric, modulus = 11, radix = 2", 11, 2, new DigitsSupplementalAlphabet())
   { }
}
