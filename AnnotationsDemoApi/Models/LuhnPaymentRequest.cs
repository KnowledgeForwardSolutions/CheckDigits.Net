// Ignore Spelling: Luhn

namespace AnnotationsDemoApi.Models;

public class LuhnPaymentRequest
{
   [LuhnCheckDigit]
   public String CardNumber { get; set; } = null!;
}

public class LuhnErrorMessagePaymentRequest
{
   [LuhnCheckDigit(ErrorMessage = "CardNumber check digit error")]
   public String CardNumber { get; set; } = null!;
}

public class LuhnGlobalPaymentRequest
{
   [LuhnCheckDigit(ErrorMessageResourceName = "InvalidCardNumber", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String CardNumber { get; set; } = null!;
}

