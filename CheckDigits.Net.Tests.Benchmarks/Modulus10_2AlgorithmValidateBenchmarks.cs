namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Modulus10_2AlgorithmValidateBenchmarks
{
   private static readonly Modulus10_1Algorithm _algorithm = new();

   [Params("9074729", "9707792", "1010569")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
