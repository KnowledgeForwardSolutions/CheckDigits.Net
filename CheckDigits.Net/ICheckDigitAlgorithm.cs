namespace CheckDigits.Net;

/// <summary>
///   Public contract for validating a string that contains a check digit or
///   check digits.
/// </summary>
public interface ICheckDigitAlgorithm
{
   /// <summary>
   ///   Description of the algorithm details.
   /// </summary>
   String AlgorithmDescription { get; }

   /// <summary>
   ///   The name of the algorithm.
   /// </summary>
   String AlgorithmName { get; }

   /// <summary>
   ///   Determine if the <paramref name="value"/> contains a valid check digit
   ///   (or check digits).
   /// </summary>
   /// <param name="value">
   ///   The value to validate. 
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if the check digit(s) contained in
   ///   <paramref name="value"/> matches the check digit(s) calculated by this
   ///   algorithm; otherwise <see langword="false"/>.
   /// </returns>
   /// <remarks>
   ///   Validate will return <see langword="false"/> if <paramref name="value"/>
   ///   is mal-formed. Examples of mal-formed values are <see langword="null"/>,
   ///   <see cref="String.Empty"/> or a string that is of invalid length for 
   ///   this algorithm.
   /// </remarks>
   Boolean Validate(String value);
}
