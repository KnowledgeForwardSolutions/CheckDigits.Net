// Ignore Spelling: Figi

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Financial Instrument Global Identifier algorithm. Uses Modulus 10 "Double
///   Add Double" technique.
/// </summary>
/// <remarks>
///   <para>
///      Valid characters are decimal digits (0-9) and upper case consonants 
///      (BCDFGHJKLMNPQRSTVWXYZ).
///   </para>
///   <para>
///      Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///      Assumes that the check digit is the twelfth (right-most) digit in the
///      input value.
///   </para>
///   <para>
///      Will detect all single-digit transcription errors and most two digit 
///      transpositions of adjacent digits (except 09 <-> 90). Will not detect 
///      transpositions of two letters. Will detect most twin errors 
///      (i.e. 11 <-> 44) except 22 <-> 55,  33 <-> 66 and 44 <-> 77.
///   </para>
/// </remarks>
public class FigiAlgorithm : ICheckDigitAlgorithm
{
   private const Int32 _expectedLength = 12;
   private static readonly Int32[] _oddLookupTable = 
      CharacterMap.CreateCharacterMap(Chars.DigitZero, Chars.UpperCaseZ, MapOddCharacter);
   private static readonly Int32[] _evenLookupTable = 
      CharacterMap.CreateCharacterMap(Chars.DigitZero, Chars.UpperCaseZ, MapEvenCharacter);

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
      var oddPosition = true;
      Char ch;
      for (var index = value.Length - 2; index >= 0; index--)
      {
         ch = value[index];
         var num = -1;
         if (ch is >= Chars.DigitZero and <= Chars.UpperCaseZ)
         {
            var offset = ch - Chars.DigitZero;
            num = oddPosition ? _oddLookupTable[offset] : _evenLookupTable[offset];
         }
         if (num == -1)
         {
            return false;
         }
         sum += num;
         oddPosition = !oddPosition;
      }

      var checkDigit = (10 - (sum % 10)) % 10;

      ch = value[^1];
      if (!ch.IsAsciiDigit())
      {
         return false;
      }

      return ch.MapAsciiDigit() == checkDigit;
   }

   // Odd characters are not doubled, but the individual digits of a mapped
   // letter character are summed.
   private static Int32 MapOddCharacter(Char ch)
      => ch switch
      {
         _ when ch.IsAsciiDigit() => ch.MapAsciiDigit(),
         _ when ch.IsAsciiConsonantUpper() => (ch.MapAsciiUppercaseAlpha() + 10).SumIndividualDigits(),
         _ => -1
      };

   // Even characters are doubled and then the individual digits are summed.
   private static Int32 MapEvenCharacter(Char ch)
      => ch switch
      {
         _ when ch.IsAsciiDigit() => (ch.MapAsciiDigit() * 2).SumIndividualDigits(),
         _ when ch.IsAsciiConsonantUpper() => ((ch.MapAsciiUppercaseAlpha() + 10) * 2).SumIndividualDigits(),
         _ => -1
      };
}
