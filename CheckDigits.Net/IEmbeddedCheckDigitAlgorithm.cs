namespace CheckDigits.Net;

/// <summary>
///   Public contract for validating the check digit of a field embedded within 
///   a larger string.
/// </summary>
public interface IEmbeddedCheckDigitAlgorithm : ICheckDigitAlgorithm
{
   /// <summary>
   ///   Determine if the substring located at <paramref name="start"/> in the 
   ///   <paramref name="value"/> contains a valid check digit (or check digits).
   /// </summary>
   /// <param name="value">
   ///   The value to validate. 
   /// </param>
   /// <param name="start">
   ///   The index within the <paramref name="value"/> where the field to check
   ///   is located.
   /// </param>
   /// <param name="length">
   ///   The length of the field to check.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if the field check digit(s)  matches the check 
   ///   digit(s) calculated by this algorithm; otherwise <see langword="false"/>.
   /// </returns>
   /// <remarks>
   ///   Validate will return <see langword="false"/> if <paramref name="value"/>
   ///   is mal-formed. Examples of mal-formed values include:
   ///   <list type="bullet">
   ///     <item><paramref name="value"/> is <see langword="null"/></item>
   ///     <item><paramref name="value"/> is <see cref="String.Empty"/></item>
   ///     <item><paramref name="start"/> is less than zero</item>
   ///     <item><paramref name="value"/> is less than 2</item>
   ///     <item><paramref name="start"/> is greater than <paramref name="value"/> length</item>
   ///     <item><paramref name="start"/> plus <paramref name="length"/> exceeds <paramref name="value"/> length</item>
   ///   </list>  
   /// </remarks>
   Boolean Validate(String value, Int32 start, Int32 length);
}
