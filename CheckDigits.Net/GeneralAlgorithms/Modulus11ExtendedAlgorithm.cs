namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Modulus 11 (Extended) algorithm where every digit is weighted by its 
///   position in the value, starting from the right-most position, and the 
///   check digit being the modulus 11 of the sum of weighted values. Modulus 11
///   allows for 11 possible check "digits" and this algorithm extends the
///   possible check digit characters to include '0'-'9' and 'X' (represents a
///   check value of 10, which is not possible to represent with a single digit
///   character).
/// </summary>
/// <remarks>
///   <para>
///   This algorithm replaces the depreciated <see cref="Modulus11Algorithm"/>.
///   </para>
///   <para>
///   See <see cref="Modulus11DecimalAlgorithm"/> for a related modulus 11
///   algorithm that restricts check characters to '0'-'9', but at the cost of 
///   rejecting one out of every 11 values, or approximately 9.09%.
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9) or an 
///   uppercase 'X'.
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and all two digit 
///   transposition errors.
///   </para>
///   <para>
///   Maximum length allowed is 9 characters for calculating a new check digit 
///   and 10 characters for validating a value that contains a check digit.
///   </para>
/// </remarks>
public sealed class Modulus11ExtendedAlgorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _tryCalculateMaxLength = 9;
   private const Int32 _validateMinLength = 2;
   private const Int32 _validateMaxLength = 10;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Modulus11ExtendedAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Modulus11ExtendedAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value) || value.Length > _tryCalculateMaxLength)
      {
         return false;
      }

      var s = 0;
      var t = 0;
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var currentDigit = value![index].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }
         t += currentDigit;
         s += t;
      }
      s += t;

      var mod = (11 - (s % 11)) % 11;
      checkDigit = mod < 10 ? mod.ToDigitChar() : Chars.UpperCaseX;

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value)
          || value.Length < _validateMinLength
          || value.Length > _validateMaxLength)
      {
         return false;
      }

      var s = 0;
      var t = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         var currentDigit = value![index].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }
         t += currentDigit;
         s += t;
      }
      s += t;

      var checkDigit = value[^1].ToExtendedDecimalCheckDigit();
      if (checkDigit.IsInvalidExtendedDecimalCheckDigit())
      {
         return false;
      }

      s += checkDigit;

      return (s % 11) == 0;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var s = 0;
      var t = 0;
      var processedDigits = 0;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         var currentDigit = value![index].ToIntegerDigit();
         if (currentDigit.IsInvalidDigit())
         {
            return false;
         }
         t += currentDigit;
         s += t;
         processedDigits++;
      }
      if (processedDigits == 0 || processedDigits >= _validateMaxLength)
      {
         return false;
      }
      s += t;

      var checkDigit = value[^1].ToExtendedDecimalCheckDigit();
      if (checkDigit.IsInvalidExtendedDecimalCheckDigit())
      {
         return false;
      }

      s += checkDigit;

      return (s % 11) == 0;
   }
}
