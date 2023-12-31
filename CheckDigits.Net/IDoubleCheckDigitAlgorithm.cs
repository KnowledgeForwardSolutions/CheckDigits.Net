﻿namespace CheckDigits.Net;

/// <summary>
///   Public contract for a check digit algorithm that uses a check value
///   consisting of two digits or characters.
/// </summary>
public interface IDoubleCheckDigitAlgorithm : ICheckDigitAlgorithm
{
   /// <summary>
   ///   Try to calculate the check digits for the input <paramref name="value"/>.
   /// </summary>
   /// <param name="value">
   ///   The value to calculate a check digit for.
   /// </param>
   /// <param name="first">
   ///   Output. The first of two calculated check digits or <see langword="null"/> 
   ///   if it was not possible to calculate check digits for the input 
   ///   <paramref name="value"/>.
   /// </param>
   /// <param name="second">
   ///   Output. The second of two calculated check digits or <see langword="null"/> 
   ///   if it was not possible to calculate check digits for the input 
   ///   <paramref name="value"/>.
   /// </param>
   /// <returns>
   ///   <see langword="true"/> if check digits were successfully calculated
   ///   from the input <paramref name="value"/>; otherwise 
   ///   <see langword="false"/>.
   /// </returns>
   /// <remarks>
   ///   This method assumes that the supplied <paramref name="value"/> does not
   ///   include check digit characters and uses the entire value to calculate 
   ///   the check digits. If the algorithm specifies that the check digit
   ///   positions are embedded in the value (example: International Bank 
   ///   Account Number) then this method assumes that the value includes check 
   ///   digit positions but those positions are ignored while calculating the 
   ///   check digits.
   /// </remarks>
   Boolean TryCalculateCheckDigits(String value, out Char first, out Char second);
}
