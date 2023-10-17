namespace CheckDigits.Net.Support;

/// <summary>
///   Character domain used ISO/ICE 7064 MOD 37-2 check digit algorithm. Valid 
///   characters are '0' - '9', 'A' - 'Z' and the check characters are '0' - '9',
///   'A' - 'Z' and a supplemental '*' character.
/// </summary>
public class AlphanumericSupplementaryDomain : ICharacterDomain
{
   private const String _checkCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ*";
   private const String _validCharacters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

   private static readonly Int32[] _lookupTable = 
      Enumerable.Range(CharConstants.Asterisk, CharConstants.UpperCaseZ - CharConstants.Asterisk + 1)
         .Select(x => x switch
         {
            CharConstants.Asterisk => 36,
            Int32 d when d >= CharConstants.DigitZero && d <= CharConstants.DigitNine => d - CharConstants.DigitZero,
            Int32 c when c >= CharConstants.UpperCaseA && c <= CharConstants.UpperCaseZ => c - CharConstants.UpperCaseA + 10,
            _ => -1
         }).ToArray();

   /// <inheritdoc/>
   public String CheckCharacters => _checkCharacters;

   /// <inheritdoc/>
   public String ValidCharacters => _validCharacters;

   /// <inheritdoc/>
   public Char GetCheckCharacter(Int32 checkDigit)
      => checkDigit >= 0 && checkDigit <= 36
         ? _checkCharacters[checkDigit]
         : throw new ArgumentOutOfRangeException(nameof(checkDigit), checkDigit, Resources.GetCheckCharacterValueOutOfRangeMessage);

   /// <inheritdoc/>
   public Int32 MapCharacterToNumber(Char ch)
   {
      var value = ch - CharConstants.Asterisk;
      return value < 1 || value > 48 ? -1 : _lookupTable[value];  // Note difference with MapCheckCharacterToNumber - this excludes initial asterisk character
   }

   /// <inheritdoc/>
   public Int32 MapCheckCharacterToNumber(Char ch)
   {
      var value = ch - CharConstants.Asterisk;
      return value < 0 || value > 48 ? -1 : _lookupTable[value];
   }
}
