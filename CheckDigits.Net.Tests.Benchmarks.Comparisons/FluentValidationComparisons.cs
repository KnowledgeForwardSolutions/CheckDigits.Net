// Ignore Spelling: Validator luhn

namespace CheckDigits.Net.Tests.Benchmarks.Comparisons;

[MemoryDiagnoser]
public class FluentValidationComparisons
{
   public static readonly PaymentRequest _paymentRequest = new PaymentRequest();

   public static readonly PaymentValidatorCreditCard _creditCardValidator = 
      new PaymentValidatorCreditCard();

   public static readonly PaymentValidatorLuhnCheckDigit _luhnCheckDigitValidator =
      new PaymentValidatorLuhnCheckDigit();

   public static readonly PaymentValidatorMaskedLuhnCheckDigit _maskedLuhnCheckDigitValidator =
      new PaymentValidatorMaskedLuhnCheckDigit();

   [Benchmark(Baseline = true)]
   [Arguments("4012888888881881")]
   [Arguments("4012 8888 8888 1881")]
   [Arguments("4012-8888-8888-1881")]
   [Arguments("4012 8888-8888 1881")]
   public void CreditCardValidator(String cardNumber)
   {
      _paymentRequest.CardNumber = cardNumber;

      _creditCardValidator.Validate(_paymentRequest);
   }

   [Benchmark]
   [Arguments("4012888888881881")]
   public void LuhnValidator(String cardNumber)
   {
      _paymentRequest.CardNumber = cardNumber;

      _luhnCheckDigitValidator.Validate(_paymentRequest);
   }

   [Benchmark]
   [Arguments("4012 8888 8888 1881")]
   [Arguments("4012-8888-8888-1881")]
   [Arguments("4012 8888-8888 1881")]
   public void MaskedLuhnValidator(String cardNumber)
   {
      _paymentRequest.CardNumber = cardNumber;

      _maskedLuhnCheckDigitValidator.Validate(_paymentRequest);
   }

   public class PaymentRequest
   {
      public String CardNumber { get; set; } = default!;
   }

   public class CreditCardMask : ICheckDigitMask
   {
      public Boolean ExcludeCharacter(Int32 index) => (index + 1) % 5 == 0;

      public Boolean IncludeCharacter(Int32 index) => (index + 1) % 5 != 0;
   }

   public class PaymentValidatorCreditCard : AbstractValidator<PaymentRequest>
   {
      public PaymentValidatorCreditCard()
      {
         RuleFor(x => x.CardNumber)
            .CreditCard();
      }
   }

   public class PaymentValidatorLuhnCheckDigit : AbstractValidator<PaymentRequest>
   {
      public PaymentValidatorLuhnCheckDigit()
      {
         RuleFor(x => x.CardNumber)
            .CheckDigit(Algorithms.Luhn);
      }
   }

   public class PaymentValidatorMaskedLuhnCheckDigit : AbstractValidator<PaymentRequest>
   {
      public PaymentValidatorMaskedLuhnCheckDigit()
      {
         RuleFor(x => x.CardNumber)
            .MaskedCheckDigit(new LuhnAlgorithm(), new CreditCardMask());
      }
   }
}
