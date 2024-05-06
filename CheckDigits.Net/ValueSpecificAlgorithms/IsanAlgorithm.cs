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
         if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else if ((ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseF)      // Hexadecimal chars only, unless check character position
                 || (ch >= Chars.UpperCaseG && ch <= Chars.UpperCaseZ && index == _validateFirstCheckCharIndex))
         {
            num = ch - Chars.UpperCaseA + 10;
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
      if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
      {
         num = ch.ToIntegerDigit();
      }
      else if (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ)
      {
         num = ch - Chars.UpperCaseA + 10;
      }
      else
      {
         return false;
      }
      product += num;

      return product % _modulus == 1;
   }

   /// <summary>
   ///   Determine if the <paramref name="value"/> contains a valid check digit
   ///   (or check digits).
   /// </summary>
   /// <param name="value">
   ///   The value to validate. 
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if the check digit(s) contained in
   ///   <paramref name="value"/> matches the check digit(s) calculated by this
   ///   algorithm; otherwise <see langword="false"/>.
   /// </returns>
   /// <remarks>
   ///   Validate will return <see langword="false"/> if <paramref name="value"/>
   ///   is mal-formed. Examples of mal-formed values are <see langword="null"/>,
   ///   <see cref="String.Empty"/> or a string that is of invalid length for 
   ///   this algorithm.
   /// </remarks>
   public Boolean ValidateFormatted(String value)
   {
      if (String.IsNullOrEmpty(value)
         || (value.Length != _validateFormattedExpectedRootLength && value.Length != _validateFormattedExpectedVersionLength))
      {
         return false;
      }

      Char ch;
      Int32 num;
      var product = _modulus;
      for (var index = 0; index < value.Length - 1; index++)
      {
         ch = value[index];
         var maskChar = _mask[index];
         switch (maskChar)
         {
            case 'h':
               if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
               {
                  num = ch.ToIntegerDigit();
               }
               else if (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseF)
               {
                  num = ch - Chars.UpperCaseA + 10;
               }
               else
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
               break;

            case '#':
               if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
               {
                  num = ch.ToIntegerDigit();
               }
               else if (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ)
               {
                  num = ch - Chars.UpperCaseA + 10;
               }
               else
               {
                  return false;
               }
               var rootProduct = product + num;
               if (rootProduct % _modulus != 1)
               {
                  return false;
               }
               break;

            default:
               if (ch != maskChar)
               {
                  return false;
               }
               break;
         }
      }

      ch = value[^1];
      if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
      {
         num = ch.ToIntegerDigit();
      }
      else if (ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ)
      {
         num = ch - Chars.UpperCaseA + 10;
      }
      else
      {
         return false;
      }
      product += num;

      return product % _modulus == 1;
   }
}
