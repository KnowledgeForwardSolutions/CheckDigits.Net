namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod97_10AlgorithmTryCalculateBenchmarks
{
   private static readonly Iso7064Mod97_10Algorithm _algorithm = new();

   [Params("123456", "1632175818351910", "10113393912554329261011442299914333")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var first, out var second);
   }
}
