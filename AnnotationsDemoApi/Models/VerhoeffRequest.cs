// Ignore Spelling: Aadhaar, Verhoeff

namespace AnnotationsDemoApi.Models;

public class VerhoeffRequest
{
   [VerhoeffCheckDigit]
   public String AadhaarIdNumber { get; set; } = null!;
}

public class VerhoeffRequestCustomErrorMessage
{
   [VerhoeffCheckDigit(ErrorMessage = "Aadhaar number check digit error")]
   public String AadhaarIdNumber { get; set; } = null!;
}

public class VerhoeffRequestGlobalizedErrorMessage
{
   [VerhoeffCheckDigit(ErrorMessageResourceName = "InvalidAadhaarNumber", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String AadhaarIdNumber { get; set; } = null!;
}
