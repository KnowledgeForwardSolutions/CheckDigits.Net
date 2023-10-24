namespace CheckDigits.Net.Iso7064;

/// <summary>
///   Defines the alphabet used by a generic ISO/IEC 7064 algorithm that requires
///   a supplemental check character.
/// </summary>
/// <remarks>
///   A supplemental check character is an extra character that can only appear
///   in the check character position of a value. Supplemental check characters
///   are used ISO/IEC 7064 pure system algorithms that generate a single check
///   character. Such algorithms are implemented by
///   <see cref="Iso7064PureSystemSingleCharacterAlgorithm"/>.
/// </remarks>
public interface ISupplementalCharacterAlphabet : IAlphabet
{
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
   Int32 CheckCharacterToInteger(Char ch);
}
