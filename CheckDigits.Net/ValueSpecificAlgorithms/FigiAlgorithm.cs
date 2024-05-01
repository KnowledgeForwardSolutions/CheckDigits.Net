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
   private static readonly FigiLookupTable _lookupTable = new();

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
      var oddEvenIndex = new ModulusInt32(2);
      for (var index = value.Length - 2; index >= 0; index--)
      {
         var num = _lookupTable[value[index], oddEvenIndex];
         if (num == -1)
         {
            return false;
         }
         sum += num;
         oddEvenIndex++;
      }

      var checkDigit = (10 - (sum % 10)) % 10;

      return value[^1].ToIntegerDigit() == checkDigit;
   }

   /// <summary>
   ///   Optimized algorithm lookup table. Handles the mapping of characters to
   ///   their integer equivalent and optimizes the processing for each character
   ///   by creating a pre-computed lookup table of the processed value for each
   ///   character, including the doubling applied to even position characters.
   /// </summary>
   internal class FigiLookupTable
   {
      private const Int32 _mapCount = CharConstants.UpperCaseZ - CharConstants.DigitZero + 1;
      private static readonly Int32[] _values = [.. GetOddCharacterLookup(), .. GetEvenCharacterLookup()];

      /// <summary>
      ///   Get the pre-computed value for the <paramref name="ch"/> and the
      ///   character's <paramref name="oddEven"/> position.
      /// </summary>
      /// <param name="ch">
      ///   The character to lookup.
      /// </param>
      /// <param name="oddEven">
      ///   Zero (0) for odd position characters (no doubling) and one (1) for
      ///   even position characters (with doubling).
      /// </param>
      /// <returns>
      ///   The <paramref name="ch"/>'s pre-computed value or -1 if the character
      ///   is not 0-9 or BCDFGHJKLMNPQRSTVWXYZ.
      /// </returns>
      /// <remarks>
      ///   2D array collapsed to a 1D array to take advantage of .Net optimization
      ///   for 1D array access. This is about 25% more efficient, even with the
      ///   manual calculation to required to treat a 1D array as a logical 2D
      ///   array.
      /// </remarks>
      public Int32 this[Char ch, Int32 oddEven]
         => ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ
            ? _values[(oddEven * _mapCount) + ch - CharConstants.DigitZero]
            : -1;

      private static Int32[] GetOddCharacterLookup() =>
         CharacterMapUtility.GetFigiCharacterMap()
            .Select(x => x switch
            {
               -1 => -1,
               _ => (x / 10) + (x % 10)
            }).ToArray();

      private static Int32[] GetEvenCharacterLookup() =>
         CharacterMapUtility.GetFigiCharacterMap()
            .Select(x => x switch
            {
               -1 => -1,
               _ => (x * 2 / 10) + (x * 2 % 10)
            }).ToArray();
   }
}
