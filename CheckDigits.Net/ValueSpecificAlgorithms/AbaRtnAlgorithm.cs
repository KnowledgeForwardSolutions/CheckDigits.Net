// Ignore Spelling: Aba Rtn

namespace CheckDigits.Net.ValueSpecificAlgorithms;

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
public sealed class AbaRtnAlgorithm : ICheckDigitAlgorithm
{
   private const Int32 _expectedLength = 9;
   private const Int32 _minWeight = 1;
   private const Int32 _initialWeight = 3;
   private const Int32 _maxWeight = 7;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.AbaRtnAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.AbaRtnAlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _expectedLength)
      {
         return false;
      }

      var sum = 0;
      var weight = _initialWeight;
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var ch = value[index];
         if (!ch.IsAsciiDigit())
         {
            return false;
         }
         var currentDigit = ch.MapAsciiDigit();

         sum += currentDigit * weight;

         weight = weight * 2 + 1;      // 1 -> 3, 3 -> 7
         if (weight > _maxWeight)
         {
            weight = _minWeight;                // 7 -> 1
         }
      }

      return sum % 10 == 0;
   }
}
