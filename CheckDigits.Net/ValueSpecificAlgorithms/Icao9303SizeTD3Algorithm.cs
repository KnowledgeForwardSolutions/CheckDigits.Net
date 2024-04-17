// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used to validate the check digits contained in the machine readable
///   zone of ICAO (International Civil Aviation Organization) Machine Readable 
///   Passports and other Size TD3 travel documents.
/// </summary>
/// <remarks>
///   <para>
///   Validates the following fields in the machine readable zone:
///   <list type="bullet">
///      <item>Passport number, line 2, characters 1-9, check digit in character 10</item>
///      <item>Date of birth, line 2, characters 14-19, check digit in character 20</item>
///      <item>Date of expiry, line 2, characters 22-27, check digit in character 28</item>
///      <item>Optional personal number, line 2, characters 29-42, check digit in character 43</item>
///      <item>Composite check digit for all of the above fields and their check digits</item>
///   </list>
///   </para>
///   <para>
///   If the optional personal number field is not used and consists of all 
///   filler characters ('<') then the check digit for that field will be either
///   a filler character or a digit zero. The choice to use a filler character
///   or digit zero is left to the issuing authorithy and either version is 
///   supported by the algorithm.
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9), uppercase alphabetic characters 
///   (A-Z) and a filler character ('<').
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Uses the same Modulus 10 algorithm as the <see cref="Icao9303Algorithm"/>
///   and has the same weaknesses as that algorithm.
///   </para>
/// </remarks>
public sealed class Icao9303SizeTD3Algorithm : ICheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [7, 3, 1];
   private static readonly Int32[] _fieldStartPositions = [0, 13, 21, 28];
   private static readonly Int32[] _fieldSLengths = [9, 6, 6, 14];
   private const Int32 _numFields = 4;
   private const Int32 _lineLength = 44;
   private static readonly Int32[] _charMap = Icao9303CharacterMap.GetCharacterMap();

   private LineSeparator _lineSeparator = LineSeparator.None;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303SizeTD3AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303SizeTD3AlgorithmName;

   /// <summary>
   ///   Specifies the character(s) used to separate lines in the value being
   ///   validated.
   /// </summary>
   public LineSeparator LineSeparator
   {
      get => _lineSeparator;
      set
      {
         if (!value.IsDefined())
         {
            throw new ArgumentOutOfRangeException(nameof(value), value, Resources.LineSeparatorInvalidValueMessage);
         }

         _lineSeparator = value;
      }
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      var lineSeparatorLength = LineSeparator switch
      {
         LineSeparator.Crlf => 2,
         LineSeparator.Lf => 1,
         _ => 0
      };

      if (String.IsNullOrEmpty(value) || value.Length != (_lineLength * 2) + lineSeparatorLength)
      {
         return false;
      }

      Char ch;
      Int32 num;
      var compositeSum = 0;
      var compositeWeightIndex = new ModulusInt32(3);
      for (var fieldIndex = 0; fieldIndex < _numFields; fieldIndex++)
      {
         var fieldSum = 0;
         var fieldWeightIndex = new ModulusInt32(3);
         var start = _fieldStartPositions[fieldIndex] + _lineLength + lineSeparatorLength;
         var end = start + _fieldSLengths[fieldIndex];
         for(var charIndex = start; charIndex < end; charIndex++)
         {
            ch = value[charIndex];
            num = (ch >= CharConstants.DigitZero && ch <= CharConstants.UpperCaseZ)
               ? _charMap[ch - CharConstants.DigitZero]
               : -1;
            if (num == -1)
            {
               return false;
            }

            fieldSum += num * _weights[fieldWeightIndex];
            fieldWeightIndex++;

            compositeSum += num * _weights[compositeWeightIndex];
            compositeWeightIndex++;
         }

         // Handle field check digit for composite check digit calculations.
         ch = value[end];
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else if (fieldIndex == _numFields - 1 && ch == CharConstants.LeftAngleBracket)
         {
            // Only allowed for final, optional field
            num = 0;
         }
         else
         {
            return false;
         }

         compositeSum += num * _weights[compositeWeightIndex];
         compositeWeightIndex++;

         // Test field check digit.
         if (num != fieldSum % 10)
         {
            return false;
         }
      }

      var compositeCheckDigit = compositeSum % 10;
      
      return value[^1].ToIntegerDigit() == compositeCheckDigit;
   }
}
