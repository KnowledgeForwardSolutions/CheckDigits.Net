namespace CheckDigits.Net.Tests.Unit.TestData;

public class AlphanumericSupplementalAlphabet : ISupplementalCharacterAlphabet
{
    private static readonly int[] _lookupTable =
       Enumerable.Range(CharConstants.Asterisk, CharConstants.UpperCaseZ - CharConstants.Asterisk + 1)
          .Select(x => x switch
          {
              CharConstants.Asterisk => 36,
              int d when d >= CharConstants.DigitZero && d <= CharConstants.DigitNine => d - CharConstants.DigitZero,
              int c when c >= CharConstants.UpperCaseA && c <= CharConstants.UpperCaseZ => c - CharConstants.UpperCaseA + 10,
              _ => -1
          }).ToArray();
    private const int _asteriskOffset = 0;
    private const int _digitLowerBound = CharConstants.DigitZero - CharConstants.Asterisk;
    private const int _digitUpperBound = CharConstants.DigitNine - CharConstants.Asterisk;
    private const int _alphaLowerBound = CharConstants.UpperCaseA - CharConstants.Asterisk;
    private const int _alphaUpperBound = CharConstants.UpperCaseZ - CharConstants.Asterisk;
    private const string _checkCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*";

    public int CharacterToInteger(char ch)
    {
        var offset = ch - CharConstants.Asterisk;
        return offset >= _digitLowerBound && offset <= _digitUpperBound
              || offset >= _alphaLowerBound && offset <= _alphaUpperBound
              ? _lookupTable[offset]
              : -1;
    }

    public int CheckCharacterToInteger(char ch)
    {
        var offset = ch - CharConstants.Asterisk;
        return offset == _asteriskOffset
              || offset >= _digitLowerBound && offset <= _digitUpperBound
              || offset >= _alphaLowerBound && offset <= _alphaUpperBound
              ? _lookupTable[offset]
              : -1;
    }

    public char IntegerToCheckCharacter(int checkDigit) => _checkCharacters[checkDigit];
}
