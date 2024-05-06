namespace CheckDigits.Net.Tests.Unit.TestData;

public class LettersAlphabet : IAlphabet
{
    public Int32 CharacterToInteger(Char ch)
    {
        var num = ch - Chars.UpperCaseA;
        return num >= 0 && num <= 25 ? num : -1;
    }

    public Char IntegerToCheckCharacter(Int32 checkDigit) => (Char)(checkDigit + Chars.UpperCaseA);
}
