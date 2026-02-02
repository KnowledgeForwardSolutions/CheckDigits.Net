// Ignore Spelling: Cusip, Damm, Figi, Iban, Isan, Isin, Luhn, Ncd, Nhs, Npi, Rtn, Sedol, Verhoeff, Vin

using CheckDigits.Net.Iso7064;
using CheckDigits.Net.ValueSpecificAlgorithms;

namespace CheckDigits.Net.DataAnnotations.Tests.Unit.TestData;

public interface IFooRequest
{
   String BarValue { get; set; }
}

public class FooAbaRtn : IFooRequest
{
   [CheckDigit<AbaRtnAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooAlphanumericMod97_10 : IFooRequest
{
   [CheckDigit<AlphanumericMod97_10Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooCusip : IFooRequest
{
   [CheckDigit<CusipAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooDamm : IFooRequest
{
   [CheckDigit<DammAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooFigi : IFooRequest
{
   [CheckDigit<FigiAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIban : IFooRequest
{
   [CheckDigit<IbanAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIcao9303 : IFooRequest
{
   [CheckDigit<Icao9303Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIcao9303MachineReadableVisa : IFooRequest
{
   [CheckDigit<Icao9303MachineReadableVisaAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIcao9303SizeTD1 : IFooRequest
{
   [CheckDigit<Icao9303SizeTD1Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIcao9303SizeTD2 : IFooRequest
{
   [CheckDigit<Icao9303SizeTD2Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIcao9303SizeTD3 : IFooRequest
{
   [CheckDigit<Icao9303SizeTD3Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIsan : IFooRequest
{
   [CheckDigit<IsanAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIso6346 : IFooRequest
{
   [CheckDigit<Iso6346Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIsin : IFooRequest
{
   [CheckDigit<IsinAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIso7064Mod11_10 : IFooRequest
{
   [CheckDigit<Iso7064Mod11_10Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIso7064Mod11_2 : IFooRequest
{
   [CheckDigit<Iso7064Mod11_2Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIso7064Mod1271_36 : IFooRequest
{
   [CheckDigit<Iso7064Mod1271_36Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIso7064Mod27_26 : IFooRequest
{
   [CheckDigit<Iso7064Mod27_26Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIso7064Mod37_2 : IFooRequest
{
   [CheckDigit<Iso7064Mod37_2Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIso7064Mod37_36 : IFooRequest
{
   [CheckDigit<Iso7064Mod37_36Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIso7064Mod661_26 : IFooRequest
{
   [CheckDigit<Iso7064Mod661_26Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooIso7064Mod97_10 : IFooRequest
{
   [CheckDigit<Iso7064Mod97_10Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooLuhn : IFooRequest
{
   [CheckDigit<LuhnAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooMaskedLuhn : IFooRequest
{
   [MaskedCheckDigit<LuhnAlgorithm, CreditCardMask>]
   public String BarValue { get; set; } = null!;
}

public class FooModulus10_13 : IFooRequest
{
   [CheckDigit<Modulus10_13Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooModulus10_1 : IFooRequest
{
   [CheckDigit<Modulus10_1Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooModulus10_2 : IFooRequest
{
   [CheckDigit<Modulus10_2Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooModulus11 : IFooRequest
{
   [CheckDigit<Modulus11Algorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooNcd : IFooRequest
{
   [CheckDigit<NcdAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooNhs : IFooRequest
{
   [CheckDigit<NhsAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooNpi : IFooRequest
{
   [CheckDigit<NpiAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooSedol : IFooRequest
{
   [CheckDigit<SedolAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooVerhoeff : IFooRequest
{
   [CheckDigit<VerhoeffAlgorithm>]
   public String BarValue { get; set; } = null!;
}

public class FooVin : IFooRequest
{
   [CheckDigit<VinAlgorithm>]
   public String BarValue { get; set; } = null!;
}
