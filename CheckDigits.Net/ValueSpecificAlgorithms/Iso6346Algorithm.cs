namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used by ISO 6346 compliant shipping container numbers
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z)
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
/// </remarks>
public class Iso6346Algorithm : ISingleCheckDigitAlgorithm
{
   private const Int32 _calculateLength = 10;
   private const Int32 _validateLength = 11;

   // Character numeric values. (-1 for invalid chars)                           :,  ;,  <,  =,  >,  ?,  @,  A,  B,  C,  D,  E,  F,  G,  H,  I,  J,  K,  L,  M,  N,  O,  P,  Q,  R,  S,  T,  U,  V,  W,  X,  Y,  Z  
   private static readonly Int32[] _charValues = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, -1, -1, -1, -1, -1, -1, -1, 10, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 34, 35, 36, 37, 38];
   private static readonly Int32[] _weights = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512];

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso6346AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso6346AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = CharConstants.NUL;
      if (String.IsNullOrEmpty(value) || value.Length != _calculateLength)
      {
         return false;
      }

      var sum = 0;
      Int32 num;
      for (var index = 0; index < value.Length; index++)
      {
         var ch = value[index];
         num = -1;
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ)
         {
            var offset = ch - CharConstants.DigitZero;
            num = _charValues[offset];
         }

         if (num == -1)
         {
            return false;
         }
         sum += num * _weights[index];
      }
      var modulo = sum % 11;
      checkDigit = (modulo == 10 ? 0 : modulo).ToDigitChar();

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
      Int32 num;
      for (var index = 0; index < value.Length - 1; index++)
      {
         var ch = value[index];
         num = -1;
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ)
         {
            var offset = ch - CharConstants.DigitZero;
            num = _charValues[offset];
         }

         if (num == -1)
         {
            return false;
         }
         sum += num * _weights[index];
      }
      var modulo = sum % 11;
      var checkDigit = modulo == 10 ? 0 : modulo;

      return value[^1].ToIntegerDigit() == checkDigit;
   }
}
