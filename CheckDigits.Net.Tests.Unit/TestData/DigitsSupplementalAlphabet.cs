namespace CheckDigits.Net.Tests.Unit.TestData;

public class DigitsSupplementalAlphabet : ISupplementalCharacterAlphabet
{
    private const string _checkCharacters = "0123456789X";

    public int CharacterToInteger(char ch)
    {
        var num = ch.ToIntegerDigit();
        return num >= 0 && num <= 9 ? num : -1;
    }

    public int CheckCharacterToInteger(char ch)
    {
        var num = ch.ToIntegerDigit();
        return num >= 0 && num <= 9
           ? num
           : ch == CharConstants.UpperCaseX ? 10 : -1;
    }

    public char IntegerToCheckCharacter(int checkDigit) => _checkCharacters[checkDigit];
}
