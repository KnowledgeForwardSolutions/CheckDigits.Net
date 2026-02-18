// Ignore Spelling: Nhs

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   UK National Health Service (NHS) algorithm. A variation of the 
///   <see cref="Modulus11Algorithm"/> where remainders of 10 are considered 
///   invalid, thus resulting in check digits from 0-9.
/// </summary>
/// <remarks>
///   <para>
///   This algorithm is deprecated in favor of the 
///   <see cref="Modulus11DecimalAlgorithm"/> algorithm. Modulus11DecimalAlgorithm
///   is slightly more efficient and more flexible than this algorithm and follows 
///   the naming convention (Decimal/Extended) used for other modulus 11 
///   algorithms in this library. Modulus11DecimalAlgorithm removes the fixed 10 
///   character length requirement of NhsAlgorithm and instead allows values of
///   length 2 to 10 characters for validation. To completely replicate the 
///   behavior of NhsAlgorithm you will need to add your own length check to 
///   ensure that the value being validated is exactly 10 characters long.
///   </para>
///   <para>
///   While marked as obsolete, NhsAlgorithm is still available for use and
///   will not be removed in a future major release because Modulus11DecimalAlgorithm
///   is not a drop-in replacement for NhsAlgorithm.
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit is the right-most digit in the input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and all two digit 
///   transposition errors.
///   </para>
///   <para>
///   Value length is 10 characters.
///   </para>
/// </remarks>
[Obsolete("NhsAlgorithm is deprecated in favor of Modulus11DecimalAlgorithm.")]
public sealed class NhsAlgorithm : ICheckDigitAlgorithm
{
   private const Int32 _expectedLength = 10;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.NhsAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.NhsAlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _expectedLength)
      {
         return false;
      }

      var s = 0;
      var t = 0;
      for (var index = 0; index < value.Length - 1; index++)
      {
         var currentDigit = value![index].ToIntegerDigit();
         if (currentDigit < 0 || currentDigit > 9)
         {
               return false;
         }
         t += currentDigit;
         s += t;
      }
      s += t;

      var checkDigit = (11 - (s % 11)) % 11;

#pragma warning disable IDE0046 // Convert to conditional expression
      if (checkDigit == 10)
      {
         return false;
      }

      return value[^1].ToIntegerDigit() == checkDigit;
#pragma warning restore IDE0046 // Convert to conditional expression
   }
}
