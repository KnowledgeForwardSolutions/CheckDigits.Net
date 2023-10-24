// Ignore Spelling: Nhs

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   UK National Health Service (NHS) algorithm. A variation of the 
///   <see cref="Modulus11Algorithm"/> where remainders of 10 are considered 
///   invalid, thus resulting in check digits from 0-9.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit is the right-most digit in the input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and all two digit 
///   transposition errors.
///   </para>
///   <para>
///   Value length is 10 characters.
///   </para>
/// </remarks>
public class NhsAlgorithm : ICheckDigitAlgorithm
{
    private const int _expectedLength = 10;

    /// <inheritdoc/>
    public string AlgorithmDescription => Resources.NhsAlgorithmDescription;

    /// <inheritdoc/>
    public string AlgorithmName => Resources.NhsAlgorithmName;

    /// <inheritdoc/>
    public bool Validate(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != _expectedLength)
        {
            return false;
        }

        var s = 0;
        var t = 0;
        for (var index = 0; index < value.Length - 1; index++)
        {
            var currentDigit = value![index].ToIntegerDigit();
            if (currentDigit < 0 || currentDigit > 9)
            {
                return false;
            }
            t += currentDigit;
            s += t;
        }
        s += t;

        var checkDigit = (11 - s % 11) % 11;
        if (checkDigit == 10)
        {
            return false;
        }

        return value[^1].ToIntegerDigit() == checkDigit;
    }
}
