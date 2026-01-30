namespace AnnotationsDemoApi.Utility;

public class CreditCardMask : ICheckDigitMask
{
   public Boolean ExcludeCharacter(Int32 index) => (index + 1) % 5 == 0;

   public Boolean IncludeCharacter(Int32 index) => (index + 1) % 5 != 0;
}
