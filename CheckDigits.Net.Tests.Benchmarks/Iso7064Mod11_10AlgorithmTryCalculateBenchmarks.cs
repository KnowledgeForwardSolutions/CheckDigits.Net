namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod11_10AlgorithmTryCalculateBenchmarks
{
   private static readonly Iso7064Mod11_10Algorithm _algorithm = new();

   [Params("0794", "12345678", "1632175818351910", "12345678901234567890123456")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
