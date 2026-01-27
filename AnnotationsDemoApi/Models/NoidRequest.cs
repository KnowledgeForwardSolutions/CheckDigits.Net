// Ignore Spelling: Noid

namespace AnnotationsDemoApi.Models;

public class NoidRequest
{
   [NoidCheckDigit]
   public String ArkIdentifier { get; set; } = null!;
}

public class NoidRequestCustomErrorMessage
{
   [NoidCheckDigit(ErrorMessage = "ARK Identifier check digit error")]
   public String ArkIdentifier { get; set; } = null!;
}

public class NoidRequestGlobalizedErrorMessage
{
   [NoidCheckDigit(ErrorMessageResourceName = "InvalidArkIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String ArkIdentifier { get; set; } = null!;
}
