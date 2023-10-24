namespace CheckDigits.Net.Tests.Unit.TestData;

public class AlphanumericAlphabet : IAlphabet
{
    private static readonly int[] _lookupTable =
       Enumerable.Range(CharConstants.DigitZero, CharConstants.UpperCaseZ - CharConstants.DigitZero + 1)
          .Select(x => x switch
          {
              int d when d >= CharConstants.DigitZero && d <= CharConstants.DigitNine => d - CharConstants.DigitZero,
              int c when c >= CharConstants.UpperCaseA && c <= CharConstants.UpperCaseZ => c - CharConstants.UpperCaseA + 10,
              _ => -1
          }).ToArray();
    private const int _digitLowerBound = 0;
    private const int _digitUpperBound = 9;
    private const int _alphaLowerBound = CharConstants.UpperCaseA - CharConstants.DigitZero;
    private const int _alphaUpperBound = CharConstants.UpperCaseZ - CharConstants.DigitZero;
    private const string _checkCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public int CharacterToInteger(char ch)
    {
        var offset = ch - CharConstants.DigitZero;
        return offset >= _digitLowerBound && offset <= _digitUpperBound
              || offset >= _alphaLowerBound && offset <= _alphaUpperBound
              ? _lookupTable[offset]
              : -1;
    }

    public char IntegerToCheckCharacter(int checkDigit) => _checkCharacters[checkDigit];
}
