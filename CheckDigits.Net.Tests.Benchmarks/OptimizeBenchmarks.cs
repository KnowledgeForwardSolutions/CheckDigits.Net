namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly NcdAlgorithm _algorithm = new();

   [Params("11404/2h9m", "11404/2h9tqb0", "11404/2h9tqbxk6d", "11404/2h9tqbxk6rw74", "11404/2h9tqbxk6rw7dwmz")]
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
   //public void Startup() => _algorithm.TryCalculateCheckDigit(TryValue, out var ch);

   //[Params("11404/2h9", "11404/2h9tqb", "11404/2h9tqbxk6", "11404/2h9tqbxk6rw7", "11404/2h9tqbxk6rw7dwm")]
   //public String TryValue { get; set; } = String.Empty;

   //[Benchmark(Baseline = true)]
   //public void TryCalculate() => _ = _algorithm.TryCalculateCheckDigit(TryValue, out var ch);

   //[Benchmark]
   //public void TryCalculate2() => _ = _algorithm.TryCalculateCheckDigit2(TryValue, out var ch);
}
