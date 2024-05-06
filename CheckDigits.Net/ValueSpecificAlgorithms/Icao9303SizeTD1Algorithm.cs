// Ignore Spelling: Icao

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   Algorithm used to validate the check digits contained in the machine readable
///   zone of ICAO (International Civil Aviation Organization) Machine Readable 
///   Travel Document Size TD1.
/// </summary>
/// <remarks>
///   <para>
///   Validates the following fields in the machine readable zone:
///   <list type="bullet">
///      <item>Document number, line 1, characters 6-14, check digit in character 15</item>
///      <item>Optional document number extension, line 1, characters 16-28, check digit in final non-filler character</item>
///      <item>Date of birth, line 2, characters 1-6, check digit in character 7</item>
///      <item>Date of expiry, line 2, characters 9-14, check digit in character 15</item>
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
public sealed class Icao9303SizeTD1Algorithm : ICheckDigitAlgorithm
{
   private static readonly Int32[] _weights = [7, 3, 1];
   private static readonly FieldDetails[] _fields = [
      new (0, 5, 9),    // Document number field
      new (1, 0, 6),    // Date of birth field
      new (1, 8, 6)];   // Date of expiry field
   private static readonly FieldDetails _extendedDocumentNumber = new (0, 15, 13);
   private const Int32 _numFields = 3;
   private const Int32 _lineLength = 30;
   private const Int32 _compositeCheckDigitPosition = 59;
   private static readonly Int32[] _charMap = Chars.Range(Chars.DigitZero, Chars.UpperCaseZ)
      .Select(x => Icao9303Algorithm.MapCharacter(x))
      .ToArray();

   private LineSeparator _lineSeparator = LineSeparator.None;
   private Int32 _lineSeparatorLength = 0;
   private Int32 _expectedLength = _lineLength * 3;

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Icao9303SizeTD1AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Icao9303SizeTD1AlgorithmName;

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
         _expectedLength = (_lineLength * 3) + (_lineSeparatorLength * 2);
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
         var (line, charPos, length) = _fields[fieldIndex];
         var start = (line * (_lineLength + _lineSeparatorLength)) + charPos;
         var end = start + length;
         for (var charIndex = start; charIndex < end; charIndex++)
         {
            ch = value[charIndex];
            num = (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
               ? _charMap[ch - Chars.DigitZero]
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
         // https://www.icao.int/publications/Documents/9303_p5_cons_en.pdf,
         // note j on page 17 for details.
         if (fieldIndex == 0 && value[end] == Chars.LeftAngleBracket)
         {
            (line, charPos, length) = _extendedDocumentNumber;
            start = (line * (_lineLength + _lineSeparatorLength)) + charPos;
            end = start + length;
            for (var charIndex = start; charIndex < end; charIndex++)
            {
               ch = value[charIndex];
               num = (ch >= Chars.DigitZero && ch <= Chars.UpperCaseZ)
                  ? _charMap[ch - Chars.DigitZero]
                  : -1;
               if (num == -1)
               {
                  return false;
               }

               fieldSum += num * _weights[fieldWeightIndex];
               fieldWeightIndex++;

               compositeSum += num * _weights[compositeWeightIndex];
               compositeWeightIndex++;

               if (value[charIndex + 2] == Chars.LeftAngleBracket)
               {
                  end = charIndex + 1;
                  break;
               }
            }
         }

         // Field check digit.
         ch = value[end];
         if (ch >= Chars.DigitZero && ch <= Chars.DigitNine)
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
      return value[_compositeCheckDigitPosition + _lineSeparatorLength].ToIntegerDigit() == compositeCheckDigit;
   }

   private record struct FieldDetails(Int32 Line, Int32 CharPosition, Int32 Length);
}
