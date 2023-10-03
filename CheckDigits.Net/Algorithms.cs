// Ignore Spelling: Luhn

namespace CheckDigits.Net;

/// <summary>
///   Lazy instantiated singleton instances of the algorithms supported by
///   CheckDigits.Net.
/// </summary>
public class Algorithms
{
   private static readonly Lazy<ISingleCheckDigitAlgorithm> _luhn =
     new(() => new LuhnAlgorithm());

   /// <summary>
   ///   Luhn algorithm.
   /// </summary>
   public static ISingleCheckDigitAlgorithm LuhnAlgorithm => _luhn.Value;
}
