// Ignore Spelling: Iban

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class IbanBenchmarks
{
   private readonly IbanAlgorithm _algorithm = new IbanAlgorithm();

   [Params("GB82WEST12345698765432", "BE71096123456769", "DO22ACAU00000000000123456789", "SC74MCBL01031234567890123456USD")]
   public String Value { get; set; } = default!;

   [Benchmark(Baseline = true)]
   public void Baseline()
   {
      _ = _algorithm.Validate(Value);
   }
}
