namespace CheckDigits.Net.Iso7064;

/// <summary>
///   The public contract for a check digit algorithm based on the ISO/IEC 7064
///   standard.
/// </summary>
public interface IIso7064Algorithm
{
   /// <summary>
   ///   The domain of characters that the algorithm operates on. Specifies both
   ///   the valid characters for the algorithm and the possible check 
   ///   characters produced by the algorithm.
   /// </summary>
   ICharacterDomain CharacterDomain { get; }

   /// <summary>
   ///   The number of check characters used by the algorithm.
   /// </summary>
   Int32 CheckCharacterCount { get; }

   /// <summary>
   ///   The value used by the algorithm modulus operation.
   /// </summary>
   Int32 Modulus { get; }

   /// <summary>
   ///   The base of the geometric progression used by the algorithm.
   /// </summary>
   Int32 Radix { get; }
}
