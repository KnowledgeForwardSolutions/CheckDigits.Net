namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class Iso7064Mod661_26AlgorithmValidateBenchmarks
{
   private static readonly Iso7064Mod661_26Algorithm _algorithm = new();

   [Params("ISOHJTC", "ABCDEFGHIJKLMNJF", "AAAEEEIIIOOOUUUBCDEFJY", "ZZZZZZZZZZZZZZZZZZZZZZZZZZZZZZNS")]
   public String Value { get; set; } = String.Empty;

   [Benchmark]
   public void Validate()
   {
      _ = _algorithm.Validate(Value);
   }
}