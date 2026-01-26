// Ignore Spelling: Cas

namespace AnnotationsDemoApi.Models;

public class Modulus10_1Request
{
   [Modulus10_1CheckDigit]
   public String CasNumber { get; set; } = null!;
}

public class Modulus10_1RequestCustomErrorMessage
{
   [Modulus10_1CheckDigit(ErrorMessage = "CAS Registry Number check digit error")]
   public String CasNumber { get; set; } = null!;
}

public class Modulus10_1RequestGlobalizedErrorMessage
{
   [Modulus10_1CheckDigit(ErrorMessageResourceName = "InvalidIsbn10", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String CasNumber { get; set; } = null!;
}
