// Ignore Spelling: Damm

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class DammBenchmarks
{
   private readonly DammAlgorithm _sut = new();

   [Params("140", "140662", "140662538", "140662538042", "140662538042551", "140662538042551028", "140662538042551028265")]
   //[Params("1402", "1406622", "1406625388", "1406625380422", "1406625380425518", "1406625380425510280", "1406625380425510282654")]
   public String Value { get; set; }

   //[Benchmark(Baseline = true)]
   //public void Baseline() => _ = _sut.Validate(Value);

   //[Benchmark]
   //public void V2() => _ = _sut.Validate2(Value);

   //[Benchmark(Baseline = true)]
   //public void Baseline() => _ = _sut.TryCalculateCheckDigit(Value, out var checkDigit);

   //[Benchmark]
   //public void V2() => _ = _sut.TryCalculateCheckDigit2(Value, out var checkDigit);
}
