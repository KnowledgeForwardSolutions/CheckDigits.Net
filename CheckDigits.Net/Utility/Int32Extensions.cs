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
      ///   Converts an integer value between 0 and 9 to its corresponding ASCII 
      ///   digit character.
      /// </summary>
      /// <remarks>
      ///   If <paramref name="num"/> is not between 0-9 then this method will
      ///   return an unexpected value.
      /// </remarks>
      /// <returns>
      ///   The ASCII digit character in the range '0'-'9' that represents the 
      ///   integer value.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Char ToAsciiDigit() => (Char)(Chars.DigitZero + num);

      /// <summary>
      ///   Converts an integer value between 0 and 10 to its corresponding 
      ///   ASCII extended decimal digit character (where 10 is represented by
      ///   'X').
      /// </summary>
      /// <remarks>
      ///   If <paramref name="num"/> is not between 0-10 then this method will
      ///   return an unexpected value.
      /// </remarks>
      /// <returns>
      ///   The ASCII digit character in the range '0'-'9' (for values 0-9) or
      ///   'X' for the value = 10.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Char ToAsciiExtendedDecimalDigit()
         => num == 10 ? Chars.UpperCaseX : num.ToAsciiDigit();

      /// <summary>
      /// Converts an integer value between 0 and 25 to its corresponding ASCII
      /// uppercase letter ('A'-'Z').
      /// </summary>
      /// <remarks>
      ///   If <paramref name="num"/> is not between 0-25 then this method will
      ///   return an unexpected value.
      /// </remarks>
      /// <returns>
      ///   The ASCII digit character in the range 'A'-'Z' that represents the 
      ///   integer value.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Char ToAsciiUppercaseAlpha()
         => (Char)(Chars.UpperCaseA + num);
   }
}
