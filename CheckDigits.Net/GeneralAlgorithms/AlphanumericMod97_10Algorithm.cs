namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Variation of the ISO/IEC 7064 MOD 97-10 algorithm where alphabetic 
///   characters (A-Z) are mapped to integers (10-35) before calculating the 
///   check digit. The algorithm is case insensitive and lowercase letters are 
///   mapped to their uppercase equivalent before conversion to integers.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z).
///   </para>
///   <para>
///   Check characters calculated by the algorithm are decimal digits (0-9).
///   </para>
///   <para>
///   Assumes that the check characters (if present) are the two right-most 
///   characters in the input value.
///   </para>
///   <para>
///   This algorithm implements <see cref="IMaskedCheckDigitAlgorithm"/> 
///   and can validate values that contain non-check digit characters (such as 
///   spaces or dashes for human readability) when used with an 
///   <see cref="ICheckDigitMask"/>. Note that the trailing two check digit 
///   characters are never masked.
///   </para>
/// </remarks>
public class AlphanumericMod97_10Algorithm : IDoubleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _modulus = 97;
   private const Int32 _radix = 10;
   private const Int32 _validateMinLength = 3;

   // Reduce threshold is calculated to ensure that (sum + digit) * radix will
   // not overflow a 32-bit integer.
   private const Int32 _digitMaxValue = 9;
   private const Int32 _reduceThreshold = (Int32.MaxValue / _radix) - (_digitMaxValue + 1);

   // Precomputed first and second digits for letters 'A' to 'Z' to optimize processing.
   private static readonly Int32[] _letterFirstDigits = Chars.Range(Chars.UpperCaseA, Chars.UpperCaseZ)
      .Select(x => x - Chars.UpperCaseA + 10)
      .Select(x => x / 10)       // Extract first digit (tens place)
      .ToArray();
   private static readonly Int32[] _letterSecondDigits = Chars.Range(Chars.UpperCaseA, Chars.UpperCaseZ)
      .Select(x => x - Chars.UpperCaseA + 10)
      .Select(x => x % 10)       // Extract second digit (ones place)
      .ToArray();

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.AlphanumericMod97_10AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.AlphanumericMod97_10AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigits(
   String value,
   out Char first,
   out Char second)
   {
      first = Chars.NUL;
      second = Chars.NUL;

      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var ch = value[index];
         if (IsDigitCharacter(ch))
         {
            sum = (sum + ch.ToIntegerDigit()) * _radix;
         }
         else if (IsLetterCharacter(ch))
         {
            var offset = ch - (ch < Chars.LowerCaseA ? Chars.UpperCaseA : Chars.LowerCaseA);
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
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      var sum = 0;

      // Process all characters except the last one, which is the second check character.
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         var ch = value[index];
         if (IsDigitCharacter(ch))
         {
            sum = (sum + ch.ToIntegerDigit()) * _radix;
         }
         else if (IsLetterCharacter(ch))
         {
            // Because the underlying algorithm, ISO/IEC MOD 97-10, is designed
            // to process digits, we map each letter to two digits. For example,
            // 'A' is mapped to 10, which is processed as '1' followed by '0'.
            // The first and second digits for each letter are pre-calculated so
            // that we can do a quick lookup during processing instead of doing 
            // the calculation for each letter on the fly.
            var offset = ch - (ch < Chars.LowerCaseA ? Chars.UpperCaseA : Chars.LowerCaseA);
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

      // Add value for second check character. Check characters are always
      // digits.
      var num = value[^1].ToIntegerDigit();
      if (num.IsInvalidDigit())
      {
         return false;
      }
      sum += num;

      return sum % _modulus == 1;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      if (mask is null)
      {
         throw new ArgumentNullException(nameof(mask), Resources.NullMaskMessage);
      }
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;

      // Process maskable characters (trailing two characters are check characters
      // and are not maskable).
      var processLength = value.Length - 2;
      var processedCharacters = 0;
      for (var index = 0; index < processLength; index++)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         var ch = value[index];
         if (IsDigitCharacter(ch))
         {
            sum = (sum + ch.ToIntegerDigit()) * _radix;
         }
         else if (IsLetterCharacter(ch))
         {
            var offset = ch - (ch < Chars.LowerCaseA ? Chars.UpperCaseA : Chars.LowerCaseA);
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
         processedCharacters++;
      }
      if (processedCharacters == 0)
      {
         return false;
      }

      // First check character. Check characters are always digits.
      var num = value[^2].ToIntegerDigit();
      if (num.IsInvalidDigit())
      {
         return false;
      }
      sum = (sum + num) * _radix;
      if (sum >= _reduceThreshold)
      {
         sum %= _modulus;
      }

      // Add value for second check character.
      num = value[^1].ToIntegerDigit();
      if (num.IsInvalidDigit())
      {
         return false;
      }
      sum += num;

      return sum % _modulus == 1;
   }

   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   private Boolean IsDigitCharacter(Char ch) => ch >= Chars.DigitZero && ch <= Chars.DigitNine;

   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   private Boolean IsLetterCharacter(Char ch) =>
      (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ) 
      || (ch >= Chars.LowerCaseA && ch <= Chars.LowerCaseZ);
}
