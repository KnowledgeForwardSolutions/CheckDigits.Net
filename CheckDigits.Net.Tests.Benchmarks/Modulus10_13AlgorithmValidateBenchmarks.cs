namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Modulus10_13AlgorithmValidateBenchmarks
{
   private static readonly Modulus10_13Algorithm _algorithm = new();

   [Params("425261", "73513537", "036000291452", "4006381333931", "012345678000045678")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
