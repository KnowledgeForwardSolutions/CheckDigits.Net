namespace CheckDigits.Net;

/// <summary>
///   Public contract for validating a string that contains a check digit or
///   check digits. Supports masking to include or exclude characters from the
///   check digit calculation.
/// </summary>
public interface IMaskedCheckDigitAlgorithm : ICheckDigitAlgorithm
{
   /// <summary>
   ///   Determine if the <paramref name="value"/> contains a valid check digit
   ///   (or check digits).
   /// </summary>
   /// <param name="value">
   ///   The value to validate. 
   /// </param>
   /// <param name="mask">
   ///   Mask used to include or exclude characters from the check digit
   ///   calculation. Note that check digit characters are generally located at
   ///   fixed locations (e.g., last character) and the mask is not consulted
   ///   when considering the check digit character(s).
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
   Boolean Validate(String value, ICheckDigitMask mask);
}
