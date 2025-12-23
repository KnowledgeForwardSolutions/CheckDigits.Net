namespace CheckDigits.Net.Tests.Benchmarks;

public class GroupsOfThreeCheckDigitMask : ICheckDigitMask
{
   public Boolean ExcludeCharacter(Int32 index) => (index + 1) % 4 == 0;

   public Boolean IncludeCharacter(Int32 index) => (index + 1) % 4 != 0;
}
