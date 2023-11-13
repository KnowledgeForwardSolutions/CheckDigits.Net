// Ignore Spelling: Noid

using System.Text;

namespace CheckDigits.Net.Tests.Unit.TestData;

/// <summary>
///   Tests to generate data used by benchmarks.
/// </summary>
public class BenchmarkData
{
   private readonly ITestOutputHelper _outputHelper;
   private readonly List<Int32> _lengths = new() { 3, 6, 9, 12, 15, 18, 21 };

   public BenchmarkData(ITestOutputHelper outputHelper) => _outputHelper = outputHelper;

   [Fact]
   public void BenchmarkData_GenerateNumericData()
   {
      var numericData = String.Concat(Enumerable.Range(0, 26)
         .Select(x => Random.Shared.Next(0, 9).ToString()));
         
      _outputHelper.WriteLine(numericData);

      // 14066253804255102826542671
   }

   [Fact]
   public void BenchmarkData_GenerateAlphabeticData()
   {
      var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

      var alphabeticData = String.Concat(Enumerable.Range(0, 26)
         .Select(x => letters[Random.Shared.Next(0, 25)].ToString()));

      _outputHelper.WriteLine(alphabeticData);

      // EGRNMLJOCECUJIKNWWVVOYNTDM
   }

   [Fact]
   public void BenchmarkData_GenerateAlphanumericData()
   {
      var chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

      var alphabeticData = String.Concat(Enumerable.Range(0, 26)
         .Select(x => chars[Random.Shared.Next(0, 35)].ToString()));

      _outputHelper.WriteLine(alphabeticData);

      // K1MEL37655H24EDKCA69ID433B
      // U7Y8SXRC0O3SC4IHYQF4MYY6XF  updated after only including first 26 characters
   }

   [Fact]
   public void BenchmarkData_GenerateNoidData()
   {
      var digits = "0123456789";
      var chars = "0123456789bcdfghjkmnpqrstvwxz";

      var naanData = String.Concat(Enumerable.Range(0, 5)
         .Select(x => digits[Random.Shared.Next(0, 9)].ToString()));
      var name = String.Concat(Enumerable.Range(0, 20)
         .Select(x => chars[Random.Shared.Next(0, 28)].ToString()));

      _outputHelper.WriteLine($"{naanData}/{name}");

      // 11404/2h9tqbxk6rw7dwmq73g3
   }

   [Fact]
   public void BenchmarkData_NumericAlgorithms()
   {
      const String digits = "14066253804255102826542671";

      var algorithms = new List<ISingleCheckDigitAlgorithm>()
      {
         Algorithms.Damm,
         Algorithms.Iso7064Mod11_10,
         Algorithms.Iso7064Mod11_2,
         Algorithms.Luhn,
         Algorithms.Modulus10_1,
         Algorithms.Modulus10_2,
         Algorithms.Modulus10_13,
         Algorithms.Modulus11,
         Algorithms.Verhoeff
      };

      String details;

      foreach (var algorithm in algorithms)
      {
         details = SingleCharacterValues(algorithm, digits);
         _outputHelper.WriteLine(details);
         _outputHelper.WriteLine(String.Empty);
      }

      details = DoubleCharacterValues(Algorithms.Iso7064Mod97_10, digits);
      _outputHelper.WriteLine(details);
      _outputHelper.WriteLine(String.Empty);
   }

   [Fact]
   public void BenchmarkData_AlphabeticAlgorithms()
   {
      const String chars = "EGRNMLJOCECUJIKNWWVVOYNTDM";

      var details = SingleCharacterValues(Algorithms.Iso7064Mod27_26, chars);
      _outputHelper.WriteLine(details);
      _outputHelper.WriteLine(String.Empty);

      details = DoubleCharacterValues(Algorithms.Iso7064Mod661_26, chars);
      _outputHelper.WriteLine(details);
      _outputHelper.WriteLine(String.Empty);
   }

   [Fact]
   public void BenchmarkData_AlphanumericAlgorithms()
   {
      const String chars = "U7Y8SXRC0O3SC4IHYQF4MYY6XF";
      const String noid = "11404/2h9tqbxk6rw7dwmq73g3";

      var details = DoubleCharacterValues(Algorithms.AlphanumericMod97_10, chars);
      _outputHelper.WriteLine(details);
      _outputHelper.WriteLine(String.Empty);

      details = DoubleCharacterValues(Algorithms.Iso7064Mod1271_36, chars);
      _outputHelper.WriteLine(details);
      _outputHelper.WriteLine(String.Empty);

      details = SingleCharacterValues(Algorithms.Iso7064Mod37_2, chars);
      _outputHelper.WriteLine(details);
      _outputHelper.WriteLine(String.Empty);

      details = SingleCharacterValues(Algorithms.Iso7064Mod37_36, chars);
      _outputHelper.WriteLine(details);
      _outputHelper.WriteLine(String.Empty);

      details = SingleCharacterValues(Algorithms.Ncd, noid);
      _outputHelper.WriteLine(details);
      _outputHelper.WriteLine(String.Empty);
   }

   private String SingleCharacterValues(
      ISingleCheckDigitAlgorithm algorithm,
      String chars)
   {
      var sb = new StringBuilder();
      sb.Append(algorithm.AlgorithmName);
      sb.Append(' ');
      foreach (var length in _lengths)
      {
         var substr = chars[..length];
         algorithm.TryCalculateCheckDigit(substr, out var checkDigit);

         sb.Append('"');
         sb.Append(substr + checkDigit);
         sb.Append('"');
         sb.Append(", ");
      }

      return sb.ToString();
   }

   private String DoubleCharacterValues(
      IDoubleCheckDigitAlgorithm algorithm,
      String chars)
   {
      var sb = new StringBuilder();
      sb.Append(algorithm.AlgorithmName);
      sb.Append(' ');
      foreach (var length in _lengths)
      {
         var substr = chars[..length];
         algorithm.TryCalculateCheckDigits(substr, out var first, out var second);

         sb.Append('"');
         sb.Append(substr + first + second);
         sb.Append('"');
         sb.Append(", ");
      }

      return sb.ToString();
   }
}
