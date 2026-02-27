// Ignore Spelling: Damm Quasigroup

namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Defines the contract for a Damm quasigroup, which provides methods for 
///   mapping characters, retrieving interim values, and calculating check 
///   digits based on the Damm algorithm.
/// </summary>
/// <remarks>
///   Implementations of this interface must ensure that all methods adhere to 
///   the constraints defined for the parameters, particularly regarding the 
///   valid ranges for interim values and input digits.
/// </remarks>
public interface IDammQuasigroup
{
   /// <summary>
   ///   The order of the quasigroup, which is the number of elements in the set.
   /// </summary>
   Int32 Order { get; }

   /// <summary>
   ///   Gets the next interim value from the quasigroup table based on the 
   ///   current interim value and the next input digit.
   /// </summary>
   /// <param name="interm">
   ///   The current interm value, used as the zero-based x-coordinate in the 
   ///   quasigroup table. Must be a non-negative integer less than <see cref="Order"/>.
   /// </param>
   /// <param name="next">
   ///   The next input digit, used as the zero-based y-coordinate in the 
   ///   quasigroup table. Must be a non-negative integer less than 
   ///   <see cref="Order"/>.
   /// </param>
   /// <returns>
   ///   The next interim value from the quasigroup table corresponding to the
   ///   [interm, next] coordinates. Will be a non-negative integer less than
   ///   <see cref="Order"/>.
   /// </returns>
   Int32 this[Int32 interm, Int32 next] { get; }

   /// <summary>
   ///   Maps the specified character to its corresponding integer value.
   /// </summary>
   /// <param name="ch">
   ///   The character to be mapped to an integer value.</param>
   /// <returns>
   ///   An integer representing the mapped value of the specified character or 
   ///   -1 if the character is invalid for this quasigroup.
   /// </returns>
   Int32 MapCharacter(Char ch);

   /// <summary>
   ///   Calculates the check digit corresponding to the specified interim value.
   /// </summary>
   /// <param name="interim">
   ///   The Damm algorithm interim value used to compute the check digit. Must 
   ///   be a non-negative integer less than <see cref="Order"/>.
   /// </param>
   /// <returns>
   ///   The check digit as a character derived from the interim value.
   /// </returns>
   Char GetCheckDigit(Int32 interim);
}
