namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Modulus 11 (Extended) algorithm which uses the IBM Modulus 11 weighting 
///   scheme of 2, 3, 4, 5, 6, 7 starting from the right-most non-check digit.
///   For strings longer than 6 characters, the weighting scheme repeats 
///   starting again with 2 for the 7th character from the right, etc. The check 
///   digit is calculated as the modulus 11 of the sum of weighted values.
/// </summary>
/// <remarks>
///   <para>
///   Calculating modulus 11 of the sum of weighted values results in a check
///   value from 0 to 10 (11 distinct values). Since it is not possible to 
///   represent 10 with a single decimal digit this algorithm extends the
///   possible check digit characters to include '0'-'9' and 'X' (represents a
///   check value of 10).
///   </para>
///   <para>
///   See <see cref="Modulus11_27DecimalAlgorithm"/> for a related modulus 11
///   algorithm that restricts check characters to '0'-'9', but at the cost of 
///   rejecting one out of every 11 values, or approximately 9.09%.
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9) or an 
///   uppercase 'X'.
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and all two digit 
///   transposition errors.
///   </para>
/// </remarks>
public class Modulus11_27ExtendedAlgorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private const Int32 _validateMinLength = 2;
   private static readonly Int32[] _weights = [2, 3, 4, 5, 6, 7];

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Modulus11_27ExtendedAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Modulus11_27ExtendedAlgorithmName;

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
