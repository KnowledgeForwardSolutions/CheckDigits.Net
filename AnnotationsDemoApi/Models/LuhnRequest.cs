// Ignore Spelling: Luhn

using AnnotationsDemoApi.Utility;

namespace AnnotationsDemoApi.Models;

public class LuhnRequest
{
   [Required, LuhnCheckDigit]
   public String CardNumber { get; set; } = null!;
}

public class LuhnRequestCustomErrorMessage
{
   [Required, LuhnCheckDigit(ErrorMessage = "CardNumber check digit error")]
   public String CardNumber { get; set; } = null!;
}

public class LuhnRequestGlobalizedErrorMessage
{
   [Required, LuhnCheckDigit(ErrorMessageResourceName = "InvalidCardNumber", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String CardNumber { get; set; } = null!;
}

public class MaskedLuhnRequest
{
   [Required, MaskedLuhnCheckDigit<CreditCardMask>]
   public String CardNumber { get; set; } = null!;
}

public class MaskedLuhnRequestCustomErrorMessage
{
   [Required, MaskedLuhnCheckDigit<CreditCardMask>(ErrorMessage = "CardNumber check digit error")]
   public String CardNumber { get; set; } = null!;
}

public class MaskedLuhnRequestGlobalizedErrorMessage
{
   [Required, MaskedLuhnCheckDigit<CreditCardMask>(ErrorMessageResourceName = "InvalidCardNumber", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String CardNumber { get; set; } = null!;
}

