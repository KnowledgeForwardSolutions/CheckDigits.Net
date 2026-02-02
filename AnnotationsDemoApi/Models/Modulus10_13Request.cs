namespace AnnotationsDemoApi.Models;

public class Modulus10_13Request
{
   [Required, CheckDigit<Modulus10_13Algorithm>]
   public String UpcCode { get; set; } = null!;
}

public class Modulus10_13RequestCustomErrorMessage
{
   [Required, CheckDigit<Modulus10_13Algorithm>(ErrorMessage = "Invalid UPC Code")]
   public String UpcCode { get; set; } = null!;
}

public class Modulus10_13RequestGlobalizedErrorMessage
{
   [Required, CheckDigit<Modulus10_13Algorithm>(ErrorMessageResourceName = "InvalidUpcCode", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String UpcCode { get; set; } = null!;
}
