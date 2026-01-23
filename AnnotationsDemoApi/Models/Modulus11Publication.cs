namespace AnnotationsDemoApi.Models;

public class Modulus11Publication
{
   [Modulus11CheckDigit]
   public String Isbn { get; set; } = null!;
}

public class Modulus11PublicationCustomErrorMessage
{
   [Modulus11CheckDigit(ErrorMessage = "ISBN check digit error")]
   public String Isbn { get; set; } = null!;
}

public class Modulus11PublicationGlobalizedErrorMessage
{
   [Modulus11CheckDigit(ErrorMessageResourceName = "InvalidIsbn10", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String Isbn { get; set; } = null!;
}
