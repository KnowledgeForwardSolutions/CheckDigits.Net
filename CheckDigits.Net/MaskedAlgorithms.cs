// Ignore Spelling: Damm Luhn Verhoeff

namespace CheckDigits.Net;

/// <summary>
///   Lazy instantiated singleton instances of masked algorithms supported by
///   CheckDigits.Net.
/// </summary>
public static class MaskedAlgorithms
{
   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _alphanumericMod97_10 =
      new(() => new AlphanumericMod97_10Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _damm =
      new(() => new DammAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _iso7064Mod11_10 =
      new(() => new Iso7064Mod11_10Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _iso7064Mod11_2 =
      new(() => new Iso7064Mod11_2Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _iso7064Mod1271_36 =
      new(() => new Iso7064Mod1271_36Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _iso7064Mod27_26 =
      new(() => new Iso7064Mod27_26Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _iso7064Mod37_2 =
      new(() => new Iso7064Mod37_2Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _luhn =
      new(() => new LuhnAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus10_1 =
      new(() => new Modulus10_1Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus10_13 =
      new(() => new Modulus10_13Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus10_2 =
      new(() => new Modulus10_2Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus11_27Decimal =
      new(() => new Modulus11_27DecimalAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus11_27Extended =
      new(() => new Modulus11_27ExtendedAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus11Decimal =
      new(() => new Modulus11DecimalAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus11Extended =
      new(() => new Modulus11ExtendedAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _verhoeff =
      new(() => new VerhoeffAlgorithm());

   /// <summary>
   ///   AlphanumericMod97_10 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm AlphanumericMod97_10 => _alphanumericMod97_10.Value;

   /// <summary>
   ///   Damm algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Damm => _damm.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 11,10 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Iso7064Mod11_10 => _iso7064Mod11_10.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 11-2 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Iso7064Mod11_2 => _iso7064Mod11_2.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 1271-36 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Iso7064Mod1271_36 => _iso7064Mod1271_36.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 27,26 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Iso7064Mod27_26 => _iso7064Mod27_26.Value;

   /// <summary>
   ///   ISO/IEC 7064 MOD 37-2 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Iso7064Mod37_2 => _iso7064Mod37_2.Value;

   /// <summary>
   ///   Luhn algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Luhn => _luhn.Value;

   /// <summary>
   ///   Modulus10_1 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Modulus10_1 => _modulus10_1.Value;

   /// <summary>
   ///   Modulus10_13 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Modulus10_13 => _modulus10_13.Value;

   /// <summary>
   ///   Modulus10_2 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Modulus10_2 => _modulus10_2.Value;

   /// <summary>
   ///   Modulus11_27Decimal algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Modulus11_27Decimal => _modulus11_27Decimal.Value;

   /// <summary>
   ///   Modulus11_27Extended algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Modulus11_27Extended => _modulus11_27Extended.Value;

   /// <summary>
   ///   Modulus11Decimal algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Modulus11Decimal => _modulus11Decimal.Value;

   /// <summary>
   ///   Modulus11Extended algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Modulus11Extended => _modulus11Extended.Value;

   /// <summary>
   ///   Verhoeff algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Verhoeff => _verhoeff.Value;
}
