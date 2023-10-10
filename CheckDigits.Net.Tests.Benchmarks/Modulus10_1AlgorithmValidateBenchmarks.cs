namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Modulus10_1AlgorithmValidateBenchmarks
{
   private static readonly Modulus10_1Algorithm _algorithm = new();

   [Params("58082", "7732185", "28728554")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
