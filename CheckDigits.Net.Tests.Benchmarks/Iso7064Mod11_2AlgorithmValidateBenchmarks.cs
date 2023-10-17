namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod11_2AlgorithmValidateBenchmarks
{
   private static readonly Iso7064Mod11_2Algorithm _algorithm = new();

   [Params("07940", "000000012095650X", "0000000109302468", "999999999999999999999999999999999994")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
