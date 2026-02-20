// Ignore Spelling: Luhn

namespace CheckDigits.Net;

/// <summary>
///   Lazy instantiated singleton instances of masked algorithms supported by
///   CheckDigits.Net.
/// </summary>
public static class MaskedAlgorithms
{
   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _luhn =
      new(() => new LuhnAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus10_13 =
      new(() => new Modulus10_13Algorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus11_27Decimal =
      new(() => new Modulus11_27DecimalAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus11_27Extended =
      new(() => new Modulus11_27ExtendedAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus11Decimal =
      new(() => new Modulus11DecimalAlgorithm());

   private static readonly Lazy<IMaskedCheckDigitAlgorithm> _modulus11Extended =
      new(() => new Modulus11ExtendedAlgorithm());

   /// <summary>
   ///   Luhn algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Luhn => _luhn.Value;

   /// <summary>
   ///   Modulus10_13 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Modulus10_13 => _modulus10_13.Value;

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

}
