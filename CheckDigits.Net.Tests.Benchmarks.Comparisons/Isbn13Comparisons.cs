// Ignore Spelling: Nager

namespace CheckDigits.Net.Tests.Benchmarks.Comparisons;

[MemoryDiagnoser]
public class Isbn13Comparisons
{
   private static ICheckDigitAlgorithm _baseline = Algorithms.Modulus10_13;

   [Params("9780500516959", "9781861978769", "9780691161730")]
   public String Value { get; set; } = default!;

   [Benchmark(Baseline = true)]
   public Boolean CheckDigitsDotNet() => _baseline.Validate(Value);

   [Benchmark]
   public Boolean NagerArticleNumber() => Nager.ArticleNumber.ArticleNumberHelper.IsValidIsbn13(Value);
}
