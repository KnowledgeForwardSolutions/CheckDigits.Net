// Ignore Spelling: Luhn

namespace CheckDigits.Net;

/// <summary>
///   Modulus 10 algorithm created by Hans Peter Luhn. Uses a weight of 2 which
///   is applied to every odd position digit.
/// </summary>
/// <remarks>
///   Valid characters are decimal digits (0-9).
///   </para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   Will detect all single-digit transcription errors and most two digit 
///   transpositions of adjacent digits (except 09 <-> 90). Will detect most
///   twin errors (i.e. 11 <-> 44) except 22 <-> 55,  33 <-> 66 and 44 <-> 77.
/// </remarks>
public sealed class LuhnAlgorithm : ISingleCheckDigitAlgorithm
{
   private static readonly Int32[] _doubledValues = new Int32[] { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };
   
   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.LuhnAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.LuhnAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
      => CalculateCheckDigit(value.AsSpan(), out checkDigit);

   /// <inheritdoc/>
   public Boolean Validate(String value)
      => !String.IsNullOrEmpty(value)
         && CalculateCheckDigit(value[..^1].AsSpan(), out var checkDigit) 
         && value[^1] == checkDigit;

   private static Boolean CalculateCheckDigit(ReadOnlySpan<Char> value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (value.Length == 0)
      {
         return false;
      }

      var sum = 0;
      var oddPosition = true;
      for (var index = value.Length - 1; index >= 0; index--)
      {
         var digit = value[index].ToIntegerDigit();
         if (digit < 0 || digit > 9)
         {
            return false;
         }
         sum += oddPosition ? _doubledValues[digit] : digit;
         oddPosition = !oddPosition;
      }
      var mod = 10 - (sum % 10);
      checkDigit = mod == 10 ? CharConstants.DigitZero : mod.ToDigitChar();

      return true;
   }
}
