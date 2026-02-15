namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Modulus 11 (Decimal) algorithm where every digit is weighted by its 
///   position in the value, starting from the right-most position, and the 
///   check digit being the modulus 11 of the sum of weighted values.
/// </summary>
///   <para>
///   Calculating modulus 11 of the sum of weighted values results in a check
///   value from 0 to 10 (11 distinct values). Since it is not possible to 
///   represent 10 with a single decimal digit, any value with a check value of
///   10 is rejected by this algorithm. Both the Validate and 
///   TryCalculateCheckDigit methods will return false if the value results in
///   a check value of 10. This eliminates approximately 9.09% of possible 
///   numbers from consideration.
///   </para>
/// <remarks>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and all two digit 
///   transposition errors.
///   </para>
///   <para>
///   Maximum length allowed is 9 characters for calculating a new check digit 
///   and 10 characters for validating a value that contains a check digit.
///   </para>
/// </remarks>
public class Modulus11DecimalAlgorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _tryCalculateMaxLength = 9;
   private const Int32 _validateMinLength = 2;
   private const Int32 _validateMaxLength = 10;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Modulus11DecimalAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Modulus11DecimalAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      throw new NotImplementedException();
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      throw new NotImplementedException();
   }

   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      throw new NotImplementedException();
   }
}
