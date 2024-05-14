namespace CheckDigits.Net.Tests.Unit.TestData;

public class AlphanumericSupplementalAlphabet : ISupplementalCharacterAlphabet
{
    private static readonly Int32[] _lookupTable =
       Enumerable.Range(Chars.Asterisk, Chars.UpperCaseZ - Chars.Asterisk + 1)
          .Select(x => x switch
          {
              Chars.Asterisk => 36,
              Int32 d when d >= Chars.DigitZero && d <= Chars.DigitNine => d - Chars.DigitZero,
              Int32 c when c >= Chars.UpperCaseA && c <= Chars.UpperCaseZ => c - Chars.UpperCaseA + 10,
              _ => -1
          }).ToArray();
    private const Int32 _asteriskOffset = 0;
    private const Int32 _digitLowerBound = Chars.DigitZero - Chars.Asterisk;
    private const Int32 _digitUpperBound = Chars.DigitNine - Chars.Asterisk;
    private const Int32 _alphaLowerBound = Chars.UpperCaseA - Chars.Asterisk;
    private const Int32 _alphaUpperBound = Chars.UpperCaseZ - Chars.Asterisk;
    private const String _checkCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*";

    public Int32 CharacterToInteger(Char ch)
    {
        var offset = ch - Chars.Asterisk;
        return (offset >= _digitLowerBound && offset <= _digitUpperBound)
              || (offset >= _alphaLowerBound && offset <= _alphaUpperBound)
              ? _lookupTable[offset]
              : -1;
    }

    public Int32 CheckCharacterToInteger(Char ch)
    {
        var offset = ch - Chars.Asterisk;
        return offset == _asteriskOffset
              || (offset >= _digitLowerBound && offset <= _digitUpperBound)
              || (offset >= _alphaLowerBound && offset <= _alphaUpperBound)
              ? _lookupTable[offset]
              : -1;
    }

    public Char IntegerToCheckCharacter(Int32 checkDigit) => _checkCharacters[checkDigit];
}
