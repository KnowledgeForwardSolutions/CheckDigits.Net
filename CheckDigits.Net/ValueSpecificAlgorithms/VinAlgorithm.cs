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
   private static readonly Int32[] _lookupTable = Chars.Range(Chars.DigitZero, Chars.UpperCaseZ)
      .Select(x => MapCharacter(x))
      .ToArray();
   private const Int32 _expectedLength = 17;
   private const Int32 _checkDigitPosition = 8;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.VinAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.VinAlgorithmName;

   /// <summary>
   ///   Map a character to its integer equivalent in the 
   ///   <see cref="VinAlgorithm"/>. Characters that are not valid for the 
   ///   VinAlgorithm are mapped to -1.
   /// </summary>
   /// <param name="ch">
   ///   The character to map.
   /// </param>
   /// <returns>
   ///   The integer value associated with <paramref name="ch"/>.
   /// </returns>
   public static Int32 MapCharacter(Char ch) => ch switch
   {
      var d when ch >= Chars.DigitZero && ch <= Chars.DigitNine => d.ToIntegerDigit(),
      var a when ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseH => a - Chars.UpperCaseA + 1,
      var j when ch >= Chars.UpperCaseJ && ch <= Chars.UpperCaseN => j - Chars.UpperCaseJ + 1,
      Chars.UpperCaseP => 7,
      Chars.UpperCaseR => 9,
      var s when ch >= Chars.UpperCaseS && ch <= Chars.UpperCaseZ => s - Chars.UpperCaseS + 2,
      _ => -1
   };

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
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
         if (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
         {
            num = _lookupTable[ch - Chars.DigitZero];
         }
         if (num == -1)
         {
            return false;
         }
         sum += num * _weights[index];
      }
      var mod = sum % 11;
      checkDigit = mod == 10 ? Chars.UpperCaseX : mod.ToDigitChar();

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
         if (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
         {
            num = _lookupTable[ch - Chars.DigitZero];
         }
         if (num == -1)
         {
            return false;
         }
         sum += num * _weights[index];
      }
      var mod = sum % 11;
      var checkDigit = mod == 10 ? Chars.UpperCaseX : mod.ToDigitChar();

      return value[_checkDigitPosition] == checkDigit;
   }
}
