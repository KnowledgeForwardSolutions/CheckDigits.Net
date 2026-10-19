namespace CheckDigits.Net.Utility;

/// <summary>
///   Provides extension methods for System.Int32 (int) values.
/// </summary>
/// <remarks>
///   Contains static extension methods intended for internal use within the 
///   assembly; see individual members for details on behavior and thread 
///   safety.
/// </remarks>
internal static class Int32Extensions
{
   extension(Int32 num)
   {
      /// <summary>
      ///   Determines whether <paramref name="num"/> is within the inclusive 
      ///   range 0 to 9.
      /// </summary>
      /// <remarks>
      ///   <para>
      ///      The range 0 - 9 are treated as valid individual digits; negative 
      ///      values and values greater than 9 are invalid.
      ///   </para>
      ///   <para>
      ///      CheckDigits.Net optimizes conversion of characters to their 
      ///      integer equivalents by subtracting the constant '0' from the 
      ///      character. For '0'-'9', this operation results in the integer
      ///      equivalent. But for characters other than '0'-'9', the 
      ///      subtraction results in a negative value (for characters that 
      ///      precede '0' in the ASCII table) or a value greater than 9 (for 
      ///      characters that follow '9' in the ASCII table). This method is 
      ///      used to ensure that the conversion of a character to an integer 
      ///      resulted in a valid integer digit.
      ///   </para>
      /// </remarks>
      /// <returns>
      ///   <see langword="true"/> if <paramref name="num"/> between 0-9
      ///   (inclusive); otherwise, <see langword="false"/>.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Boolean IsValidDigit() => num is >= 0 and <= 9;

      /// <summary>
      ///   Determines whether the check digit value is within the valid 
      ///   extended decimal range (0 through 10, inclusive).
      /// </summary>
      /// <remarks>
      ///   <para>
      ///      The range 0 - 10 are treated as valid check digits; negative 
      ///      values and values greater than 10 are invalid.
      ///   </para>
      ///   <para>
      ///     Modulus 11 check digit algorithms produce check digits in the 
      ///     range 0–10. Because the value 10 cannot be represented by a single 
      ///     digit character, an extended representation is used (commonly 'X' 
      ///     for 10). This method should be applied only to check characters; 
      ///     non-check characters should use <see cref="IsValidDigit"/>.
      ///   </para>
      ///   Validates the backing value as an extended decimal check digit; valid values are 0
      /// through 10 inclusive.
      /// </remarks>
      /// <returns>
      ///   <see langword="true"/> if the check digit value is between 0 and 10, 
      ///   inclusive; otherwise, <see langword="false"/>.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Boolean IsValidExtendedDecimalCheckDigit() => num is >= 0 and <= 10;
   }
}
