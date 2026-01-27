namespace AnnotationsDemoApi.Models;

public class Iso7064Mod11_2Request
{
   [Iso7064Mod11_2CheckDigit]
   public String StandardNameIdentifier { get; set; } = null!;
}

public class Iso7064Mod11_2RequestCustomErrorMessage
{
   [Iso7064Mod11_2CheckDigit(ErrorMessage = "ISNI check digit error")]
   public String StandardNameIdentifier { get; set; } = null!;
}

public class Iso7064Mod11_2RequestGlobalizedErrorMessage
{
   [Iso7064Mod11_2CheckDigit(ErrorMessageResourceName = "InvalidStandardNameIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String StandardNameIdentifier { get; set; } = null!;
}
