namespace AnnotationsDemoApi.Models;

public class Iso7064Mod11_10Request
{
   [Iso7064Mod11_10CheckDigit]
   public String ItemIdentifier { get; set; } = null!;
}

public class Iso7064Mod11_10RequestCustomErrorMessage
{
   [Iso7064Mod11_10CheckDigit(ErrorMessage = "Item Identifier check digit error")]
   public String ItemIdentifier { get; set; } = null!;
}

public class Iso7064Mod11_10RequestGlobalizedErrorMessage
{
   [Iso7064Mod11_10CheckDigit(ErrorMessageResourceName = "InvalidItemIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String ItemIdentifier { get; set; } = null!;
}

