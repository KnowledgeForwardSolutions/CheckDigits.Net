namespace AnnotationsDemoApi.Models;

public class Iso7064Mod37_2Request
{
   [Required, Iso7064Mod37_2CheckDigit]
   public String DonationIdentifier { get; set; } = null!;
}

public class Iso7064Mod37_2RequestCustomErrorMessage
{
   [Required, Iso7064Mod37_2CheckDigit(ErrorMessage = "DonationIdentifier check digit error")]
   public String DonationIdentifier { get; set; } = null!;
}

public class Iso7064Mod37_2RequestGlobalizedErrorMessage
{
   [Required, Iso7064Mod37_2CheckDigit(ErrorMessageResourceName = "InvalidDonationIdentifier", ErrorMessageResourceType = typeof(Resources.SharedStrings))]
   public String DonationIdentifier { get; set; } = null!;
}
