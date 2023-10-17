//   _ = _domain.TryGetValue(Ch, out var value);
using CheckDigits.Net.Utility;

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class CharacterDomainBenchmarks
{
   private readonly AlphanumericSupplementaryDomain _alphanumeric = new();

   private readonly DigitsSupplementaryDomain _domain = new();

   //[Params('0', '5', '9')]
   //public Char Ch { get; set; }

   //[Params("07940", "000000012095650X", "0000000109302468")]
   [Params("ZZZZO", "A999914123456N", "A999522123456", "ABCDEFGHIJKLMNOPQRSTUVWX*")]
   public String Value { get; set; } = String.Empty;

   //[Benchmark(Baseline = true)]
   //public void TryGetValue()
   //{
   //   _ = _domain.TryGetValue(Ch, out var value);
   //}

   //[Benchmark]
   //public void TryGetValueSearch()
   //{
   //   _ = _domain.TryGetValueSearch(Ch, out var value);
   //}

   [Benchmark(Baseline = true)]
   public void Original()
   {
      for (var index = 0; index < Value.Length - 2; index++)
      {
         _ = _domain.MapCharacterToNumber(Value[index]);
      }
      _ = _domain.MapCheckCharacterToNumber(Value[^1]);
   }

   //[Benchmark]
   //public void Basic()
   //{
   //   for (var index = 0; index < Value.Length - 1; index++)
   //   {
   //      var num = Value[index].ToIntegerDigit();
   //   }
   //}
}
