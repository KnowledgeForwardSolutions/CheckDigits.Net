// Ignore Spelling: Nager

namespace CheckDigits.Net.Tests.Benchmarks.Comparisons;

[MemoryDiagnoser]
public class Isbn10Comparisons
{
   private static ICheckDigitAlgorithm _baseline = Algorithms.Modulus11;

   [Params("1568656521", "0714105449", "050027293X")]
   public String Value { get; set; } = default!;

   [Benchmark(Baseline = true)]
   public Boolean CheckDigitsDotNet() => _baseline.Validate(Value);

   [Benchmark]
   public Boolean NagerArticleNumber() => Nager.ArticleNumber.ArticleNumberHelper.IsValidIsbn10(Value);
}
