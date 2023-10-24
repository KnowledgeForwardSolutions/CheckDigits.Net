namespace CheckDigits.Net.Tests.Unit.TestData;

public class DanishAlphabet : IAlphabet
{
    // Additional characters:
    // diphthong AE (\u00C6) has value 26
    // slashed O (\u00D8) has value 27
    // A with diaeresis (\u00C4) has value 28
    private const string _validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ\u00C6\u00D8\u00C4";

    public int CharacterToInteger(char ch)
       => ch switch
       {
           var x when x >= 'A' && x <= 'Z' => x - 'A',
           '\u00C6' => 26,
           '\u00D8' => 27,
           '\u00C4' => 28,
           _ => -1
       };

    public char IntegerToCheckCharacter(int checkDigit) => _validCharacters[checkDigit];
}
