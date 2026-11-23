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

   // Lookup tables created by mapping alphabetic characters A-Z to integer
   // values 10-35 and then extracting the 10's digit and 1's digit into
   // separate tables.
   private static readonly Int32[] _tensDigits = CharacterMap.CreateCharacterMap(
      Chars.UpperCaseA,       // A -> 10
      Chars.UpperCaseZ,       // Z -> 35
      ch => ch.MapAsciiUppercaseAlphanumeric().TensDigit());
   private static readonly Int32[] _onesDigit = CharacterMap.CreateCharacterMap(
      Chars.UpperCaseA,       // A -> 10
      Chars.UpperCaseZ,       // Z -> 35
      ch => ch.MapAsciiUppercaseAlphanumeric().OnesDigit());

   private const Int32 AccountNumberStart = 4;     // Zero-based offset of the start of the country-specific account number
   private const Int32 CheckDigitStart = 2;        // Zero-based offset of the start of the two check digit characters
   private const Int32 CountryCodeStart = 0;       // Zero-based offset of the start of the country code segment
   private const Int32 AccountSegment = 0;
   private const Int32 CountryCodeSegment = 1;

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

      // Specification calls for moving first 4 characters to end of string.
      // Here, we process in two different ranges to avoid having to create a
      // new string.
      var sum = 0;
      for (var segment = 0; segment <= CountryCodeSegment; segment++)
      {
         Int32 start;
         Int32 end;
         if (segment == AccountSegment)
         {
            start = AccountNumberStart;
            end = value.Length - 1;
         }
         else
         {
            start = CountryCodeStart;
            end = CheckDigitStart - 1;    // Check digits positions are excluded because we're calculating their values
         }

         // This is essentially the same loop as for the ISO/IEC 7064 MOD 97-10
         // algorithm, except that we process two integers when the current
         // character is A-Z.
         for (var index = start; index <= end; index++)
         {
            var ch = value[index];
            if (ch.IsAsciiDigit())
            {
               sum = (sum + ch.MapAsciiDigit()) * _radix;
            }
            else if (ch.IsAsciiLetterUpper())
            {
               sum = ProcessLetter(sum, ch);
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

      first = quotient.ToAsciiDigit();
      second = remainder.ToAsciiDigit();

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _minimumLength)
      {
         return false;
      }

      // Process value as two segments. First, the country specific account
      // number (from zero-based offset 4 to end of string) and then the first
      // three characters of the value which are the two character country code
      // and the first check digit.
      //
      // Note that using ReadOnlySpan<Char> and Ranges to represent the two 
      // regions was profiled and benchmarks showed that it was ~10% slower than
      // this index based approach.
      var sum = 0;
      Char ch;
      for (var segment = 0; segment <= CountryCodeSegment; segment++)
      {
         Int32 start;
         Int32 end;
         if (segment == AccountSegment)
         {
            start = AccountNumberStart;
            end = value.Length - 1;
         }
         else
         {
            start = CountryCodeStart;
            end = CheckDigitStart;
         }

         // This is essentially the same loop as for the ISO/IEC 7064 MOD 97-10
         // algorithm, except that we process two integers when the current
         // character is A-Z.
         for (var index = start; index <= end; index++)
         {
            ch = value[index];
            if (ch.IsAsciiDigit())
            {
               sum = (sum + ch.MapAsciiDigit()) * _radix;
            }
            else if (ch.IsAsciiLetterUpper())
            {
               sum = ProcessLetter(sum, ch);
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

      // Add value for second check character. Check characters are always
      // digits.
      ch = value[3];
      if (!ch.IsAsciiDigit())
      {
         return false;
      }
      var num = ch.MapAsciiDigit();
      sum += num;

      return sum % _modulus == 1;
   }

   /// <summary>
   ///   Process a letter character by mapping the letter to a number between
   ///   10 and 35. Then the 10's digit and the 1's digit are processed as 
   ///   separate numbers. 
   /// </summary>
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   private static Int32 ProcessLetter(Int32 sum, Char ch)
   {
      // Determine zero-based offset of the character into the lookup tables.
      // The lookup tables are indexed 0-25 so we use MapAsciiUppercaseAlpha to
      // return 0-25 instead of MapAsciiUppercaseAlphanumeric which would return
      // 10-35.
      var offset = ch.MapAsciiUppercaseAlpha();

      // Process the 10's digit and allow for value reduction if necessary.
      sum = (sum + _tensDigits[offset]) * _radix;
      if (sum >= _reduceThreshold)
      {
         sum %= _modulus;
      }
      
      // Process 1's digit. Value reduction not performed because main loop will
      // handle that instead.
      return (sum + _onesDigit[offset]) * _radix;
   }
}
