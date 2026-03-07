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
///   transpositions of adjacent digits.
///   </para>
/// </remarks>
public sealed class VerhoeffAlgorithm : ISingleCheckDigitAlgorithm, IMaskedCheckDigitAlgorithm
{
   private static readonly VerhoeffInverseTable _inverseTable =
      VerhoeffInverseTable.Instance;
   private static readonly VerhoeffMultiplicationTable _multiplicationTable =
      VerhoeffMultiplicationTable.Instance;
   private static readonly VerhoeffPermutationTable _permutationTable =
      VerhoeffPermutationTable.Instance;
   private const Int32 _validateMinLength = 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.VerhoeffAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.VerhoeffAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var c = 0;
      var i = 1;
      for (var index = value.Length - 1; index >= 0; index--)
      {
         var n = value![index].ToIntegerDigit();
         if (n.IsInvalidDigit())
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
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      var c = 0;
      var i = 0;
      for (var index = value.Length - 1; index >= 0; index--)
      {
         var n = value![index].ToIntegerDigit();
         if (n.IsInvalidDigit())
         {
            return false;
         }

         var p = _permutationTable[i % 8, n];
         c = _multiplicationTable[c, p];

         i++;
      }

      return c == 0;
   }
 
   /// <inheritdoc/>
   public Boolean Validate(String value, ICheckDigitMask mask)
   {
      if (mask is null)
      {
         throw new ArgumentNullException(nameof(mask), Resources.NullMaskMessage);
      }
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var c = 0;
      var i = 0;

      // Handle check digit outside of loop to prevent the mask from excluding it.
      var n = value[^1].ToIntegerDigit();
      if (n.IsInvalidDigit())
      {
         return false;
      }
      var p = _permutationTable[i % 8, n];
      c = _multiplicationTable[c, p];
      i++;

      var processedDigits = 0;
      for (var index = value.Length - 2; index >= 0; index--)     // Minus 2 because check digit is handled outside of loop
      {
         if (mask.ExcludeCharacter(index))
         {
            continue;
         }
         n = value[index].ToIntegerDigit();
         if (n.IsInvalidDigit())
         {
            return false;
         }

         p = _permutationTable[i % 8, n];
         c = _multiplicationTable[c, p];

         i++;
         processedDigits++;
      }
      if (processedDigits == 0)
      {
         return false;
      }

      return c == 0;
   }
}
