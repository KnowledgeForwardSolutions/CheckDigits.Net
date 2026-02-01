namespace CheckDigits.Net.DataAnnotations;

public class Iso7065HybridCheckDigitAttribute<TAlgorithm>()
   : BaseCheckDigitAttribute(new TAlgorithm(), Messages.SingleCheckDigitFailure)
   where TAlgorithm : Iso7064HybridSystemAlgorithm, new()
{
}
