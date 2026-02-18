namespace CheckDigits.Net.Utility;

public static class ExtensionMethods
{
   /// <summary>
   ///   Determines if the specified integer value (generally returned by 
   ///   <see cref="ToIntegerDigit(Char)"/>) is not a valid single digit integer.
   /// </summary>
   /// <remarks>
   ///   <see cref="ToIntegerDigit(Char)"/> converts a single character to an
   ///   integer by subtracting the character zero ('0') from the character to 
   ///   convert. If the character to convert is not '0' - '9', then the result
   ///   will be less than 0 or greater than 9.
   /// </remarks>
   /// <param name="digit">
   ///   The integer to evaluate for validity. Must be in the range 0 to 9 to be 
   ///   considered valid.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if the digit is less than 0 or greater than 9; 
   ///   otherwise, <see langword="false"/>.
   /// </returns>
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public static Boolean IsInvalidDigit(this Int32 digit)
      => digit < 0 || digit > 9;

   /// <summary>
   ///   Determines whether the specified integer value is an invalid extended 
   ///   decimal check digit.
   /// </summary>
   /// <param name="value">
   ///   The integer value to evaluate as an extended decimal check digit. Must 
   ///   be in the range 0 to 10, inclusive.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if the value is less than 0 or greater than 10; 
   ///   otherwise, <see langword="false"/>.
   /// </returns>
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public static Boolean IsInvalidExtendedDecimalCheckDigit(this Int32 value)
      => value < 0 || value > 10;

   /// <summary>
   ///   Get the equivalent ASCII digit character for an integer between 0-9;
   /// </summary>
   /// <param name="num">
   ///   The <see cref="Int32"/> to convert.
   /// </param>
   /// <returns>
   ///   The equivalent ASCII digit character ('0'-'9').
   /// </returns>
   /// <remarks>
   ///   If <paramref name="num"/> is not between 0-9 then this method will
   ///   return an unexpected value
   /// </remarks>
   public static Char ToDigitChar(this Int32 num) => (Char)(Chars.DigitZero + num);

   /// <summary>
   ///   Converts the specified character to its extended decimal check digit 
   ///   value, interpreting 'X' as 10 and numeric digits as their integer 
   ///   equivalents.
   /// </summary>
   /// <param name="ch">
   ///   The character to convert. Must be a numeric digit ('0'-'9') or the 
   ///   uppercase letter 'X'.
   /// </param>
   /// <returns>
   ///   An integer representing the extended check digit value. Returns 10 if 
   ///   the character is 'X'; otherwise, returns the integer value of the 
   ///   digit.
   /// </returns>
   /// <remarks>
   ///   If <paramref name="ch"/> is not 'X' or '0'-'9' then this method will
   ///   return an unexpected value
   /// </remarks>
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public static Int32 ToExtendedDecimalCheckDigit(this Char ch)
      => ch == Chars.UpperCaseX ? 10 : ch - Chars.DigitZero;

   /// <summary>
   ///   Get the integer equivalent of an ASCII digit character.
   /// </summary>
   /// <param name="ch">
   ///   The <see cref="Char"/> to convert.
   /// </param>
   /// <returns>
   ///   The integer equivalent of the ASCII character.
   /// </returns>
   /// <remarks>
   ///   If <paramref name="ch"/> is not an ASCII digit char (0-9) then this 
   ///   method will return a value that is not between 0-9.
   /// </remarks>
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   public static Int32 ToIntegerDigit(this Char ch) => ch - Chars.DigitZero;
}
