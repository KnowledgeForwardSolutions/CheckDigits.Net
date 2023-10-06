namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Modulus11AlgorithmValidateBenchmarks
{
   private static readonly Modulus11Algorithm _algorithm = new();

   [Params("1235", "03178471", "050027293X")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
