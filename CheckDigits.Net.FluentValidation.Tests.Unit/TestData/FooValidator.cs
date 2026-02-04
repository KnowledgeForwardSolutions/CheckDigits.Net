// Ignore Spelling: Cusip Damm Figi Iban Isan Isin Luhn Ncd Nhs Npi Rtn Sedol Validator Verhoeff Vin

namespace CheckDigits.Net.FluentValidation.Tests.Unit.TestData;

public class FooAbaRtnValidator : AbstractValidator<FooRequest>
{
   public FooAbaRtnValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.AbaRtn);
   }
}

public class FooAlphanumericMod97_10Validator : AbstractValidator<FooRequest>
{
   public FooAlphanumericMod97_10Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.AlphanumericMod97_10);
   }
}

public class FooCusipValidator : AbstractValidator<FooRequest>
{
   public FooCusipValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Cusip);
   }
}

public class FooDammValidator : AbstractValidator<FooRequest>
{
   public FooDammValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Damm);
   }
}

public class FooFigiValidator : AbstractValidator<FooRequest>
{
   public FooFigiValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Figi);
   }
}

public class FooIbanValidator : AbstractValidator<FooRequest>
{
   public FooIbanValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iban);
   }
}

public class FooIcao9303Validator : AbstractValidator<FooRequest>
{
   public FooIcao9303Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Icao9303);
   }
}

public class FooIcao9303MachineReadableVisaValidator : AbstractValidator<FooRequest>
{
   public FooIcao9303MachineReadableVisaValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Icao9303MachineReadableVisa);
   }
}

public class FooIcao9303SizeTD1Validator : AbstractValidator<FooRequest>
{
   public FooIcao9303SizeTD1Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Icao9303SizeTD1);
   }
}

public class FooIcao9303SizeTD2Validator : AbstractValidator<FooRequest>
{
   public FooIcao9303SizeTD2Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Icao9303SizeTD2);
   }
}

public class FooIcao9303SizeTD3Validator : AbstractValidator<FooRequest>
{
   public FooIcao9303SizeTD3Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Icao9303SizeTD3);
   }
}

public class FooIsanValidator : AbstractValidator<FooRequest>
{
   public FooIsanValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Isan);
   }
}

public class FooIsinValidator : AbstractValidator<FooRequest>
{
   public FooIsinValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Isin);
   }
}

public class FooIso6346Validator : AbstractValidator<FooRequest>
{
   public FooIso6346Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iso6346);
   }
}

public class FooIso7064CustomDanishAlphabetValidator : AbstractValidator<FooRequest>
{
   public FooIso7064CustomDanishAlphabetValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(new Iso7064CustomDanishAlgorithm());
   }
}

public class FooIso7064CustomLettersAlphabetValidator : AbstractValidator<FooRequest>
{
   public FooIso7064CustomLettersAlphabetValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(new Iso7064CustomLettersAlgorithm());
   }
}

public class FooIso7064CustomNumericSupplementalAlphabetValidator : AbstractValidator<FooRequest>
{
   public FooIso7064CustomNumericSupplementalAlphabetValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(new Iso7064CustomNumericSupplementalAlgorithm());
   }
}

public class FooIso7064Mod11_10Validator : AbstractValidator<FooRequest>
{
   public FooIso7064Mod11_10Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iso7064Mod11_10);
   }
}

public class FooIso7064Mod11_2Validator : AbstractValidator<FooRequest>
{
   public FooIso7064Mod11_2Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iso7064Mod11_2);
   }
}

public class FooIso7064Mod1271_36Validator : AbstractValidator<FooRequest>
{
   public FooIso7064Mod1271_36Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iso7064Mod1271_36);
   }
}

public class FooIso7064Mod27_26Validator : AbstractValidator<FooRequest>
{
   public FooIso7064Mod27_26Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iso7064Mod27_26);
   }
}

public class FooIso7064Mod37_2Validator : AbstractValidator<FooRequest>
{
   public FooIso7064Mod37_2Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iso7064Mod37_2);
   }
}

public class FooIso7064Mod37_36Validator : AbstractValidator<FooRequest>
{
   public FooIso7064Mod37_36Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iso7064Mod37_36);
   }
}

public class FooIso7064Mod661_26Validator : AbstractValidator<FooRequest>
{
   public FooIso7064Mod661_26Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iso7064Mod661_26);
   }
}

public class FooIso7064Mod97_10Validator : AbstractValidator<FooRequest>
{
   public FooIso7064Mod97_10Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Iso7064Mod97_10);
   }
}

public class FooLuhnValidator : AbstractValidator<FooRequest>
{
   public FooLuhnValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Luhn);
   }
}

public class FooModulus10_13Validator : AbstractValidator<FooRequest>
{
   public FooModulus10_13Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Modulus10_13);
   }
}

public class FooModulus10_1Validator : AbstractValidator<FooRequest>
{
   public FooModulus10_1Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Modulus10_1);
   }
}

public class FooModulus10_2Validator : AbstractValidator<FooRequest>
{
   public FooModulus10_2Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Modulus10_2);
   }
}

public class FooModulus11Validator : AbstractValidator<FooRequest>
{
   public FooModulus11Validator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Modulus11);
   }
}

public class FooNcdValidator : AbstractValidator<FooRequest>
{
   public FooNcdValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Ncd);
   }
}

public class FooNhsValidator : AbstractValidator<FooRequest>
{
   public FooNhsValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Nhs);
   }
}

public class FooNpiValidator : AbstractValidator<FooRequest>
{
   public FooNpiValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Npi);
   }
}

public class FooSedolValidator : AbstractValidator<FooRequest>
{
   public FooSedolValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Sedol);
   }
}

public class FooVerhoeffValidator : AbstractValidator<FooRequest>
{
   public FooVerhoeffValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Verhoeff);
   }
}

public class FooVinValidator : AbstractValidator<FooRequest>
{
   public FooVinValidator()
   {
      RuleFor(x => x.Bar).CheckDigit(Algorithms.Vin);
   }
}

