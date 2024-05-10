namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly IbanAlgorithm _algorithm = new();

   [Params("BE71096123456769", "GB82WEST12345698765432", "SC74MCBL01031234567890123456USD")]
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

   //[Params("BE00096123456769", "GB00WEST12345698765432", "SC00MCBL01031234567890123456USD")]
   //public String TryValue { get; set; } = String.Empty;

   //[Benchmark(Baseline = true)]
   //public void TryCalculate() => _ = _algorithm.TryCalculateCheckDigits(TryValue, out var ch, out var ch2);

   //[Benchmark]
   //public void TryCalculate2() => _ = _algorithm.TryCalculateCheckDigits2(TryValue, out var ch, out var ch2);
}
