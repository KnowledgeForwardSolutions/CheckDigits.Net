namespace CheckDigits.Net;

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
public class VinAlgorithm : ISingleCheckDigitAlgorithm
{
   private static readonly Int32[] _weights = new[] { 8, 7, 6, 5, 4, 3, 2, 10, 0, 9, 8, 7, 6, 5, 4, 3, 2 };
   private const Int32 _expectedLength = 17;
   private const Int32 _checkDigitPosition = 8;

   // Table used to transliterate characters to numeric equivalents. (-1 for invalid chars)        :,  ;,  <,  =,  >,  ?,  @, A, B, C, D, E, F, G, H,  I, J, K, L, M, N,  O, P,  Q, R, S, T, U, V, W, X, Y, Z  
   private static readonly Int32[] _transliterationValues = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, -1, -1, -1, -1, -1, -1, -1, 1, 2, 3, 4, 5, 6, 7, 8, -1, 1, 2, 3, 4, 5, -1, 7, -1, 9, 2, 3, 4, 5, 6, 7, 8, 9 };

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.VinAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.VinAlgorithmName;

   /// <summary>
   ///   Transliterate a character contained in a VIN to its integer equivalent.
   /// </summary>
   /// <param name="ch">
   ///   The character to convert.
   /// </param>
   /// <returns>
   ///   An integer between 0 and 9, or -1 if the <paramref name="ch"/> was not
   ///   a allowed in a VIN.
   /// </returns>
   public static Int32 TransliterateCharacter(Char ch)
   {
      var index = ch - CharConstants.DigitZero;
      return index < 0 || index > 42 ? -1 : _transliterationValues[index];
   }

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

         var currentValue = TransliterateCharacter(value[index]);
         if (currentValue == -1)
         {
            return false;
         }
         sum += currentValue * _weights[index];
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

         var currentValue = TransliterateCharacter(value[index]);
         if (currentValue == -1)
         {
            return false;
         }
         sum += currentValue * _weights[index];
      }
      var mod = sum % 11;
      var checkDigit = mod == 10 ? CharConstants.UpperCaseX : mod.ToDigitChar();

      return value[_checkDigitPosition] == checkDigit;
   }
}
