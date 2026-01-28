namespace AnnotationsDemoApi.Models;

public class Iso7064Mod11_2Request
{
   [Required, Iso7064Mod11_2CheckDigit]
   public String StandardNameIdentifier { get; set; } = null!;
}

public class Iso7064Mod11_2RequestCustomErrorMessage
{
   [Required, Iso7064Mod11_2CheckDigit(ErrorMessage = "ISNI check digit error")]
   public String StandardNameIdentifier { get; set; } = null!;
}

public class Iso7064Mod11_2RequestGlobalizedErrorMessage
{
   [Required, Iso7064Mod11_2CheckDigit(ErrorMessageResourceName = "InvalidStandardNameIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String StandardNameIdentifier { get; set; } = null!;
}
