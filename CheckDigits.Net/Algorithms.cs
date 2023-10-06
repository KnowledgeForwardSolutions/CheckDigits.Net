// Ignore Spelling: Damm Luhn Verhoeff

namespace CheckDigits.Net;

/// <summary>
///   Lazy instantiated singleton instances of the algorithms supported by
///   CheckDigits.Net.
/// </summary>
public class Algorithms
{
   private static readonly Lazy<ISingleCheckDigitAlgorithm> _damm =
     new(() => new DammAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _luhn =
     new(() => new LuhnAlgorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _modulus10_13 =
     new(() => new Modulus10_13Algorithm());

   private static readonly Lazy<ISingleCheckDigitAlgorithm> _verhoeff =
     new(() => new VerhoeffAlgorithm());

   /// <summary>
   ///   Damm algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm DammAlgorithm => _damm.Value;

   /// <summary>
   ///   Luhn algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm LuhnAlgorithm => _luhn.Value;

   /// <summary>
   ///   Modulus10_13 algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm Modulus10_13Algorithm => _modulus10_13.Value;

   /// <summary>
   ///   Verhoeff algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm VerhoeffAlgorithm => _verhoeff.Value;
}
