// Ignore Spelling: Iban

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   International Bank Account Number (IBAN) algorithm. Variation of the 
///   ISO/IEC 7064 MOD 97-10 algorithm that map alphabetic characters A-Z to
///   integer values (10-35) before calculating the check digits.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z).
///   </para>
///   <para>
///   Check digits calculated by the algorithm are two decimal digits (0-9).
///   </para>
/// </remarks>
public class IbanAlgorithm : IDoubleCheckDigitAlgorithm
{
   private const Int32 _minimumLength = 5;
   private const Int32 _modulus = 97;
   private const Int32 _radix = 10;
   private const Int32 _reduceThreshold = Int32.MaxValue / _radix;
   private static readonly Int32[] _letterFirstDigits = Chars.Range(Chars.UpperCaseA, Chars.UpperCaseZ)
      .Select(x => x - Chars.UpperCaseA + 10)
      .Select(x => x / 10)
      .ToArray();
   private static readonly Int32[] _letterSecondDigits = Chars.Range(Chars.UpperCaseA, Chars.UpperCaseZ)
      .Select(x => x - Chars.UpperCaseA + 10)
      .Select(x => x % 10)
      .ToArray();

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.IbanAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.IbanAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigits(
      String value,
      out Char first,
      out Char second)
   {
      first = Chars.NUL;
      second = Chars.NUL;

      if (String.IsNullOrEmpty(value) || value.Length < _minimumLength)
      {
         return false;
      }

      Int32 start;
      Int32 end;
      var sum = 0;

      // Specification calls for moving first 4 characters to end of string.
      // Here, we process in two different ranges to avoid having to create a
      // new string.
      for (var pass = 0; pass < 2; pass++)
      {
         if (pass == 0)
         {
            start = 4;
            end = value.Length - 1;
         }
         else
         {
            start = 0;
            end = 1;
         }

         // This is essentially the same loop as for the ISO/IEC 7064 MOD 97-10
         // algorithm, except that we process two integers when the current
         // character is A-Z.
         for (var index = start; index <= end; index++)
         {
            var ch = value[index];
            if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
            {
               sum = (sum + ch.ToIntegerDigit()) * _radix;
            }
            else if (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ)
            {
               var offset = ch - Chars.UpperCaseA;
               sum = (sum + _letterFirstDigits[offset]) * _radix;
               if (sum >= _reduceThreshold)
               {
                  sum %= _modulus;
               }
               sum = (sum + _letterSecondDigits[offset]) * _radix;
            }
            else
            {
               return false;
            }
            if (sum >= _reduceThreshold)
            {
               sum %= _modulus;
            }
         }
      }

      // Per ISO/IEC 7064, two character algorithms perform one final pass with
      // effective character value of zero.
      sum = sum * _radix % _modulus;

      var checkSum = _modulus - sum + 1;
      var quotient = checkSum / _radix;
      var remainder = checkSum % _radix;

      first = quotient.ToDigitChar();
      second = remainder.ToDigitChar();

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _minimumLength)
      {
         return false;
      }

      Int32 start;
      Int32 end;
      var sum = 0;

      // Specification calls for moving first 4 characters to end of string.
      // Here, we process in two different ranges to avoid having to create a
      // new string.
      for (var pass = 0; pass < 2; pass++)
      {
         if (pass == 0)
         {
            start = 4;
            end = value.Length - 1;
         }
         else
         {
            start = 0;
            end = 2;
         }

         // This is essentially the same loop as for the ISO/IEC 7064 MOD 97-10
         // algorithm, except that we process two integers when the current
         // character is A-Z.
         for (var index = start; index <= end; index++)
         {
            var ch = value[index];
            if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
            {
               sum = (sum + ch.ToIntegerDigit()) * _radix;
            }
            else if (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ)
            {
               var offset = ch - Chars.UpperCaseA;
               sum = (sum + _letterFirstDigits[offset]) * _radix;
               if (sum >= _reduceThreshold)
               {
                  sum %= _modulus;
               }
               sum = (sum + _letterSecondDigits[offset]) * _radix;
            }
            else
            {
               return false;
            }
            if (sum >= _reduceThreshold)
            {
               sum %= _modulus;
            }
         }
      }

      // Add value for second check character. Check characters are always digits.
      var num = value[3].ToIntegerDigit();
      if (num < 0 || num > 9)
      {
         return false;
      }
      sum += num;

      return sum % _modulus == 1;
   }
}
