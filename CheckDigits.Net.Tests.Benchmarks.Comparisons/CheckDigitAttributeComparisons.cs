// Ignore Spelling: Luhn 

namespace CheckDigits.Net.Tests.Benchmarks.Comparisons;

[MemoryDiagnoser]
public class CheckDigitAttributeComparisons
{
   public static readonly CreditCardAttribute _creditCardAttribute = new CreditCardAttribute();

   public static readonly CheckDigitAttribute<LuhnAlgorithm> _luhnCheckDigitAttribute = new CheckDigitAttribute<LuhnAlgorithm>();

   public static readonly MaskedCheckDigitAttribute<LuhnAlgorithm, CreditCardMask> _maskedLuhnCheckDigitAttribute =
      new MaskedCheckDigitAttribute<LuhnAlgorithm, CreditCardMask>();

   [Benchmark(Baseline = true)]
   [Arguments("4012888888881881")]
   [Arguments("4012 8888 8888 1881")]
   [Arguments("4012-8888-8888-1881")]
   [Arguments("4012 8888-8888 1881")]
   public void CreditCardAttribute(String cardNumber) => _creditCardAttribute.IsValid(cardNumber);

   [Benchmark]
   [Arguments("4012888888881881")]
   public void LuhnCheckDigitAttribute(String cardNumber) => _luhnCheckDigitAttribute.IsValid(cardNumber);

   [Benchmark]
   [Arguments("4012 8888 8888 1881")]
   [Arguments("4012-8888-8888-1881")]
   [Arguments("4012 8888-8888 1881")]
   public void MaskedLuhnCheckDigitAttribute(String cardNumber) => _maskedLuhnCheckDigitAttribute.IsValid(cardNumber);

   public class CreditCardMask : ICheckDigitMask
   {
      public Boolean ExcludeCharacter(Int32 index) => (index + 1) % 5 == 0;

      public Boolean IncludeCharacter(Int32 index) => (index + 1) % 5 != 0;
   }
}
