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
   ///   Get the value assigned to the <paramref name="ch"/> by the check digit
   ///   algorithm.
   /// </summary>
   /// <param name="ch">
   ///   The character to convert.
   /// </param>
   /// <param name="value">
   ///   Output. The value assigned to the <paramref name="ch"/> by the check 
   ///   digit algorithm.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if <paramref name="ch"/> is a valid character 
   ///   for the check digit algorithm; otherwise <see langword="false"/>.
   /// </returns>
   Boolean TryGetValue(Char ch, out Int32 value);
}
