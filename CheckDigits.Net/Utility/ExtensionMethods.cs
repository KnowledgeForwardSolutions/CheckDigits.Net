using System.Runtime.CompilerServices;

namespace CheckDigits.Net.Utility;

public static class ExtensionMethods
{
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
