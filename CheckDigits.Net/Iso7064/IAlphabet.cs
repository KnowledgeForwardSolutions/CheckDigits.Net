namespace CheckDigits.Net.Iso7064;

/// <summary>
///   Defines the alphabet used by a generic ISO/IEC 7064 algorithm.
/// </summary>
public interface IAlphabet
{
   /// <summary>
   ///   Maps a valid non-check digit character to its integer equivalent.
   /// </summary>
   /// <param name="ch">
   ///   The character to convert.
   /// </param>
   /// <returns>
   ///   The integer equivalent of <paramref name="ch"/> or -1 if 
   ///   <paramref name="ch"/> is not a valid character.
   /// </returns>
   Int32 CharacterToInteger(Char ch);

   /// <summary>
   ///   Maps a valid check digit character to its integer equivalent.
   /// </summary>
   /// <param name="ch">
   ///   The character to convert.
   /// </param>
   /// <returns>
   ///   The integer equivalent of <paramref name="ch"/> or -1 if 
   ///   <paramref name="ch"/> is not a valid character.
   /// </returns>
   /// <remarks>
   ///   This method is only used by algorithms that require supplementary check
   ///   characters, i.e. pure system algorithms that use a single check 
   ///   character.
   /// </remarks>
   Int32 CheckCharacterToInteger(Char ch);

   /// <summary>
   ///   Maps the check digit calculated by an algorithm to its character 
   ///   representation.
   /// </summary>
   /// <param name="checkDigit">
   ///   The value to convert.
   /// </param>
   /// <returns>
   ///   The character representation of the <paramref name="checkDigit"/> value.
   /// </returns>
   Char IntegerToCheckCharacter(Int32 checkDigit);
}
