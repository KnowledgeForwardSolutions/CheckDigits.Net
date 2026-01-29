namespace AnnotationsDemoApi.Models;

public class Iso7064Mod97_10Request
{
   [Required, Iso7064Mod97_10CheckDigit]
   public String Mod97_10Identifier { get; set; } = null!;
}

public class Iso7064Mod97_10RequestCustomErrorMessage
{
   [Required, Iso7064Mod97_10CheckDigit(ErrorMessage = "Mod97_10Identifier check digit error")]
   public String Mod97_10Identifier { get; set; } = null!;
}

public class Iso7064Mod97_10RequestGlobalizedErrorMessage
{
   [Required, Iso7064Mod97_10CheckDigit(ErrorMessageResourceName = "InvalidMod97_10Identifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String Mod97_10Identifier { get; set; } = null!;
}
