namespace AnnotationsDemoApi.Models;

public class Iso7064Mod27_26Request
{
   [Required, Iso7064Mod27_26CheckDigit]
   public String Mod27_26Identifier { get; set; } = null!;
}

public class Iso7064Mod27_26RequestCustomErrorMessage
{
   [Required, Iso7064Mod27_26CheckDigit(ErrorMessage = "Mod27_26Identifier check digit error")]
   public String Mod27_26Identifier { get; set; } = null!;
}

public class Iso7064Mod27_26RequestGlobalizedErrorMessage
{
   [Required, Iso7064Mod27_26CheckDigit(ErrorMessageResourceName = "InvalidMod27_26Identifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String Mod27_26Identifier { get; set; } = null!;
}
