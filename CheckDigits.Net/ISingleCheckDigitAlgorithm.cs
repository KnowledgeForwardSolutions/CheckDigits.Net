namespace CheckDigits.Net;

/// <summary>
///   Public contract for a check digit algorithm that uses a check value
///   consisting of one digit or character.
/// </summary>
public interface ISingleCheckDigitAlgorithm : ICheckDigitAlgorithm
{
   /// <summary>
   ///   Try to calculate a check digit for the input <paramref name="value"/>.
   /// </summary>
   /// <param name="value">
   ///   The value to calculate a check digit for.
   /// </param>
   /// <param name="checkDigit">
   ///   Output. The calculated check digit or '\0' if it was not possible to 
   ///   calculate a check digit for the input <paramref name="value"/>.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if a check digit was successfully calculated
   ///   from the input <paramref name="value"/>; otherwise 
   ///   <see langword="false"/>.
   /// </returns>
   /// <remarks>
   ///   If the algorithm specifies a trailing (or leading) check digit position
   ///   then <paramref name="value"/> should not contain a check digit and the
   ///   entire string will be used to calculate the check digit.
   ///   <para>
   ///   If the algorithm specifies an embedded check digit position then
   ///   <paramref name="value"/> should include space for the check digit but
   ///   that position will be ignored when calculating the check digit.
   ///   </para>
   /// </remarks>
   Boolean TryCalculateCheckDigit(String value, out Char checkDigit);
}
