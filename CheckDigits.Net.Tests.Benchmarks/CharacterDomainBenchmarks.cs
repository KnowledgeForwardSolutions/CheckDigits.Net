namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class CharacterDomainBenchmarks
{
   private readonly DigitsSupplementaryDomain _domain = new();
   
   [Params('0', '5', '9')]
   public Char Ch { get; set; }

   [Benchmark(Baseline = true)]
   public void TryGetValue()
   {
      _ = _domain.TryGetValue(Ch, out var value);
   }

   //[Benchmark]
   //public void TryGetValueSearch()
   //{
   //   _ = _domain.TryGetValueSearch(Ch, out var value);
   //}
}
