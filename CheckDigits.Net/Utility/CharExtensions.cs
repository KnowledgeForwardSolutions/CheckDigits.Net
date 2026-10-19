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
      ///   Converts an ASCII decimal digit character ('0'-'9') to its 
      ///   equivalent integer value.
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
   }
}
