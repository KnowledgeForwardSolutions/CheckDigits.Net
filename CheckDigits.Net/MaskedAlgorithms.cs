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

   /// <summary>
   ///   Luhn algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Luhn => _luhn.Value;

   /// <summary>
   ///   Modulus10_13 algorithm.
   /// </summary>
   public static IMaskedCheckDigitAlgorithm Modulus10_13 => _modulus10_13.Value;

}
