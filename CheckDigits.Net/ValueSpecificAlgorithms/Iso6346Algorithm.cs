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
   private static readonly Int32[] _charValues = Chars.Range(Chars.DigitZero, Chars.UpperCaseZ)
         .Select(x => MapCharacter(x))
         .ToArray();
   private static readonly Int32[] _weights = [1, 2, 4, 8, 16, 32, 64, 128, 256, 512];

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso6346AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso6346AlgorithmName;

   /// <summary>
   ///   Map a character to its integer equivalent in the 
   ///   <see cref="Iso6346Algorithm"/>. Characters that are not valid for the 
   ///   Iso6346Algorithm are mapped to -1.
   /// </summary>
   /// <param name="ch">
   ///   The character to map.
   /// </param>
   /// <returns>
   ///   The integer value associated with <paramref name="ch"/>.
   /// </returns>
   public static Int32 MapCharacter(Char ch) => ch switch
   {
      var d when ch >= Chars.DigitZero && ch <= Chars.DigitNine => d.ToIntegerDigit(),
      Chars.UpperCaseA => 10,
      var b when ch >= Chars.UpperCaseB && ch <= Chars.UpperCaseK => b - Chars.UpperCaseB + 12,
      var l when ch >= Chars.UpperCaseL && ch <= Chars.UpperCaseU => l - Chars.UpperCaseL + 23,
      var v when ch >= Chars.UpperCaseV && ch <= Chars.UpperCaseZ => v - Chars.UpperCaseV + 34,
      _ => -1
   };

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
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
         if (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
         {
            var offset = ch - Chars.DigitZero;
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
         if (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
         {
            var offset = ch - Chars.DigitZero;
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
