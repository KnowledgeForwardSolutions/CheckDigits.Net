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
      ///   Determines if the character is an ASCII uppercase consonant letter
      ///   (i.e. not A, E, I, O or U).
      /// </summary>
      /// <returns>
      ///   <see langword="true"/> if the character is an ASCII uppercase 
      ///   consonant letter;  otherwise, <see langword="false"/>.
      /// </returns>
      public Boolean IsAsciiConsonantUpper()
         => ch.IsAsciiLetterUpper() 
            && ch != Chars.UpperCaseA
            && ch != Chars.UpperCaseE
            && ch != Chars.UpperCaseI
            && ch != Chars.UpperCaseO
            && ch != Chars.UpperCaseU;

      /// <summary>
      ///   Determines if the character is an ASCII extended decimal digit (i.e.
      ///   a decimal digit, '0'-'9', or an uppercase 'X' which represents 10 in
      ///   modulus 11 check digit algorithms).
      /// </summary>
      /// <returns>
      ///   <see langword="true"/> if the character is in the range '0'-'9' or 
      ///   is 'X'; otherwise, <see langword="false"/>.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Boolean IsAsciiExtendedDecimalDigit()
         => ch.IsAsciiDigit() || ch == Chars.UpperCaseX;

      /// <summary>
      ///   Determines whether the character is an ASCII digit.
      /// </summary>
      /// <returns>
      ///   <see langword="true"/> if the character is in the range '0'-'9'; 
      ///   otherwise, <see langword="false"/>.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Boolean IsAsciiDigit()
#if NET7_0_OR_GREATER
         => Char.IsAsciiDigit(ch);
         #else
         => ch is >= Chars.DigitZero and <= Chars.DigitNine;
      #endif

      /// <summary>
      ///   Determines whether the character is an uppercase OR lowercase ASCII
      ///   letter.
      /// </summary>
      /// <returns>
      ///   <see langword="true"/> if the character is in the range 'A'-'Z' or
      ///   'a-z'; otherwise, <see langword="false"/>.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Boolean IsAsciiLetter()
#if NET7_0_OR_GREATER
         => Char.IsAsciiLetter(ch);
#else
         => ch is (>= Chars.UpperCaseA and <= Chars.UpperCaseZ) or (>= Chars.LowerCaseA and <= Chars.LowerCaseZ);
#endif

      /// <summary>
      ///   Determines whether the character is an uppercase ASCII letter.
      /// </summary>
      /// <returns>
      ///   <see langword="true"/> if the character is in the range 'A'-'Z'; 
      ///   otherwise, <see langword="false"/>.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Boolean IsAsciiLetterUpper()
#if NET7_0_OR_GREATER
         => Char.IsAsciiLetterUpper(ch);
#else
         => ch is >= Chars.UpperCaseA and <= Chars.UpperCaseZ;
#endif

      /// <summary>
      ///   Determines whether the character is an ASCII digit or an uppercase
      ///   ASCII letter.
      /// </summary>
      /// <returns>
      ///   <see langword="true"/> if the character is in the range '0'-'9' or
      ///   'A'-'Z'; otherwise, <see langword="false"/>.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Boolean IsAsciiUppercaseAlphanumeric()
         => ch.IsAsciiDigit() || ch.IsAsciiLetterUpper();

      /// <summary>
      ///   Maps an uppercase Latin letter to its zero-based alphabetical index.
      /// </summary>
      /// <remarks>
      ///   For characters outside the uppercase Latin range, the returned value 
      ///   can be outside 0 through 25.
      /// </remarks>
      /// <returns>
      ///   The integer value of the supplied character, where 'A'-'Z' = 0-25.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Int32 MapAsciiUppercaseAlpha() => ch - Chars.UpperCaseA;

      /// <summary>
      ///   Maps an ASCII digit or uppercase ASCII letter to its base-36 numeric 
      ///   value.
      /// </summary>
      /// <remarks>
      ///   Only ASCII uppercase letters are supported; lowercase and 
      ///   non-alphanumeric characters return -1.
      /// </remarks>
      /// <returns>
      ///   A value from 0 to 35 for ASCII digits ('0'-'9') and  uppercase 
      ///   letters ('A'-'Z'); otherwise, -1.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Int32 MapAsciiUppercaseAlphanumeric()
         => ch switch
         {
            _ when ch.IsAsciiDigit() => ch.MapAsciiDigit(),
            _ when ch.IsAsciiLetterUpper() => 10 + (ch - Chars.UpperCaseA),
            _ => -1
         };

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
      public Int32 MapAsciiDigit() => ch - Chars.DigitZero;

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
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Int32 MapAsciiExtendedDecimalDigit()
         => ch == Chars.UpperCaseX ? 10 : ch - Chars.DigitZero;

      /// <summary>
      ///   Maps characters from the ICAO 9303 characterset 
      ///   ('0'-'9', 'A'-Z', '&lt;') to their integer equivalents 
      ///   (0-9, 10-35, 0). Invalid characters are mapped to -1.
      /// </summary>
      /// <returns>
      ///   The ICAO 9303 integer equivalent of the supplied 
      ///   <paramref name="ch"/> ('0'-'9' -> 0-9, 'A'-Z' -> 10-35, '&lt;' -> 0)
      ///   or -1 for invalid characters.
      /// </returns>
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      public Int32 MapIcao9303Character()
         => ch switch
         {
            _ when ch.IsAsciiDigit() => ch.MapAsciiDigit(),
            _ when ch.IsAsciiLetterUpper() => 10 + (ch - Chars.UpperCaseA),
            Chars.LeftAngleBracket => 0,
            _ => -1
         };
   }
}
