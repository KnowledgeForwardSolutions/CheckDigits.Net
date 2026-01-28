// Ignore Spelling: Noid

namespace AnnotationsDemoApi.Models;

public class NoidRequest
{
   [Required, NoidCheckDigit]
   public String ArkIdentifier { get; set; } = null!;
}

public class NoidRequestCustomErrorMessage
{
   [Required, NoidCheckDigit(ErrorMessage = "ARK Identifier check digit error")]
   public String ArkIdentifier { get; set; } = null!;
}

public class NoidRequestGlobalizedErrorMessage
{
   [Required, NoidCheckDigit(ErrorMessageResourceName = "InvalidArkIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String ArkIdentifier { get; set; } = null!;
}
