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
