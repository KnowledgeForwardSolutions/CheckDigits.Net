namespace AnnotationsDemoApi.Models;

public class Iso7064Mod37_36Request
{
   [Required, Iso7064Mod37_36CheckDigit]
   public String GlobalReleaseIdentifier { get; set; } = null!;
}

public class Iso7064Mod37_36RequestCustomErrorMessage
{
   [Required, Iso7064Mod37_36CheckDigit(ErrorMessage = "GlobalReleaseIdentifier check digit error")]
   public String GlobalReleaseIdentifier { get; set; } = null!;
}

public class Iso7064Mod37_36RequestGlobalizedErrorMessage
{
   [Required, Iso7064Mod37_36CheckDigit(ErrorMessageResourceName = "InvalidGlobalReleaseIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String GlobalReleaseIdentifier { get; set; } = null!;
}
