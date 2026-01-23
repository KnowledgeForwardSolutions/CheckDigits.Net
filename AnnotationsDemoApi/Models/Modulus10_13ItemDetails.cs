namespace AnnotationsDemoApi.Models;

public class Modulus10_13ItemDetails
{
   [Modulus10_13CheckDigit]
   public String UpcCode { get; set; } = null!;
}

public class Modulus10_13ItemDetailsCustomErrorMessage
{
   [Modulus10_13CheckDigit(ErrorMessage = "Invalid UPC Code")]
   public String UpcCode { get; set; } = null!;
}

public class Modulus10_13ItemDetailsCGlobalizedErrorMessage
{
   [Modulus10_13CheckDigit(ErrorMessageResourceName = "InvalidUpcCode", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String UpcCode { get; set; } = null!;
}
