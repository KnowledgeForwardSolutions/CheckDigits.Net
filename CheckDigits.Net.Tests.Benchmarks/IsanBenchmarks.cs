// Ignore Spelling: Isan

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class IsanBenchmarks
{
   public static readonly IsanAlgorithm _mod37_36 = new();
   public static readonly IsanAlgorithm _isan = new();

   //[Params("D02C42E954183EE2Q1291C8AEO", "C594660A8B2E5D22X6DDA3272E", "E9530C32BC0EE83B269867B20F")]
   //[Params("ISAN D02C-42E9-5418-3EE2-Q-1291-C8AE-O", "ISAN C594-660A-8B2E-5D22-X-6DDA-3272-E", "ISAN E953-0C32-BC0E-E83B-2-6986-7B20-F")]
   //public String Value { get; set; }

   public IEnumerable<Object[]> ValidateArguments()
   {
      yield return new Object[] { "D02C42E954183EE2Q1291C8AEO" };
      yield return new Object[] { "C594660A8B2E5D22X6DDA3272E" };
      yield return new Object[] { "E9530C32BC0EE83B269867B20F" };
   }

   public IEnumerable<Object[]> ValidateShortFormattedArguments()
   {
      yield return new Object[] { "ISAN D02C-42E9-5418-3EE2-Q" };
      yield return new Object[] { "ISAN C594-660A-8B2E-5D22-X" };
      yield return new Object[] { "ISAN E953-0C32-BC0E-E83B-2" };
   }

   public IEnumerable<Object[]> ValidateFormattedArguments()
   {
      yield return new Object[] { "ISAN D02C-42E9-5418-3EE2-Q-1291-C8AE-O" };
      yield return new Object[] { "ISAN C594-660A-8B2E-5D22-X-6DDA-3272-E" };
      yield return new Object[] { "ISAN E953-0C32-BC0E-E83B-2-6986-7B20-F" };
   }

   [Benchmark(Baseline = true)]
   [ArgumentsSource(nameof(ValidateArguments))]
   public void Mod36_36(String value)
   {
      _ = _mod37_36.Validate(value);
   }

   [Benchmark]
   [ArgumentsSource(nameof(ValidateArguments))]
   public void Isan(String value)
   {
      _ = _isan.Validate(value);
   }

   [Benchmark]
   [ArgumentsSource(nameof(ValidateFormattedArguments))]
   public void IsanFormatted(String value)
   {
      _ = _isan.ValidateFormatted(value);
   }

   [Benchmark]
   [ArgumentsSource(nameof(ValidateShortFormattedArguments))]
   public void IsanShortFormatted(String value)
   {
      _ = _isan.ValidateFormatted(value);
   }
}
