// Ignore Spelling: Aba Cusip Damm Iban Isan Isin Luhn Ncd Nhs Npi Rtn Sedol Verhoeff

namespace CheckDigits.Net;

/// <summary>
///   Lazy instantiated singleton instances of the algorithms supported by
///   CheckDigits.Net.
/// </summary>
public static class Algorithms
{
   private static readonly Lazy<ICheckDigitAlgorithm> _abaRtn =
     new(() => new AbaRtnAlgorithm());

   private static readonly Lazy<IDoubleCheckDigitAlgorithm> _alphanumericMod97_10 =
     new(() => new AlphanumericMod97_10Algorithm());

   private static readonly Lazy<ICheckDigitAlgorithm> _cusip =
     new(() => new CusipAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _damm =
     new(() => new DammAlgorithm());

   private static readonly Lazy<IDoubleCheckDigitAlgorithm> _iban =
     new(() => new IbanAlgorithm());

   private static readonly Lazy<ICheckDigitAlgorithm> _isan =
     new(() => new IsanAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _isin =
     new(() => new IsinAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _iso6346 =
     new(() => new Iso6346Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _iso7064Mod11_10 =
     new(() => new Iso7064Mod11_10Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _iso7064Mod11_2 =
     new(() => new Iso7064Mod11_2Algorithm());

   private static readonly Lazy<IDoubleCheckDigitAlgorithm> _iso7064Mod1271_36 =
     new(() => new Iso7064Mod1271_36Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _iso7064Mod27_26 =
     new(() => new Iso7064Mod27_26Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _iso7064Mod37_2 =
     new(() => new Iso7064Mod37_2Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _iso7064Mod37_36 =
     new(() => new Iso7064Mod37_36Algorithm());

   private static readonly Lazy<IDoubleCheckDigitAlgorithm> _iso7064Mod661_26 =
     new(() => new Iso7064Mod661_26Algorithm());

   private static readonly Lazy<IDoubleCheckDigitAlgorithm> _iso7064Mod97_10 =
     new(() => new Iso7064Mod97_10Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _luhn =
     new(() => new LuhnAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _modulus10_1 =
     new(() => new Modulus10_1Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _modulus10_2 =
     new(() => new Modulus10_2Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _modulus10_13 =
     new(() => new Modulus10_13Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _modulus11 =
     new(() => new Modulus11Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _ncd =
     new(() => new NcdAlgorithm());

   private static readonly Lazy<ICheckDigitAlgorithm> _nhs =
     new(() => new NhsAlgorithm());

   private static readonly Lazy<ICheckDigitAlgorithm> _npi =
     new(() => new NpiAlgorithm());

   private static readonly Lazy<ICheckDigitAlgorithm> _sedol =
     new(() => new SedolAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _verhoeff =
     new(() => new VerhoeffAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _vin =
     new(() => new VinAlgorithm());

   /// <summary>
   ///   American Bankers Association (ABA) Routing Transit Number (RTN) 
   ///   algorithm.
   /// </summary>
   public static ICheckDigitAlgorithm AbaRtn => _abaRtn.Value;

   /// <summary>
   ///   American Bankers Association (ABA) Routing Transit Number (RTN) 
   ///   algorithm.
   /// </summary>
   public static IDoubleCheckDigitAlgorithm AlphanumericMod97_10 => _alphanumericMod97_10.Value;

   /// <summary>
   ///   CUSIP algorithm for North American Securities.
   /// </summary>
   public static ICheckDigitAlgorithm Cusip => _cusip.Value;

   /// <summary>
   ///   Damm algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Damm => _damm.Value;

   /// <summary>
   ///   International Bank Account Number algorithm.
   /// </summary>
   public static IDoubleCheckDigitAlgorithm Iban => _iban.Value;

   /// <summary>
   ///   International Standard Audiovisual Number algorithm.
   /// </summary>
   public static ICheckDigitAlgorithm Isan => _isan.Value;

   /// <summary>
   ///   International Securities Identification Number algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Isin => _isin.Value;

   /// <summary>
   ///   ISO 6346 Algorithm for shipping container numbers.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Iso6346 => _iso6346.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 11,10 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Iso7064Mod11_10 => _iso7064Mod11_10.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 11-2 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Iso7064Mod11_2 => _iso7064Mod11_2.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 1271-36 algorithm.
   /// </summary>
   public static IDoubleCheckDigitAlgorithm Iso7064Mod1271_36 => _iso7064Mod1271_36.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 27,26 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Iso7064Mod27_26 => _iso7064Mod27_26.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 37-2 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Iso7064Mod37_2 => _iso7064Mod37_2.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 37,36 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Iso7064Mod37_36 => _iso7064Mod37_36.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 1271-36 algorithm.
   /// </summary>
   public static IDoubleCheckDigitAlgorithm Iso7064Mod661_26 => _iso7064Mod661_26.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 1271-36 algorithm.
   /// </summary>
   public static IDoubleCheckDigitAlgorithm Iso7064Mod97_10 => _iso7064Mod97_10.Value;

   /// <summary>
   ///   Luhn algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Luhn => _luhn.Value;

   /// <summary>
   ///   Modulus10_1 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Modulus10_1 => _modulus10_1.Value;

   /// <summary>
   ///   Modulus10_2 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Modulus10_2 => _modulus10_2.Value;

   /// <summary>
   ///   Modulus10_13 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Modulus10_13 => _modulus10_13.Value;

   /// <summary>
   ///   Modulus11 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Modulus11 => _modulus11.Value;

   /// <summary>
   ///   NOID (Nice Opaque Identifier) Check Digit algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Ncd => _ncd.Value;

   /// <summary>
   ///   UK National Health Service (NHS) algorithm.
   /// </summary>
   public static ICheckDigitAlgorithm Nhs => _nhs.Value;

   /// <summary>
   ///   US National Provider Identifier (NPI) algorithm.
   /// </summary>
   public static ICheckDigitAlgorithm Npi => _npi.Value;

   /// <summary>
   ///   SEDOL algorithm.
   /// </summary>
   public static ICheckDigitAlgorithm Sedol => _sedol.Value;

   /// <summary>
   ///   Verhoeff algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Verhoeff => _verhoeff.Value;

   /// <summary>
   ///   Vehicle Identification Number (VIN) algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Vin => _vin.Value;
}
