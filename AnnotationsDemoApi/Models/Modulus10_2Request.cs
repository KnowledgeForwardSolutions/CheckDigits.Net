namespace AnnotationsDemoApi.Models;

public class Modulus10_2Request
{
   [Modulus10_2CheckDigit]
   public String ImoNumber { get; set; } = null!;
}

public class Modulus10_2RequestCustomErrorMessage
{
   [Modulus10_2CheckDigit(ErrorMessage = "IMO Number check digit error")]
   public String ImoNumber { get; set; } = null!;
}

public class Modulus10_2RequestGlobalizedErrorMessage
{
   [Modulus10_2CheckDigit(ErrorMessageResourceName = "InvalidImoNumber", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String ImoNumber { get; set; } = null!;
}
