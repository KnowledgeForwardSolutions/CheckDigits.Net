// Ignore Spelling: Cusip Figi Icao

namespace CheckDigits.Net.Utility;

public static class CharacterMapUtility
{
   private const Int32 _upperCaseLetterOffset = 55;   // Value needed to subtract from an ASCII uppercase letter to transform A-Z to 10-35
   private const String _upperCaseConsonants = "BCDFGHJKLMNPQRSTVWXYZ";

   /// <summary>
   ///   Get an array that maps the characters '0'-'9' and 'A'-'Z' to the 
   ///   integer values 0-9 and 10-35.
   /// </summary>
   public static Int32[] GetAlphanumericCharacterMap() => GetCharacterMap(
      CharConstants.DigitZero,
      CharConstants.UpperCaseZ,
      AlphanumericMapper);

   /// <summary>
   ///   Get an array that maps character values to the integer equivalent used
   ///   by the <see cref="FigiAlgorithm"/>.
   /// </summary>
   public static Int32[] GetFigiCharacterMap() => GetCharacterMap(
      CharConstants.DigitZero,
      CharConstants.UpperCaseZ,
      BetanumericMapper);

   public static Int32[] GetIcao9303CharacterMap() => GetCharacterMap(
      CharConstants.DigitZero,
      CharConstants.UpperCaseZ,
      Icao9303Mapper);

   /// <summary>
   ///   Get an array that maps character values to the integer equivalent used
   ///   by the <see cref="VinAlgorithm"/>.
   /// </summary>
   public static Int32[] GetVinCharacterMap() => GetCharacterMap(
      CharConstants.DigitZero,
      CharConstants.UpperCaseZ,
      VinMapper);

   private static IEnumerable<Char> CharacterRange(Char min, Char max) =>
      Enumerable.Range(min, max - min + 1)
         .Select(x => (Char)x);

   private static Int32[] GetCharacterMap(Char min, Char max, Func<Char, Int32> mapper) =>
      CharacterRange(min, max)
         .Select(x => mapper(x))
         .ToArray();

   private static Int32 AlphanumericMapper(Char ch) => ch switch
   {
      var d when ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine => d.ToIntegerDigit(),
      var c when ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ => c - _upperCaseLetterOffset,
      _ => -1
   };

   private static Int32 BetanumericMapper(Char ch) => ch switch
   {
      var d when ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine => d.ToIntegerDigit(),
      var c when _upperCaseConsonants.Contains(c) => c - _upperCaseLetterOffset,
      _ => -1
   };

   private static Int32 Icao9303Mapper(Char ch) => ch switch
   {
      var d when ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine => d.ToIntegerDigit(),
      var c when ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseZ => c - _upperCaseLetterOffset,
      CharConstants.LeftAngleBracket => 0,
      _ => -1
   };

   private static Int32 VinMapper(Char ch) => ch switch
   {
      var d when ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine => d.ToIntegerDigit(),
      var a when ch >= CharConstants.UpperCaseA && ch <= CharConstants.UpperCaseH => a - CharConstants.UpperCaseA + 1,
      var j when ch >= CharConstants.UpperCaseJ && ch <= CharConstants.UpperCaseN => j - CharConstants.UpperCaseJ + 1,
      CharConstants.UpperCaseP => 7,
      CharConstants.UpperCaseR => 9,
      var s when ch >= CharConstants.UpperCaseS && ch <= CharConstants.UpperCaseZ => s - CharConstants.UpperCaseS + 2,
      _ => -1
   };
}
