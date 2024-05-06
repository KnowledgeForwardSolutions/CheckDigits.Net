// Ignore Spelling: Cusip

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used by North American security identification number (CUSIP
///   number). Has similarities with ISIN and Luhn algorithms.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z) and *, @, #
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
/// </remarks>
public class CusipAlgorithm : ICheckDigitAlgorithm
{
   private const Int32 _validateLength = 9;
   private const Int32 _letterOffset = 55;      // Value needed to subtract from an ASCII uppercase letter to transform A-Z to 10-35
   private static readonly Int32[] _evenValues = GetLookupTable(false);
   private static readonly Int32[] _oddValues = GetLookupTable(true);

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.CusipAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.CusipAlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _validateLength)
      {
         return false;
      }

      var sum = 0;
      var oddPosition = true;
      Int32 num;
      for (var index = value.Length - 2; index >= 0; index--)
      {
         var ch = value[index];
         if (ch >= Chars.HashMark && ch <= Chars.UpperCaseZ)
         {
            var offset = ch - Chars.HashMark;
            num = oddPosition ? _oddValues[offset] : _evenValues[offset];
         }
         else
         {
            return false;
         }

         if (num > -1)
         {
            sum += num;
         }
         else
         {
            return false;
         }

         oddPosition = !oddPosition;
      }
      var checkDigit = (10 - (sum % 10)) % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }

   private const String _chars = "#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ";

   private static Int32[] GetLookupTable(Boolean odd)
      => _chars.Select(x =>
      {
         var num = x switch
         {
            var d when x >= Chars.DigitZero && x <= Chars.DigitNine => d.ToIntegerDigit(),
            var c when x >= Chars.UpperCaseA && x <= Chars.UpperCaseZ => c - _letterOffset,
            Chars.Asterisk => 36,
            Chars.AtSign => 37,
            Chars.HashMark => 38,
            _ => -1
         };
         if (num == -1)
         {
            return -1;
         }
         if (odd)
         {
            num *= 2;
         }

         return (num / 10) + (num % 10);
      })
      .ToArray();
}
