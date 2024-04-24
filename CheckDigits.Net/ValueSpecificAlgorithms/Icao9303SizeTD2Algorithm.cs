// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used to validate the check digits contained in the machine readable
///   zone of ICAO (International Civil Aviation Organization) Machine Readable 
///   Travel Document Size TD2.
/// </summary>
/// <remarks>
///   <para>
///   Validates the following fields in the machine readable zone:
///   <list type="bullet">
///      <item>Document number, line 2, characters 1-9, check digit in character 10</item>
///      <item>Optional document number extension, line 2, characters 29-35, check digit in final non-filler character</item>
///      <item>Date of birth, line 2, characters 14-19, check digit in character 20</item>
///      <item>Date of expiry, line 2, characters 22-27, check digit in character 28</item>
///      <item>Composite check digit for all of the above fields and their check digits</item>
///   </list>
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
public sealed class Icao9303SizeTD2Algorithm : ICheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [7, 3, 1];
   private static readonly Int32[] _fieldStartPositions = [0, 13, 21];
   private static readonly Int32[] _fieldSLengths = [9, 6, 6];
   private static readonly Int32[] _charMap = Icao9303CharacterMap.GetCharacterMap();
   private const Int32 _extendedDocumentNumberStart = 28;
   private const Int32 _extendedDocumentNumberLength = 5;

   private const Int32 _numFields = 3;
   private const Int32 _lineLength = 36;
   private LineSeparator _lineSeparator = LineSeparator.None;
   private Int32 _lineSeparatorLength = 0;
   private Int32 _expectedLength = _lineLength * 2;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303SizeTD2AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303SizeTD2AlgorithmName;

   /// <summary>
   ///   Specifies the character(s) used to separate lines in the value being
   ///   validated.
   /// </summary>
   /// <exception cref="ArgumentOutOfRangeException">
   ///   The value used to set the <see cref="LineSeparator"/> property is not
   ///   a defined member of the <see cref="LineSeparator"/> enumeration.
   /// </exception>
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
         _lineSeparatorLength = value.CharacterLength();
         _expectedLength = (_lineLength * 2) + _lineSeparatorLength;
      }
   }

   /// <inheritdoc/>
   public Boolean Validate(String value)
   {
      if (String.IsNullOrEmpty(value) || value.Length != _expectedLength)
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
         var start = _fieldStartPositions[fieldIndex] + _lineLength + _lineSeparatorLength;
         var end = start + _fieldSLengths[fieldIndex];
         for (var charIndex = start; charIndex < end; charIndex++)
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

         // Handle possible extended document number field. See
         // https://www.icao.int/publications/Documents/9303_p6_cons_en.pdf,
         // note j on page 16 for details.
         if (fieldIndex == 0 && value[end] == CharConstants.LeftAngleBracket)
         {
            start = _extendedDocumentNumberStart + _lineLength + _lineSeparatorLength;
            end = start + _extendedDocumentNumberLength;
            for (var charIndex = start; charIndex < end; charIndex++)
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

               if (value[charIndex + 2] == CharConstants.LeftAngleBracket)
               {
                  end = charIndex + 1;
                  break;
               }
            }
         }

         // Field check digit.
         ch = value[end];
         if (ch >= CharConstants.DigitZero && ch <= CharConstants.DigitNine)
         {
            num = ch.ToIntegerDigit();
         }
         else
         {
            return false;
         }
         if (num != fieldSum % 10)
         {
            return false;
         }

         compositeSum += num * _weights[compositeWeightIndex];
         compositeWeightIndex++;
      }

      var compositeCheckDigit = compositeSum % 10;

      return value[^1].ToIntegerDigit() == compositeCheckDigit;
   }
}
