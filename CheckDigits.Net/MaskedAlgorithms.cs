// Ignore Spelling: Damm Luhn Verhoeff

namespace CheckDigits.Net;

/// <summary>
///   Lazy instantiated singleton instances of masked algorithms supported by
///   CheckDigits.Net.
/// </summary>
public static class MaskedAlgorithms
{
   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _damm =
      new(() => new DammAlgorithm());

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
   ///   Damm algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Damm => _damm.Value;

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
