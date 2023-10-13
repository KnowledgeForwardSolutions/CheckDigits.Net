// Ignore Spelling: Aba Damm Isin Luhn Nhs Npi Rtn Verhoeff

namespace CheckDigits.Net;

/// <summary>
///   Lazy instantiated singleton instances of the algorithms supported by
///   CheckDigits.Net.
/// </summary>
public class Algorithms
{
   private static readonly Lazy<ICheckDigitAlgorithm> _abaRtn =
     new(() => new AbaRtnAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _damm =
     new(() => new DammAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _isin =
     new(() => new IsinAlgorithm());

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

   private static readonly Lazy<ICheckDigitAlgorithm> _nhs =
     new(() => new NhsAlgorithm());

   private static readonly Lazy<ICheckDigitAlgorithm> _npi =
     new(() => new NpiAlgorithm());

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
   ///   Damm algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Damm => _damm.Value;

   /// <summary>
   ///   International Securities Identification Number algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Isin => _isin.Value;

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
   ///   UK National Health Service (NHS) algorithm.
   /// </summary>
   public static ICheckDigitAlgorithm Nhs => _nhs.Value;

   /// <summary>
   ///   US National Provider Identifier (NPI) algorithm.
   /// </summary>
   public static ICheckDigitAlgorithm Npi => _npi.Value;

   /// <summary>
   ///   Verhoeff algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Verhoeff => _verhoeff.Value;

   /// <summary>
   ///   Vehicle Identification Number (VIN) algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Vin => _vin.Value;
}
