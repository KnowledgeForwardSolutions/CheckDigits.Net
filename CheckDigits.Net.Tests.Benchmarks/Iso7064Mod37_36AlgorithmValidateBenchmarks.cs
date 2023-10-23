namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod37_36AlgorithmValidateBenchmarks
{
   private static readonly Iso7064Mod37_36Algorithm _algorithm = new();

   [Params("AEIOUU", "QWERTYDVORAK1", "A1B2C3D4E5F6G7H8I9J0KI", "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZT")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}
