// Ignore Spelling: Luhn

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

