namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   North American (US and Canada) Vehicle Identification Number (VIN) modulus
///   11 algorithm.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are decimal digits (0-9) and upper-case alphabetic 
///   characters (A-Z, excluding I, O & Q).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9) or an 
///   uppercase 'X'.
///   </para>
///   <para>
///   Assumes that the check digit is located in the 9th position of a 17 
///   character value. TryCalculateCheckDigit assumes that the check digit 
///   position is present and is ignored when calculating the check digit.
///   </para>
/// </remarks>
public sealed class VinAlgorithm : ISingleCheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2];
   private static readonly Int32[] _lookupTable = CharacterMapUtility.GetVinCharacterMap();
   private const Int32 _expectedLength = 17;
   private const Int32 _checkDigitPosition = 8;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.VinAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.VinAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value) || value.Length != _expectedLength)
      {
         return false;
      }

      var sum = 0;
      for (var index = 0; index < _weights.Length; index++)
      {
         if (index == _checkDigitPosition)
         {
               continue;
         }

         var ch = value[index];
         var num = -1;
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ)
         {
            num = _lookupTable[ch - CharConstants.DigitZero];
         }
         if (num == -1)
         {
            return false;
         }
         sum += num * _weights[index];
      }
      var mod = sum % 11;
      checkDigit = mod == 10 ? CharConstants.UpperCaseX : mod.ToDigitChar();

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _expectedLength)
      {
         return false;
      }

      var sum = 0;
      for (var index = 0; index < _weights.Length; index++)
      {
         if (index == _checkDigitPosition)
         {
               continue;
         }

         var ch = value[index];
         var num = -1;
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ)
         {
            num = _lookupTable[ch - CharConstants.DigitZero];
         }
         if (num == -1)
         {
            return false;
         }
         sum += num * _weights[index];
      }
      var mod = sum % 11;
      var checkDigit = mod == 10 ? CharConstants.UpperCaseX : mod.ToDigitChar();

      return value[_checkDigitPosition] == checkDigit;
   }
}
