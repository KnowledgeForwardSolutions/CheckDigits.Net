namespace CheckDigits.Net.DataAnnotations.Tests.Unit.TestData;

internal class CaSocialInsuranceNumberMask : ICheckDigitMask
{
   public Boolean ExcludeCharacter(Int32 index) => index == 3 || index == 7;

   public Boolean IncludeCharacter(Int32 index) => index != 3 && index != 7;
}

internal class CreditCardMask : ICheckDigitMask
{
   public Boolean ExcludeCharacter(Int32 index) => (index + 1) % 5 == 0;

   public Boolean IncludeCharacter(Int32 index) => (index + 1) % 5 != 0;
}

internal class RejectAllMask : ICheckDigitMask
{
   public Boolean ExcludeCharacter(Int32 index) => true;

   public Boolean IncludeCharacter(Int32 index) => false;
}
