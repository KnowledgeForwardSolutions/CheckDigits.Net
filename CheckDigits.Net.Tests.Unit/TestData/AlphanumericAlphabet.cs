namespace CheckDigits.Net.Tests.Unit.TestData;

public class AlphanumericAlphabet : IAlphabet
{
    private static readonly Int32[] _lookupTable =
       Enumerable.Range(CharConstants.DigitZero, CharConstants.UpperCaseZ - CharConstants.DigitZero + 1)
          .Select(x => x switch
          {
              Int32 d when d >= CharConstants.DigitZero && d <= CharConstants.DigitNine => d - CharConstants.DigitZero,
              Int32 c when c >= CharConstants.UpperCaseA && c <= CharConstants.UpperCaseZ => c - CharConstants.UpperCaseA + 10,
              _ => -1
          }).ToArray();
    private const Int32 _digitLowerBound = 0;
    private const Int32 _digitUpperBound = 9;
    private const Int32 _alphaLowerBound = CharConstants.UpperCaseA - CharConstants.DigitZero;
    private const Int32 _alphaUpperBound = CharConstants.UpperCaseZ - CharConstants.DigitZero;
    private const String _checkCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public Int32 CharacterToInteger(Char ch)
    {
        var offset = ch - CharConstants.DigitZero;
        return (offset >= _digitLowerBound && offset <= _digitUpperBound)
              || (offset >= _alphaLowerBound && offset <= _alphaUpperBound)
              ? _lookupTable[offset]
              : -1;
    }

    public Char IntegerToCheckCharacter(Int32 checkDigit) => _checkCharacters[checkDigit];
}
