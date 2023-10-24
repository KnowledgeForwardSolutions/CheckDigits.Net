namespace CheckDigits.Net.Tests.Unit.TestData;

public class LettersAlphabet : IAlphabet
{
    public int CharacterToInteger(char ch)
    {
        var num = ch - CharConstants.UpperCaseA;
        return num >= 0 && num <= 25 ? num : -1;
    }

    public char IntegerToCheckCharacter(int checkDigit) => (char)(checkDigit + CharConstants.UpperCaseA);
}
