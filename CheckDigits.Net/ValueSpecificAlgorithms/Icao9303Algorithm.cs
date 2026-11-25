// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Modulus 10 algorithm used by ICAO (International Civil Aviation 
///   Organization) Machine Readable Travel Documents. The algorithm uses 
///   weights 7, 3 and 1 (weights are applied starting from the left-most 
///   character).
/// </summary>
/// <remarks>
///   <para>
///      Valid characters are decimal digits (0-9), uppercase alphabetic 
///      characters (A-Z) and a filler character ('&lt;').
///   </para>
///   <para>
///      Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///      Assumes that the check digit is the right-most digit in the input 
///      value.
///   </para>
///   <para>
///      Can not detect single character transcription errors where the 
///      difference between the correct and incorrect characters is 10, i.e. 
///      0 -> A, B -> L. Nor can the algorithm detect two character 
///      transposition errors where the difference between the transposed 
///      characters is 10, i.e. BL -> LB.
///   </para>
/// </remarks>
public sealed class Icao9303Algorithm : ISingleCheckDigitAlgorithm
{
   private const Int32 _initialWeight = 7;
   private const Int32 _validateMinLength = 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303AlgorithmName;

   /// <inheritdoc/>
   public Boolean TryCalculateCheckDigit(String value, out Char checkDigit)
   {
      checkDigit = Chars.NUL;
      if (String.IsNullOrEmpty(value))
      {
         return false;
      }

      var sum = 0;
      var weight = _initialWeight;
      var processLength = value.Length;
      for (var index = 0; index < processLength; index++)
      {
         var num = value[index].MapIcao0303Character();
         if (num == -1)
         {
            return false;
         }
         sum += num * weight;
         weight = weight.Icao9303NextWeight();
      }
      
      checkDigit = (sum % 10).ToAsciiDigit();

      return true;
   }


   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length < _validateMinLength)
      {
         return false;
      }

      var sum = 0;
      var weight = _initialWeight;
      var processLength = value.Length - 1;
      for (var index = 0; index < processLength; index++)
      {
         var num = value[index].MapIcao0303Character();
         if (num == -1)
         {
            return false;
         }
         sum += num * weight;
         weight = weight.Icao9303NextWeight();
      }
      var checkDigit = sum % 10;

      return value[^1].MapAsciiDigit() == checkDigit;
   }
}
