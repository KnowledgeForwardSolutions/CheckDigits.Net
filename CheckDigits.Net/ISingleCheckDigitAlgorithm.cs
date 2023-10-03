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
   ///   This method assumes that the supplied <paramref name="value"/> does not
   ///   include a check digit character and uses the entire value to calculate 
   ///   the check digit. If the algorithm specifies that the check digit
   ///   position is embedded in the value (example: Vehicle Identification 
   ///   Number) then this method assumes that the value includes a check digit
   ///   position but that position is ignored while calculating the check digit.
   /// </remarks>
   Boolean TryCalculateCheckDigit(String value, out Char checkDigit);
}
