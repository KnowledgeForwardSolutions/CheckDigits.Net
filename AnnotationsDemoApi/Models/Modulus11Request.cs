namespace AnnotationsDemoApi.Models;

public class Modulus11Request
{
   [Required, Modulus11CheckDigit]
   public String Isbn { get; set; } = null!;
}

public class Modulus11RequestCustomErrorMessage
{
   [Required, Modulus11CheckDigit(ErrorMessage = "ISBN check digit error")]
   public String Isbn { get; set; } = null!;
}

public class Modulus11RequestGlobalizedErrorMessage
{
   [Required, Modulus11CheckDigit(ErrorMessageResourceName = "InvalidIsbn10", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String Isbn { get; set; } = null!;
}
