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
      ///   Determine the next weight to apply when calculating an ICAO 9303
      ///   check digit.
      /// </summary>
      /// <remarks>
      ///   <para>
      ///      The weights used by the ICAO 9303 algorithm are 7, 3, 1 with the 
      ///      next weight after 1 rolling back over to 7.
      ///   </para>
      ///   <para>
      ///      <paramref name="num"/> is not checked to confirm that it is a 
      ///      valid ICAO 9303 weight. If it is not a valid weight then the
      ///      result is undefined.
      ///   </para>
      /// </remarks>
      /// <returns>
      ///   The ICAO 9303 weight that follows the supplied 
      ///   <paramref name="num"/>.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Int32 Icao9303NextWeight()
         => num == 1 ? 7 : num / 2;     // Integer division, 7 -> 3, 3 -> 1; 1 -> 7

      /// <summary>
      ///   Extract the 1's digit of a number that is &gt;= 0.
      /// </summary>
      /// <returns>
      ///   The 1's digit of the <paramref name="num"/>.
      /// </returns>
      /// <remarks>
      ///   Undefined for values &lt; 0.
      /// </remarks>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Int32 OnesDigit() => num % 10;

      /// <summary>
      ///   Given a number between 0 and 99, sum the individual digits (i.e. the
      ///   10's digit and the 1's digit).
      /// </summary>
      /// <returns>
      ///   The sum of a number's 10's digit and its 1's digit.
      /// </returns>
      /// <remarks>
      ///   Undefined for values &lt; 0 or &gt; 99.
      /// </remarks>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Int32 SumIndividualDigits()
         => (num / 10) + (num % 10);

      /// <summary>
      ///   Extract the 10's digit of a number that is between 0 and 99.
      /// </summary>
      /// <returns>
      ///   The 10's digit of the <paramref name="num"/>.
      /// </returns>
      /// <remarks>
      ///   Undefined for values &lt; 0 or &gt; 99.
      /// </remarks>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Int32 TensDigit() => num / 10;

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
