// Ignore Spelling: Ncd

namespace CheckDigits.Net.GeneralAlgorithms;

/// <summary>
///   NOID Check Digit Algorithm for betanumeric characters and modulus 29.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are betanumeric characters (0123456789bcdfghjkmnpqrstvwxz).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a betanumeric character (0123456789bcdfghjkmnpqrstvwxz).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-character transcription errors and all transpositions 
///   of adjacent characters when the value's length is less than 29 characters.
///   Slightly less capable for values of 29 characters or greater.
///   </para>
/// </remarks>
public class NcdAlgorithm : ISingleCheckDigitAlgorithm
{
   private const String _checkCharacters = "0123456789bcdfghjkmnpqrstvwxz";
   //                                                        b   c   d  e   f   g   h  i   j   k  l   m   n  o   p   q   r   s   t  u   v   w   x  y   z
   private static readonly Int32[] _letters = new Int32[] { 10, 11, 12, 0, 13, 14, 15, 0, 16, 17, 0, 18, 19, 0, 20, 21, 22, 23, 24, 0, 25, 26, 27, 0, 28 };

   /// <inheritdoc/>
   public string AlgorithmDescription => Resources.NcdAlgorithmDescription;

   /// <inheritdoc/>
   public string AlgorithmName => Resources.NcdAlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit) => throw new NotImplementedException();

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < 2)
      {
         return false;
      }

      Int32 num;
      var sum = 0;
      for(var index = 0; index < value.Length - 1; index++)
      {
         var ch = value[index];
         num = ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine
            ? ch.ToIntegerDigit()
            : ch >= CharConstants.LowerCaseB && ch <= CharConstants.LowerCaseZ 
               ? _letters[ch - CharConstants.LowerCaseB] 
               : 0;
         sum += num * (index + 1);
      }

      var checksum = sum % 29;
      
      return value[^1] == _checkCharacters[checksum];
   }
}
