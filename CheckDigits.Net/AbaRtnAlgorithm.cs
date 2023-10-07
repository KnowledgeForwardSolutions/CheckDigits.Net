// Ignore Spelling: Aba Rtn

namespace CheckDigits.Net;

/// <summary>
///   American Bankers Association (ABA) Routing Transit Number (RTN) algorithm.
///   Uses modulus 10 and weights of 3, 7 and 1 applied to every group of three
///   digits.
/// </summary>
/// <remarks>
///   <para>
///   Value length = 9.
///   </para>
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
///   Will detect all single-digit transcription errors and most two digit 
///   transpositions of adjacent digits.
///   </para>
/// </remarks>
public class AbaRtnAlgorithm : ISingleCheckDigitAlgorithm
{
   private const Int32 _calculateLength = 8;
   private const Int32 _validateLength = 9;
   private static readonly Int32[] _weights = new Int32[9] { 3, 7, 1, 3, 7, 1, 3, 7, 1 };

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.AbaRtnAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.AbaRtnAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value) || value.Length != _calculateLength)
      {
         return false;
      }

      var sum = 0;
      for (var index = 0; index < value.Length; index++)
      {
         var currentDigit = value[index].ToIntegerDigit();
         if (currentDigit < 0 || currentDigit > 9)
         {
            return false;
         }

         sum += currentDigit * _weights[index];
      }
      var mod = (10 - (sum % 10)) % 10;
      checkDigit = mod.ToDigitChar();

      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _validateLength)
      {
         return false;
      }

      var sum = 0;
      for (var index = 0; index < value.Length; index++)
      {
         var currentDigit = value[index].ToIntegerDigit();
         if (currentDigit < 0 || currentDigit > 9)
         {
            return false;
         }

         sum += currentDigit * _weights[index];
      }

      return sum % 10 == 0;
   }
}
