// Ignore Spelling: Damm

namespace CheckDigits.Net;

/// <summary>
///   Algorithm developed by H. Michael Damm that uses a quasigroup table 
///   instead of modulus operations.
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
public class DammAlgorithm : ISingleCheckDigitAlgorithm
{
   private static readonly DammQuasigroupTable _quasigroupTable =
      DammQuasigroupTable.Instance;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.DammAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.DammAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }
      if (!CalculateCheckDigit(value, out var digit))
      {
         return false;
      }

      checkDigit = digit.ToDigitChar();
      return true;
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
      => !String.IsNullOrEmpty(value)
         && value.Length > 1
         && CalculateCheckDigit(value, out var checkDigit)
         && checkDigit == 0;
   //{
   //   if (String.IsNullOrEmpty(value) || value.Length < 2)
   //   {
   //      return false;
   //   }

   //   var interim = 0;
   //   for (var index = 0; index < value.Length; index++)
   //   {
   //      var current = value![index].ToIntegerDigit();
   //      if (current < 0 || current > 9)
   //      {
   //         return false;
   //      }
   //      interim = _quasigroupTable[interim, current];
   //   }

   //   return interim == 0;
   //}

   public Boolean CalculateCheckDigit(String value, out Int32 checkDigit)
   {
      checkDigit = 0;
      for (var index = 0; index < value.Length; index++)
      {
         var current = value![index].ToIntegerDigit();
         if (current < 0 || current > 9)
         {
            return false;
         }
         checkDigit = _quasigroupTable[checkDigit, current];
      }

      return true;
   }
}
