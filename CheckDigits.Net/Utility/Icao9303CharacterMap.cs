// Ignore Spelling: Icao

namespace CheckDigits.Net.Utility;

public static class Icao9303CharacterMap
{
   private const Int32 _letterOffset = 55;      // Value needed to subtract from an ASCII uppercase letter to transform A-Z to 10-35
   private const String _chars = "0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ";
   
   public static Int32[] GetCharacterMap()
      => _chars.Select(x => x switch
      {
         var d when x >= CharConstants.DigitZero && x <= CharConstants.DigitNine => d.ToIntegerDigit(),
         var c when x >= CharConstants.UpperCaseA && x <= CharConstants.UpperCaseZ => c - _letterOffset,
         CharConstants.LeftAngleBracket => 0,
         _ => -1
      })
      .ToArray();
}
