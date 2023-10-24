namespace CheckDigits.Net.Tests.Unit.Iso7064;

public class AlphanumericSupplementalAlphabet : ISupplementalCharacterAlphabet
{
   private static readonly Int32[] _lookupTable =
      Enumerable.Range(CharConstants.Asterisk, CharConstants.UpperCaseZ - CharConstants.Asterisk + 1)
         .Select(x => x switch
         {
            CharConstants.Asterisk => 36,
            Int32 d when d >= CharConstants.DigitZero && d <= CharConstants.DigitNine => d - CharConstants.DigitZero,
            Int32 c when c >= CharConstants.UpperCaseA && c <= CharConstants.UpperCaseZ => c - CharConstants.UpperCaseA + 10,
            _ => -1
         }).ToArray();
   private const Int32 _asteriskOffset = 0;
   private const Int32 _digitLowerBound = CharConstants.DigitZero - CharConstants.Asterisk;
   private const Int32 _digitUpperBound = CharConstants.DigitNine - CharConstants.Asterisk;
   private const Int32 _alphaLowerBound = CharConstants.UpperCaseA - CharConstants.Asterisk;
   private const Int32 _alphaUpperBound = CharConstants.UpperCaseZ - CharConstants.Asterisk;
   private const String _checkCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*";

   public Int32 CharacterToInteger(Char ch)
   {
      var offset = ch - CharConstants.Asterisk;
      return (offset >= _digitLowerBound && offset <= _digitUpperBound)
            || (offset >= _alphaLowerBound && offset <= _alphaUpperBound)
            ? _lookupTable[offset]
            : -1;
   }

   public Int32 CheckCharacterToInteger(Char ch)
   {
      var offset = ch - CharConstants.Asterisk;
      return offset == _asteriskOffset
            || (offset >= _digitLowerBound && offset <= _digitUpperBound)
            || (offset >= _alphaLowerBound && offset <= _alphaUpperBound)
            ? _lookupTable[offset]
            : -1;
   }

   public Char IntegerToCheckCharacter(Int32 checkDigit) => _checkCharacters[checkDigit];
}
