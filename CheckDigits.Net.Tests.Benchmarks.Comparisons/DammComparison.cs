// Ignore Spelling: Damm Medo

namespace CheckDigits.Net.Tests.Benchmarks.Comparisons;

using McsDamm = Medo.IO.Hashing.Damm;

[MemoryDiagnoser]
public class DammComparison
{
   private static ICheckDigitAlgorithm _baseline = Algorithms.Damm;

   [Params("1402", "1406622", "1406625388", "1406625380422", "1406625380425518", "1406625380425510280", "1406625380425510282654")]
   public String Value { get; set; } = default!;

   [Benchmark(Baseline = true)]
   public Boolean CheckDigitsDotNet() => _baseline.Validate(Value);

   [Benchmark]
   public Boolean MedoChecksums() => McsDamm.ValidateHash(Value);

}
