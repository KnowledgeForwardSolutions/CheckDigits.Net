// Ignore Spelling: Damm

namespace AnnotationsDemoApi.Models;

public class DammRequest
{
   [DammCheckDigit]
   public String SubmissionIdentifier { get; set; } = null!;
}

public class DammRequestCustomErrorMessage
{
   [DammCheckDigit(ErrorMessage = "Submission Identifier check digit error")]
   public String SubmissionIdentifier { get; set; } = null!;
}

public class DammRequestGlobalizedErrorMessage
{
   [DammCheckDigit(ErrorMessageResourceName = "InvalidSubmissionIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String SubmissionIdentifier { get; set; } = null!;
}
