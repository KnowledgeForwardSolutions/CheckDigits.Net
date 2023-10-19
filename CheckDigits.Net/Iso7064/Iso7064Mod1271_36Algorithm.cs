namespace CheckDigits.Net.Iso7064;

/// <summary>
///   ISO/IEC 7064 MOD 1271-36 check digit algorithm. Pure system algorithm with
///   modulus 1271 and radix 36.
/// </summary>
/// <remarks>
///   <para>
///   Valid characters are alphanumeric characters (0-9, A-Z).
///   </para>
///   <para>
///   Check characters calculated by the algorithm are alphanumeric characters 
///   (0-9, A-Z).
///   </para>
///   <para>
///   Assumes that the check characters (if present) is the two right-most 
///   characters in the input value.
///   </para>
///   <para>
///   Will detect all single character transcription errors, all or nearly all 
///   two character transposition errors, all or nearly all jump transposition 
///   errors, all or nearly all circular shift errors and a high proportion of 
///   double character transcription errors (two separate single character 
///   transcription errors in the same value).
///   </para>
/// </remarks>
public class Iso7064Mod1271_36Algorithm : Iso7064PureSystemDoubleCharacterAlgorithm, IDoubleCheckDigitAlgorithm
{
   public Iso7064Mod1271_36Algorithm() : base (1271, 36, "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ") { }

   /// <inheritdoc/>
   public String AlgorithmDescription => Resources.Iso7064Mod1271_36AlgorithmDescription;

   /// <inheritdoc/>
   public String AlgorithmName => Resources.Iso7064Mod1271_36AlgorithmName;

   public override Int32 MapCharacterToNumber(Char ch) => "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".IndexOf(ch);
}
