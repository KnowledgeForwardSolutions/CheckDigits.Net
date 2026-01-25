// Ignore Spelling: Luhn

namespace AnnotationsDemoApi.Models;

public class LuhnRequest
{
   [LuhnCheckDigit]
   public String CardNumber { get; set; } = null!;
}

public class LuhnRequestCustomErrorMessage
{
   [LuhnCheckDigit(ErrorMessage = "CardNumber check digit error")]
   public String CardNumber { get; set; } = null!;
}

public class LuhnRequestGlobalizedErrorMessage
{
   [LuhnCheckDigit(ErrorMessageResourceName = "InvalidCardNumber", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String CardNumber { get; set; } = null!;
}

