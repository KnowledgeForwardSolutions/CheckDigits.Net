namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod661_26AlgorithmTryCalculateBenchmarks
{
   private static readonly Iso7064Mod661_26Algorithm _algorithm = new();

   [Params("ISOHJ", "ABCDEFGHIJKLMN", "AAAEEEIIIOOOUUUBCDEF", "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZ")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var first, out var second);
   }
}
