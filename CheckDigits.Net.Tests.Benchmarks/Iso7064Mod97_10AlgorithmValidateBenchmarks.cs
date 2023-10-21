namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod97_10AlgorithmValidateBenchmarks
{
   private static readonly Iso7064Mod97_10Algorithm _algorithm = new();

   [Params("12345676", "163217581835191038", "1011339391255432926101144229991433338")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}