namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod11_10AlgorithmValidateBenchmarks
{
   private static readonly Iso7064Mod11_10Algorithm _algorithm = new();

   [Params("07945", "123456788", "16321758183519103", "123456789012345678901234565")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
