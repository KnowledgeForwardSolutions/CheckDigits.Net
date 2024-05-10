// Ignore Spelling: Sedol

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used by United Kingdom and Ireland security identification 
///   numbers (SEDOL = Stock Exchange Daily Official List).
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters, except vowels (0-9, BCDFGHJKLMNPQRSTVWXYZ).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
/// </remarks>
public class SedolAlgorithm : ICheckDigitAlgorithm
{
   private const Int32 _validateLength = 7;
   private static readonly Int32[] _charValues = Chars.Range(Chars.DigitZero, Chars.UpperCaseZ)
      .Select(x => Chars.MapBetanumericCharacter(x))
      .ToArray();
   private static readonly Int32[] _weights = [1, 3, 1, 7, 3, 9];

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.SedolAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.SedolAlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _validateLength)
      {
         return false;
      }

      var sum = 0;
      Int32 num;
      for(var index = 0; index < value.Length - 1; index++)
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
      var checkDigit = (10 - (sum % 10)) % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }
}
