namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class DigitsAlphabet : IAlphabet
{
   public Int32 CharacterToInteger(Char ch)
   {
      var num = ch.ToIntegerDigit();
      return num >= 0 && num <= 9 ? num : -1;
   }

   public Int32 CheckCharacterToInteger(Char ch) => throw new NotImplementedException();

   public Char IntegerToCheckCharacter(Int32 checkDigit) => checkDigit.ToDigitChar();
}
