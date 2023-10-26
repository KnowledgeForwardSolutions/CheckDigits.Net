namespace CheckDigits.Net.Tests.Unit.TestData;

public class DigitsAlphabet : IAlphabet
{
    public int CharacterToInteger(char ch)
    {
        var num = ch.ToIntegerDigit();
        return num >= 0 && num <= 9 ? num : -1;
    }


    public char IntegerToCheckCharacter(int checkDigit) => checkDigit.ToDigitChar();
}
