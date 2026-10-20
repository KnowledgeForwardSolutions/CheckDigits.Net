namespace CheckDigits.Net.Utility;

/// <summary>
///   Provides extension methods for System.Char (char) values.
/// </summary>
/// <remarks>
///   Contains static extension methods intended for internal use within the 
///   assembly; see individual members for details on behavior and thread 
///   safety.
/// </remarks>
internal static class CharExtensions
{
   extension(Char ch)
   {
      /// <summary>
      ///   Maps the ASCII decimal digit characters ('0'-'9') to their integer
      ///   equivalents (0-9).
      /// </summary>
      /// <remarks>
      ///   Does not validate that the character is a decimal digit; non-digit 
      ///   characters produce a value based on their character code offset from 
      ///   the ASCII character '0'.
      /// </remarks>
      /// <returns>
      ///   The integer value of the digit character.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Int32 ToDecimalDigit() => ch - Chars.DigitZero;

      /// <summary>
      ///   Maps extended ASCII decimal digit characters ('0'-'9', 'X') to their
      ///   integer equivalents (0-9, 10).
      /// </summary>
      /// <remarks>
      ///   Does not validate that the character is an extended decimal digit; 
      ///   non-digit characters (other than 'X') produce a value based on their 
      ///   character code offset from the ASCII character '0'.
      /// </remarks>
      /// <returns>
      ///   10 when the character is 'X'; otherwise, the integer value of the 
      ///   digit character.
      /// </returns>
      public Int32 ToExtendedDecimalDigit()
         => ch == Chars.UpperCaseX ? 10 : ch - Chars.DigitZero;
   }
}
