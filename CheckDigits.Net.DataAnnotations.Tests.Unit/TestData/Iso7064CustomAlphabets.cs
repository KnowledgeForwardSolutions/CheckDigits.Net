namespace CheckDigits.Net.DataAnnotations.Tests.Unit.TestData;

public class DanishAlphabet : IAlphabet
{
   // Additional characters:
   // diphthong AE (\u00C6) has value 26
   // slashed O (\u00D8) has value 27
   // A with diaeresis (\u00C4) has value 28
   private const String _validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ\u00C6\u00D8\u00C4";

   public Int32 CharacterToInteger(Char ch)
      => ch switch
      {
         var x when x >= 'A' && x <= 'Z' => x - 'A',
         '\u00C6' => 26,
         '\u00D8' => 27,
         '\u00C4' => 28,
         _ => -1
      };

   public Char IntegerToCheckCharacter(Int32 checkDigit) => _validCharacters[checkDigit];
}

public class DigitsSupplementalAlphabet : ISupplementalCharacterAlphabet
{
   private const String _checkCharacters = "0123456789X";

   public Int32 CharacterToInteger(Char ch)
   {
      var num = ch - '0';
      return num >= 0 && num <= 9 ? num : -1;
   }

   public Int32 CheckCharacterToInteger(Char ch)
   {
      var num = ch - '0';
      return num >= 0 && num <= 9
         ? num
         : ch == 'X' ? 10 : -1;
   }

   public Char IntegerToCheckCharacter(Int32 checkDigit) => _checkCharacters[checkDigit];
}

public class LettersAlphabet : IAlphabet
{
   public Int32 CharacterToInteger(Char ch)
   {
      var num = ch - 'A';
      return num >= 0 && num <= 25 ? num : -1;
   }

   public Char IntegerToCheckCharacter(Int32 checkDigit) => (Char)(checkDigit + 'A');
}
