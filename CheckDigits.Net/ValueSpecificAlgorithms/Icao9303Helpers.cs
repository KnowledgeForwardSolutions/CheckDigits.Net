// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Shared helper methods and data for ICAO 9303 check digit algorithms.
/// </summary>
public static class Icao9303Helpers
{
   /// <summary>
   ///   Represents the maximum allowable integer value when converting a 
   ///   character in an ICAO 9303 alphanumeric field to its integer equivalent.
   /// </summary>
   public const Int32 AlphanumericUpperBound = 35;

   /// <summary>
   ///   Represents the maximum allowable integer value when converting a 
   ///   character in an ICAO 9303 numeric field to its integer equivalent.
   /// </summary>
   public const Int32 NumericUpperBound = 9;

   /// <summary>
   ///   Map a character to its integer equivalent in the 
   ///   <see cref="Icao9303Algorithm"/>. Characters that are not valid for the 
   ///   Icao9303Algorithm are mapped to -1.
   /// </summary>
   /// <param name="ch">
   ///   The character to map.
   /// </param>
   /// <returns>
   ///   The integer value associated with <paramref name="ch"/>.
   /// </returns>
   public static Int32 MapCharacter(Char ch) => ch switch
   {
      var d when ch >= Chars.DigitZero && ch <= Chars.DigitNine => d.ToIntegerDigit(),
      var c when ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ => c - Chars.UpperCaseA + 10,
      Chars.LeftAngleBracket => 0,
      _ => -1
   };

   /// <summary>
   ///   Pre-computed character-to-integer mapping for ICAO 9303 character set.
   /// </summary>
   internal static readonly Int32[] CharMap = 
      [.. Chars.Range(Chars.DigitZero, Chars.UpperCaseZ).Select(MapCharacter)];

   /// <summary>
   ///   Converts an ICAO 9303 character to its integer equivalent.
   /// </summary>
   /// <param name="ch">
   ///   The character to convert.
   /// </param>
   /// <returns>
   ///   The integer value (0-35) if the character is valid (0-9, A-Z, or '<'), 
   ///   otherwise -1.
   /// </returns>
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   internal static Int32 ToIcao9303IntegerValue(Char ch)
      => (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
         ? CharMap[ch - Chars.DigitZero]
         : -1;

   /// <summary>
   ///   Determines whether an integer value is invalid for a field based on 
   ///   the field's upper bound constraint.
   /// </summary>
   /// <param name="value">
   ///   The integer value to validate.
   /// </param>
   /// <param name="numUpperBound">
   ///   The upper bound for valid values (9 for numeric fields, 35 for alphanumeric).
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if the value is invalid (less than 0 or greater 
   ///   than the upper bound); otherwise, <see langword="false"/>.
   /// </returns>
   [MethodImpl(MethodImplOptions.AggressiveInlining)]
   internal static Boolean IsInvalidValueForField(Int32 value, Int32 numUpperBound)
      => value < 0 || value > numUpperBound;
}