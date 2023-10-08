// Ignore Spelling: Npi

namespace CheckDigits.Net;

/// <summary>
///   US National Provider Identifier (NPI) algorithm. Internally, uses the Luhn
///   algorithm with a constant "80840" prefix added to the value.
/// </summary>
/// <remarks>
///   <para>
///   Value length = 10.
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and most two digit 
///   transpositions of adjacent digits (except 09 <-> 90). Will detect most
///   twin errors (i.e. 11 <-> 44) except 22 <-> 55,  33 <-> 66 and 44 <-> 77.
///   </para>
/// </remarks>
public class NpiAlgorithm : ICheckDigitAlgorithm
{
   private const Int32 _calculateLength = 9;
   private const Int32 _validateLength = 10;
   private const Int32 _prefix = 24;
   private static readonly Int32[] _doubledValues = new Int32[] { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.NpiAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.NpiAlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _validateLength)
      {
         return false;
      }

      var sum = _prefix;         // Initializing to prefix (24) has the same effect as prefixing the value with "80840"
      var oddPosition = true;
      for (var index = value.Length - 2; index >= 0; index--)
      {
         var digit = value[index].ToIntegerDigit();
         if (digit < 0 || digit > 9)
         {
            return false;
         }
         sum += oddPosition ? _doubledValues[digit] : digit;
         oddPosition = !oddPosition;
      }
      var checkDigit = (10 - (sum % 10)) % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }
}
