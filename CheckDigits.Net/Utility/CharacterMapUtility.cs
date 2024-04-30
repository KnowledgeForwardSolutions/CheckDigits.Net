// Ignore Spelling: Figi

namespace CheckDigits.Net.Utility;

public static class CharacterMapUtility
{
   private const Int32 _upperCaseLetterOffset = 55;   // Value needed to subtract from an ASCII uppercase letter to transform A-Z to 10-35
   private const String _upperCaseConsonants = "BCDFGHJKLMNPQRSTVWXYZ";

   public static Int32[] GetFigiCharacterMap() => GetCharacterMap(
      CharConstants.DigitZero,
      CharConstants.UpperCaseZ,
      FigiMapper);

   private static IEnumerable<Char> CharacterRange(Char min, Char max) =>
      Enumerable.Range(min, max - min + 1)
         .Select(x => (Char)x);

   private static Int32[] GetCharacterMap(Char min, Char max, Func<Char, Int32> mapper) =>
      CharacterRange(min, max)
         .Select(x => mapper(x))
         .ToArray();

   private static Int32 FigiMapper(Char ch) => ch switch
   {
      var d when ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine => d.ToIntegerDigit(),
      var c when _upperCaseConsonants.Contains(c) => c - _upperCaseLetterOffset,
      _ => -1
   };
}
