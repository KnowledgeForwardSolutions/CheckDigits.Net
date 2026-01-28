// Ignore Spelling: Damm

namespace AnnotationsDemoApi.Models;

public class DammRequest
{
   [Required, DammCheckDigit]
   public String SubmissionIdentifier { get; set; } = null!;
}

public class DammRequestCustomErrorMessage
{
   [Required, DammCheckDigit(ErrorMessage = "Submission Identifier check digit error")]
   public String SubmissionIdentifier { get; set; } = null!;
}

public class DammRequestGlobalizedErrorMessage
{
   [Required, DammCheckDigit(ErrorMessageResourceName = "InvalidSubmissionIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String SubmissionIdentifier { get; set; } = null!;
}
