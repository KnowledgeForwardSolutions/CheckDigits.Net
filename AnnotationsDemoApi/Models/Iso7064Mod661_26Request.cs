namespace AnnotationsDemoApi.Models;

public class Iso7064Mod661_26Request
{
   [Required, Iso7064Mod661_26CheckDigit]
   public String Mod661_26Identifier { get; set; } = null!;
}

public class Iso7064Mod661_26RequestCustomErrorMessage
{
   [Required, Iso7064Mod661_26CheckDigit(ErrorMessage = "Mod661_26Identifier check digit error")]
   public String Mod661_26Identifier { get; set; } = null!;
}

public class Iso7064Mod661_26RequestGlobalizedErrorMessage
{
   [Required, Iso7064Mod661_26CheckDigit(ErrorMessageResourceName = "InvalidMod661_26Identifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String Mod661_26Identifier { get; set; } = null!;
}
