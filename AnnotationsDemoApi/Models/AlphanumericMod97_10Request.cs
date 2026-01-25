namespace AnnotationsDemoApi.Models;

public class AlphanumericMod97_10Request
{
   [AlphanumericMod97_10CheckDigit]
   public String LegalEntityIdentifier { get; set; } = null!;
}

public class AlphanumericMod97_10RequestCustomErrorMessage
{
   [AlphanumericMod97_10CheckDigit(ErrorMessage = "Legal Entity Identifier check digit error")]
   public String LegalEntityIdentifier { get; set; } = null!;
}

public class AlphanumericMod97_10RequestGlobalizedErrorMessage
{
   [AlphanumericMod97_10CheckDigit(ErrorMessageResourceName = "InvalidLegalEntityIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String LegalEntityIdentifier { get; set; } = null!;
}
