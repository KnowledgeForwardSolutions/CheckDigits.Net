namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly CusipAlgorithm _algorithm = new();

   [Params("037833100", "38143VAA7", "91282CJL6")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Startup() => _algorithm.Validate(Value);

   [Benchmark(Baseline = true)]
   public void Validate() => _ = _algorithm.Validate(Value);

   //[Benchmark]
   //public void Validate2() => _ = _algorithm.Validate2(Value);

   //[Benchmark]
   //public void Validate3() => _ = _algorithm.Validate3(Value);

   //[Benchmark]
   //public void Startup() => _algorithm.TryCalculateCheckDigits(TryValue, out var ch, out var ch2);

   //[Params("U7y", "U7y8SX", "U7y8SXrC0", "U7y8SXrC0O3S", "U7y8SXrC0O3Sc4I", "U7y8SXrC0O3Sc4IHYQ", "U7y8SXrC0O3Sc4IHYQF4M")]
   //public String TryValue { get; set; } = String.Empty;

   //[Benchmark(Baseline = true)]
   //public void TryCalculate() => _ = _algorithm.TryCalculateCheckDigits(TryValue, out var ch, out var ch2);

   //[Benchmark]
   //public void TryCalculate2() => _ = _algorithm.TryCalculateCheckDigits2(TryValue, out var ch, out var ch2);
}
