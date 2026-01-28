namespace AnnotationsDemoApi.Models;

public class Iso7064Mod1271_36Request
{
   [Required, Iso7064Mod1271_36CheckDigit]
   public String Mod1271_36Identifier { get; set; } = null!;
}

public class Iso7064Mod1271_36RequestCustomErrorMessage
{
   [Required, Iso7064Mod1271_36CheckDigit(ErrorMessage = "Mod1271_36Identifier check digit error")]
   public String Mod1271_36Identifier { get; set; } = null!;
}

public class Iso7064Mod1271_36RequestGlobalizedErrorMessage
{
   [Required, Iso7064Mod1271_36CheckDigit(ErrorMessageResourceName = "InvalidMod1271_36IdentifierIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String Mod1271_36Identifier { get; set; } = null!;
}

