namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 11-2 check digit algorithm. Pure system algorithm with
///   modulus 11 and radix 2.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are decimal digits (0-9).
///   </para>
///   <para>
///   Check digit calculated by the algorithm is a decimal digit (0-9) or an 
///   uppercase 'X'.
///   </para>
///   <para>
///   Assumes that the check digit (if present) is the right-most digit in the
///   input value.
///   </para>
///   <para>
///   Will detect all single digit transcription errors, all or nearly all two 
///   digit transposition errors, all or nearly all jump transposition errors,
///   all or nearly all circular shift errors and a high proportion of double 
///   digit transcription errors (two separate single digit transcription errors
///   in the same value).
///   </para>
/// </remarks>
public class Iso7064Mod11_2Algorithm : Iso7064PureSystemSingleCharacterAlgorithm, ISingleCheckDigitAlgorithm
{
   /// <summary>
   ///   Initialize a new <see cref="Iso7064Mod11_2Algorithm"/>
   /// </summary>
   public Iso7064Mod11_2Algorithm() : base(11, 2, CharacterDomains.DigitsSupplementary) { }

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod11_2AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod11_2AlgorithmName;
}
