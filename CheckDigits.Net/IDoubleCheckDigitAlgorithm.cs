namespace CheckDigits.Net;

/// <summary>
///   Public contract for a check digit algorithm that uses a check value
///   consisting of two digits or characters.
/// </summary>
public interface IDoubleCheckDigitAlgorithm : ICheckDigitAlgorithm
{
   /// <summary>
   ///   Try to calculate a check digit for the input <paramref name="value"/>.
   /// </summary>
   /// <param name="value">
   ///   The value to calculate a check digit for.
   /// </param>
   /// <param name="checkDigit">
   ///   Output. The calculated check digit or <see langword="null"/> if it was 
   ///   not possible to calculate a check digit for the input 
   ///   <paramref name="value"/>.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if a check digit was successfully calculated
   ///   from the input <paramref name="value"/>; otherwise 
   ///   <see langword="false"/>.
   /// </returns>
   /// <remarks>
   ///   If the algorithm specifies a trailing (or leading) check digit 
   ///   positions then <paramref name="value"/> should not contain any check 
   ///   digit characters and the entire string will be used to calculate the 
   ///   check digit characters.
   ///   </para>
   ///   If the algorithm specifies an embedded check digit position then
   ///   <paramref name="value"/> should include space for the check digit
   ///   characters but those positions will be ignored when calculating the 
   ///   check digit.
   /// </remarks>
   Boolean TryCalculateCheckDigit(String value, out String checkDigit);
}
