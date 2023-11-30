// Ignore Spelling: Verhoeff

namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   Algorithm developed by Jacobus Verhoeff that uses dihedral groups instead
///   of modulus operations.
/// </summary>
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
///   transpositions of adjacent digits
///   </para>
/// </remarks>
public sealed class VerhoeffAlgorithm : ISingleCheckDigitAlgorithm
{
    private static readonly VerhoeffInverseTable _inverseTable =
       VerhoeffInverseTable.Instance;
   private static readonly VerhoeffMultiplicationTable _multiplicationTable =
      VerhoeffMultiplicationTable.Instance;
   private static readonly VerhoeffPermutationTable _permutationTable =
      VerhoeffPermutationTable.Instance;

   /// <inheritdoc/>
   public string AlgorithmDescription => Resources.VerhoeffAlgorithmDescription;

    /// <inheritdoc/>
    public string AlgorithmName => Resources.VerhoeffAlgorithmName;

   /// <inheritdoc/>
   public bool TryCalculateCheckDigit(string value, out char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (string.IsNullOrEmpty(value))
      {
         return false;
      }

      var c = 0;
      var i = 1;
      for (var index = value.Length - 1; index >= 0; index--)
      {
         var n = value![index].ToIntegerDigit();
         if (n < 0 || n > 9)
         {
            return false;
         }

         var p = _permutationTable[i % 8, n];
         c = _multiplicationTable[c, p];

         i++;
      }
      checkDigit = _inverseTable[c].ToDigitChar();

      return true;
   }

   /// <inheritdoc/>
   public bool Validate(string value)
   {
      if (string.IsNullOrEmpty(value) || value.Length < 2)
      {
         return false;
      }

      var c = 0;
      var i = 0;
      for (var index = value.Length - 1; index >= 0; index--)
      {
         var n = value![index].ToIntegerDigit();
         if (n < 0 || n > 9)
         {
            return false;
         }

         var p = _permutationTable[i % 8, n];
         c = _multiplicationTable[c, p];

         i++;
      }
      return c == 0;
   }
}
