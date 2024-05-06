// Ignore Spelling: Betanumeric

namespace CheckDigits.Net.Utility;

/// <summary>
///   Character constants and helper methods.
/// </summary>
public static class Chars
{
   public const Char NUL = '\0';

   public const Char HashMark = '#';
   public const Char Asterisk = '*';
   public const Char Dash = '-';
   public const Char AtSign = '@';
   public const Char LeftAngleBracket = '<';

   public const Char DigitZero = '0';
   public const Char DigitOne = '1';
   public const Char DigitTwo = '2';
   public const Char DigitThree = '3';
   public const Char DigitFour = '4';
   public const Char DigitFive = '5';
   public const Char DigitSix = '6';
   public const Char DigitSeven = '7';
   public const Char DigitEight = '8';
   public const Char DigitNine = '9';

   public const Char UpperCaseA = 'A';
   public const Char UpperCaseB = 'B';
   public const Char UpperCaseC = 'C';
   public const Char UpperCaseD = 'D';
   public const Char UpperCaseE = 'E';
   public const Char UpperCaseF = 'F';
   public const Char UpperCaseG = 'G';
   public const Char UpperCaseH = 'H';
   public const Char UpperCaseI = 'I';
   public const Char UpperCaseJ = 'J';
   public const Char UpperCaseK = 'K';
   public const Char UpperCaseL = 'L';
   public const Char UpperCaseM = 'M';
   public const Char UpperCaseN = 'N';
   public const Char UpperCaseO = 'O';
   public const Char UpperCaseP = 'P';
   public const Char UpperCaseQ = 'Q';
   public const Char UpperCaseR = 'R';
   public const Char UpperCaseS = 'S';
   public const Char UpperCaseT = 'T';
   public const Char UpperCaseU = 'U';
   public const Char UpperCaseV = 'V';
   public const Char UpperCaseW = 'W';
   public const Char UpperCaseX = 'X';
   public const Char UpperCaseY = 'Y';
   public const Char UpperCaseZ = 'Z';

   public const Char LowerCaseA = 'a';
   public const Char LowerCaseB = 'b';
   public const Char LowerCaseC = 'c';
   public const Char LowerCaseD = 'd';
   public const Char LowerCaseE = 'e';
   public const Char LowerCaseF = 'f';
   public const Char LowerCaseG = 'g';
   public const Char LowerCaseH = 'h';
   public const Char LowerCaseI = 'i';
   public const Char LowerCaseJ = 'j';
   public const Char LowerCaseK = 'k';
   public const Char LowerCaseL = 'l';
   public const Char LowerCaseM = 'm';
   public const Char LowerCaseN = 'n';
   public const Char LowerCaseO = 'o';
   public const Char LowerCaseP = 'p';
   public const Char LowerCaseQ = 'q';
   public const Char LowerCaseR = 'r';
   public const Char LowerCaseS = 's';
   public const Char LowerCaseT = 't';
   public const Char LowerCaseU = 'u';
   public const Char LowerCaseV = 'v';
   public const Char LowerCaseW = 'w';
   public const Char LowerCaseX = 'x';
   public const Char LowerCaseY = 'y';
   public const Char LowerCaseZ = 'z';

   private const String _upperCaseConsonants = "BCDFGHJKLMNPQRSTVWXYZ";

   /// <summary>
   ///   Map a character to an integer value. '0'-'9' map to their integer
   ///   equivalents. 'A'-'Z' map to 10-35. All other characters map to -1.
   /// </summary>
   /// <param name="ch">
   ///   The character to map.
   /// </param>
   /// <returns>
   ///   The integer value associated with <paramref name="ch"/>.
   /// </returns>
   public static Int32 MapAlphanumericCharacter(Char ch) => ch switch
   {
      var d when ch >= Chars.DigitZero && ch <= Chars.DigitNine => d.ToIntegerDigit(),
      var c when ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseZ => c - Chars.UpperCaseA + 10,
      _ => -1
   };

   /// <summary>
   ///   Map a character to an integer value. '0'-'9' map to their integer
   ///   equivalents. Vowels ('A', 'E', 'I', 'O' and 'U') map to -1 while 
   ///   consonants ('B'-'Z') map to 11-35. All other characters map to -1.
   /// </summary>
   /// <param name="ch">
   ///   The character to map.
   /// </param>
   /// <returns>
   ///   The integer value associated with <paramref name="ch"/>.
   /// </returns>
   public static Int32 MapBetanumericCharacter(Char ch) => ch switch
   {
      var d when ch >= Chars.DigitZero && ch <= Chars.DigitNine => d.ToIntegerDigit(),
      var c when _upperCaseConsonants.Contains(c) => c - Chars.UpperCaseA + 10,
      _ => -1
   };

   /// <summary>
   ///   Get an enumerable collection of characters, ranging from 
   ///   <paramref name="start"/> to <paramref name="end"/>, inclusive.
   /// </summary>
   /// <param name="start">
   ///   The first character in the range.
   /// </param>
   /// <param name="end">
   ///   The last character in the range.
   /// </param>
   /// <returns>
   ///   An enumerable collection of characters.
   /// </returns>
   /// <remarks>
   ///   If <paramref name="end"/> is less than <paramref name="start"/> then
   ///   the start and end values are normalized before generating the range of
   ///   characters.
   /// </remarks>
   public static IEnumerable<Char> Range(Char start, Char end)
   {
      if (end < start)
      {
         (end, start) = (start, end);
      }

      var ch = start;
      while(ch <= end)
      {
         yield return ch;
         ch++;
      }
   }
}
