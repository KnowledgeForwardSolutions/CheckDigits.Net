// Ignore Spelling: Aba Rtn

namespace CheckDigits.Net.ValueSpecificAlgorithms;

/// <summary>
///   American Bankers Association (ABA) Routing Transit Number (RTN) algorithm.
///   Uses modulus 10 and weights of 3, 7 and 1 applied to every group of three
///   digits.
/// </summary>
/// <remarks>
///   <para>
///   Value length = 9.
///   </para>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9).
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single-digit transcription errors and most two digit 
///   transpositions of adjacent digits.
///   </para>
/// </remarks>
public sealed class AbaRtnAlgorithm : ICheckDigitAlgorithm
{
    private const int _expectedLength = 9;
    private static readonly int[] _weights = new int[9] { 3, 7, 1, 3, 7, 1, 3, 7, 1 };

    /// <inheritdoc/>
    public string AlgorithmDescription => Resources.AbaRtnAlgorithmDescription;

    /// <inheritdoc/>
    public string AlgorithmName => Resources.AbaRtnAlgorithmName;

    /// <inheritdoc/>
    public bool Validate(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != _expectedLength)
        {
            return false;
        }

        var sum = 0;
        for (var index = 0; index < value.Length; index++)
        {
            var currentDigit = value[index].ToIntegerDigit();
            if (currentDigit < 0 || currentDigit > 9)
            {
                return false;
            }

            sum += currentDigit * _weights[index];
        }

        return sum % 10 == 0;
    }
}
