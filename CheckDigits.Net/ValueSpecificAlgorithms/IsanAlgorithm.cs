// Ignore Spelling: Isan

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   International Standard Audiovisual Number (ISAN) algorithm. Variation of
///   ISO/IEC 7064 MOD 37,36 algorithm adapted for use with ISAN values formatted
///   as human readable strings.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are hexadecimal characters (0-9, A-F).
///   </para>
///   <para>
///   Check character(s) calculated by the algorithm is an alphanumeric character (0-9, A-Z).
///   </para>
///   <para>
///   Will detect all single character transcription errors, all or nearly all 
///   two character transposition errors, all or nearly all jump transposition 
///   errors, all or nearly all circular shift errors and a high proportion of 
///   double character transcription errors (two separate single character 
///   transcription errors in the same value).
///   </para>
/// </remarks>
public class IsanAlgorithm : ICheckDigitAlgorithm
{
   private readonly Int32 _modulus = 36;
   private readonly Int32 _modulusPlus1 = 37;
   private const String _prefix = "ISAN ";

   private const Int32 _validateExpectedLength = 26;
   private const Int32 _validateFirstCheckCharIndex = 16;

   private const Int32 _validateFormattedExpectedRootLength = 26;
   private const Int32 _validateFormattedExpectedVersionLength = 38;

   // Mask characters:
   // a - alphabetic character
   // b - betanumeric character
   // d - digit character
   // h - hexadecimal character
   // # - check character
   // _ - wildcard character; ignored
   // Another character must match exactly
   private const String _mask = "ISAN hhhh-hhhh-hhhh-hhhh-#-hhhh-hhhh-#";

   private const Int32 _rootOnlyLength = 26;
   private const Int32 _rootCheckCharacterIndex = _rootOnlyLength - 1;
   private const Int32 _rootPlusVersionLength = 38;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.IsanAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.IsanAlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _validateExpectedLength)
      {
         return false;
      }

      Char ch;
      Int32 num;
      var product = _modulus;
      for (var index = 0; index < value.Length - 1; index++)
      {
         ch = value[index];
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else if ((ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseF)      // Hexadecimal chars only, unless check character position
                 || (ch >= CharConstants.UpperCaseG && ch <= CharConstants.UpperCaseZ && index == _validateFirstCheckCharIndex))
         {
            num = ch - CharConstants.UpperCaseA + 10;
         }
         else
         {
            return false;
         }

         // Root segment check character.
         if (index == _validateFirstCheckCharIndex)
         {
            var rootProduct = product + num;
            if (rootProduct % _modulus != 1)
            {
               return false;
            }
         }
         else
         {
            product += num;
            if (product > _modulus)
            {
               product -= _modulus;
            }
            product *= 2;
            if (product >= _modulusPlus1)
            {
               product -= _modulusPlus1;
            }
         }
      }

      ch = value[^1];
      if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
      {
         num = ch.ToIntegerDigit();
      }
      else if (ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ)
      {
         num = ch - CharConstants.UpperCaseA + 10;
      }
      else
      {
         return false;
      }
      product += num;

      return product % _modulus == 1;
   }

   /// <inheritdoc/>
   public Boolean ValidateFormatted(String value)
   {
      if (String.IsNullOrEmpty(value)
         || (value.Length != _rootOnlyLength && value.Length != _rootPlusVersionLength)
         || !value.StartsWith(_prefix, StringComparison.Ordinal)) 
      {
         return false;
      }

      Char ch;
      Int32 num;
      var product = _modulus;
      for (var index = _prefix.Length; index < value.Length - 1; index++)
      {
         ch = value[index];
         if (index == 9 || index == 14 || index == 19 || index == 24 || index == 26 || index == 31 || index == 36)
         {
            // Ignore group separator dash characters. Otherwise it's an invalid character.
            if (ch == CharConstants.Dash)
            {
               continue;
            }

            return false;
         }

         if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else if (ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ)
         {
            num = ch - CharConstants.UpperCaseA + 10;
         }
         else
         {
            return false;
         }

         // Root segment check character.
         if (index == _rootCheckCharacterIndex)
         {
            var rootProduct = product + num;
            if (rootProduct % _modulus != 1)
            {
               return false;
            }
         }
         else
         {
            // Check for non-hexadecimal value.
            if (num > 15)
            {
               return false;
            }

            product += num;
            if (product > _modulus)
            {
               product -= _modulus;
            }
            product *= 2;
            if (product >= _modulusPlus1)
            {
               product -= _modulusPlus1;
            }
         }
      }

      ch = value[^1];
      if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
      {
         num = ch.ToIntegerDigit();
      }
      else if (ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ)
      {
         num = ch - CharConstants.UpperCaseA + 10;
      }
      else
      {
         return false;
      }
      product += num;

      return product % _modulus == 1;
   }
}
