// Ignore Spelling: Figi

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Financial Instrument Global Identifier algorithm. Uses Modulus 10 "Double
///   Add Double" technique.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are decimal digits (0-9) and upper case consonants 
///   (BCDFGHJKLMNPQRSTVWXYZ).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit is the twelfth (right-most) digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and most two digit 
///   transpositions of adjacent digits (except 09 <-> 90). Will not detect 
///   transpositions of two letters. Will detect most twin errors 
///   (i.e. 11 <-> 44) except 22 <-> 55,  33 <-> 66 and 44 <-> 77.
///   </para>
/// </remarks>
public class FigiAlgorithm : ICheckDigitAlgorithm
{
   private const Int32 _expectedLength = 12;
   private static readonly Int32[] _characterMap = CharacterMapUtility.GetFigiCharacterMap();

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.FigiAlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.FigiAlgorithmName;

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _expectedLength)
      {
         return false;
      }

      var sum = 0;
      var evenPosition = false;
      for (var index = value.Length - 2; index >= 0; index--)
      {
         var ch = value[index];
         var num = ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ
            ? _characterMap[ch - CharConstants.DigitZero]
            : -1;
         if (num == -1)
         {
            return false;
         }
         else if (evenPosition)
         {
            num *= 2;
         }

         sum += (num / 10) + (num % 10);

         evenPosition = !evenPosition;
      }

      var checkDigit = (10 - (sum % 10)) % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }
}
