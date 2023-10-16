namespace CheckDigits.Net.Support;

/// <summary>
///   Defines the set of characters allowed/used by a check digit algorithm.
/// </summary>
public interface ICharacterDomain
{
   /// <summary>
   ///   A string containing all of the characters that may be used as a check
   ///   character by the check digit algorithm.
   /// </summary>
   /// <remarks>
   ///   <see cref="CheckCharacters"/> is generally the same as 
   ///   <see cref="ValidCharacters"/>, but that is not always the case. Some 
   ///   algorithms support a supplemental check character, one that does not
   ///   appear in the set valid characters (for example, ISBN-10 or 
   ///   ISO/IEC 7064 MOD 37-2 algorithms).
   /// </remarks>
   String CheckCharacters { get; }

   /// <summary>
   ///   A string containing all of the characters that are valid as input to 
   ///   the check digit algorithm.
   /// </summary>
   String ValidCharacters { get; }

   /// <summary>
   ///   Gets the check character that corresponds to the suppled 
   ///   <paramref name="checkDigit"/>.
   /// </summary>
   /// <param name="checkDigit">
   ///   The check digit value to convert.
   /// </param>
   /// <returns>
   ///   The check character that corresponds to the suppled 
   ///   <paramref name="checkDigit"/>.
   /// </returns>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   <paramref name="checkDigit"/> does not map to a character in 
   ///   <see cref="CheckCharacters"/>.
   /// </exception>
   Char GetCheckCharacter(Int32 checkDigit);

   /// <summary>
   ///   Map a non-check character to its integer equivalent.
   /// </summary>
   /// <param name="ch">
   ///   The character to convert.
   /// </param>
   /// <returns>
   ///   An integer > -1 if <paramref name="ch"/> is a valid character; 
   ///   otherwise -1.
   /// </returns>
   Int32 MapCharacterToNumber(Char ch);

   /// <summary>
   ///   Map a check character to its integer equivalent.
   /// </summary>
   /// <param name="ch">
   ///   The character to convert.
   /// </param>
   /// <returns>
   ///   An integer > -1 if <paramref name="ch"/> is a valid check character; 
   ///   otherwise -1.
   /// </returns>
   /// <remarks>
   ///   Some algorithms support a supplemental check character (for example, 
   ///   ISBN-10 or ISO/IEC 7064 MOD 37-2 algorithms). For algorithms that do 
   ///   not have a supplemental check character this method is equivalent to
   ///   <see cref="MapCharacterToNumber(Char)"/>.
   /// </remarks>
   Int32 MapCheckCharacterToNumber(Char ch);
}
