namespace CheckDigits.Net.Support;

/// <summary>
///   Character domain used numeric modulus 11 check digit algorithms such as
///   ISBN-10 and ISO/IEC 7064 MOD 11-2. Valid characters are '0' - '9' and the
///   check characters are '0' - '9' and a supplemental 'X' character used to
///   represent a check digit of 10.
/// </summary>
public class DigitsSupplementaryDomain : ICharacterDomain
{
   private const String _checkCharacters = "0123456789X";
   private const String _validCharacters = "0123456789";

   /// <inheritdoc/>
   public String CheckCharacters => _checkCharacters;

   /// <inheritdoc/>
   public String ValidCharacters => _validCharacters;

   /// <inheritdoc/>
   public Char GetCheckCharacter(Int32 checkDigit)
      => checkDigit >= 0 && checkDigit <= 10
         ? _checkCharacters[checkDigit]
         : throw new ArgumentOutOfRangeException(nameof(checkDigit), checkDigit, Resources.GetCheckCharacterValueOutOfRangeMessage);

   /// <inheritdoc/>
   public Boolean TryGetValue(Char ch, out Int32 value)
   {
      value = ch.ToIntegerDigit();
      if (value >= 0 && value <= 9)
      {
         return true;
      }

      value = -1;
      return false;
   }
}
