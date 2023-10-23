namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod37_36AlgorithmTryCalculateBenchmarks
{
   private static readonly Iso7064Mod37_36Algorithm _algorithm = new();

   [Params("AEIOU", "QWERTYDVORAK", "A1B2C3D4E5F6G7H8I9J0K", "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void TryCalculateCheckDigit()
   {
      _ = _algorithm.TryCalculateCheckDigit(Value, out var checkDigit);
   }
}
