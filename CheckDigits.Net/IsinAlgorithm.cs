// Ignore Spelling: Isin

namespace CheckDigits.Net;

/// <summary>
///   Variation of the Luhn algorithm that supports alphanumeric characters in 
///   the input.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z).
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
///   transpositions of adjacent digits (except 09 <-> 90). Will not detect 
///   transpositions of two letters. Will detect most twin errors 
///   (i.e. 11 <-> 44) except 22 <-> 55,  33 <-> 66 and 44 <-> 77.
///   </para>
/// </remarks>
public class IsinAlgorithm : ISingleCheckDigitAlgorithm
{
   private const Int32 _calculateLength = 11;
   private const Int32 _validateLength = 12;
   private const Int32 _letterOffset = 55;      // Value needed to subtract from an ASCII uppercase letter to transform A-Z to 10-35
   private static readonly Int32[] _doubledValues = new Int32[] { 0, 2, 4, 6, 8, 1, 3, 5, 7, 9 };

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.IsinAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.IsinAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value) || value.Length != _calculateLength)
      {
         return false;
      }

      var sum = 0;
      var oddPosition = true;
      for (var index = value.Length - 1; index >= 0; index--)
      {
         var ch = value[index];
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
         {
            var digit = ch.ToIntegerDigit();
            sum += oddPosition ? _doubledValues[digit] : digit;
            oddPosition = !oddPosition;
         }
         else if (ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ)
         {
            var number = ch - _letterOffset;
            var firstDigit = number / 10;
            var secondDigit = number % 10;
            sum += oddPosition
               ? firstDigit + _doubledValues[secondDigit]
               : _doubledValues[firstDigit] + secondDigit;
         }
         else
         {
            return false;
         }
      }
      var mod = 10 - (sum % 10);
      checkDigit = mod == 10 ? CharConstants.DigitZero : mod.ToDigitChar();

      return true;
   }

   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _validateLength)
      {
         return false;
      }

      var sum = 0;
      var oddPosition = true;
      for (var index = value.Length - 2; index >= 0; index--)
      {
         var ch = value[index];
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
         {
            var digit = ch.ToIntegerDigit();
            sum += oddPosition ? _doubledValues[digit] : digit;
            oddPosition = !oddPosition;
         }
         else if (ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ)
         {
            var number = ch - _letterOffset;
            var firstDigit = number / 10;
            var secondDigit = number % 10;
            sum += oddPosition
               ? firstDigit + _doubledValues[secondDigit]
               : _doubledValues[firstDigit] + secondDigit;
         }
         else
         {
            return false;
         }
      }
      var checkDigit = (10 - (sum % 10)) % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }
}
