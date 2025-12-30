namespace CheckDigits.Net;

/// <summary>
///   Public contract for a mask that identifies characters in a string value 
///   that should be included or excluded from check digit calculations.
/// </summary>
public interface ICheckDigitMask
{
   /// <summary>
   ///   Determines whether the character at the specified index should be 
   ///   included in check digit calculations.
   /// </summary>
   /// <param name="index">
   ///   The zero-based index of the character to evaluate.
   ///   </param>
   /// <returns>
   ///   <see langword="true"/> if the character at the specified index should
   ///   be excluded from check digit calculations; otherwise, 
   ///   <see langword="false"/>.
   /// </returns>
   /// <remarks>
   ///   Note that the determination to include or exclude a character is based
   ///   only on the index of the character in the string value, and not on the
   ///   value of the character at that index.
   /// </remarks>
   Boolean ExcludeCharacter(Int32 index);

   /// <summary>
   ///   Determines whether the character at the specified index should be 
   ///   included in check digit calculations.
   /// </summary>
   /// <param name="index">
   ///   The zero-based index of the character to evaluate.
   ///   </param>
   /// <returns>
   ///   <see langword="true"/> if the character at the specified index should
   ///   be included in check digit calculations; otherwise, 
   ///   <see langword="false"/>.
   /// </returns>
   /// <remarks>
   ///   Note that the determination to include or exclude a character is based
   ///   only on the index of the character in the string value, and not on the
   ///   value of the character at that index.
   /// </remarks>
   Boolean IncludeCharacter(Int32 index);
}
