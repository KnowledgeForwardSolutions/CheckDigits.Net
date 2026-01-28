// Ignore Spelling: Aadhaar, Verhoeff

namespace AnnotationsDemoApi.Models;

public class VerhoeffRequest
{
   [Required, VerhoeffCheckDigit]
   public String AadhaarIdNumber { get; set; } = null!;
}

public class VerhoeffRequestCustomErrorMessage
{
   [Required, VerhoeffCheckDigit(ErrorMessage = "Aadhaar number check digit error")]
   public String AadhaarIdNumber { get; set; } = null!;
}

public class VerhoeffRequestGlobalizedErrorMessage
{
   [Required, VerhoeffCheckDigit(ErrorMessageResourceName = "InvalidAadhaarNumber", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String AadhaarIdNumber { get; set; } = null!;
}
