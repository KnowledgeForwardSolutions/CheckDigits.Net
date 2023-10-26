namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class ValueSpecificAlgorithmBenchmarks
{   public IEnumerable<Object[]> TryCalculateCheckDigitArguments()
   {
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "US037833100" };
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "AU0000XVGZA" };
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "GB000263494" };

      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1M8GDM9A_KP042788" };
      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1G8ZG127_WZ157259" };
      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1HGEM212_2L047875" };
   }

   public IEnumerable<Object[]> ValidateArguments()
   {
      yield return new Object[] { Algorithms.AbaRtn, Algorithms.AbaRtn.AlgorithmName, "111000025" };
      yield return new Object[] { Algorithms.AbaRtn, Algorithms.AbaRtn.AlgorithmName, "122235821" };
      yield return new Object[] { Algorithms.AbaRtn, Algorithms.AbaRtn.AlgorithmName, "325081403" };

      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "US0378331005" };
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "AU0000XVGZA3" };
      yield return new Object[] { Algorithms.Isin, Algorithms.Isin.AlgorithmName, "GB0002634946" };

      yield return new Object[] { Algorithms.Nhs, Algorithms.Nhs.AlgorithmName, "9434765919" };
      yield return new Object[] { Algorithms.Nhs, Algorithms.Nhs.AlgorithmName, "4505577104" };
      yield return new Object[] { Algorithms.Nhs, Algorithms.Nhs.AlgorithmName, "5301194917" };

      yield return new Object[] { Algorithms.Npi, Algorithms.Npi.AlgorithmName, "1234567893" };
      yield return new Object[] { Algorithms.Npi, Algorithms.Npi.AlgorithmName, "1245319599" };
      yield return new Object[] { Algorithms.Npi, Algorithms.Npi.AlgorithmName, "1122337797" };

      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1M8GDM9AXKP042788" };
      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1G8ZG127XWZ157259" };
      yield return new Object[] { Algorithms.Vin, Algorithms.Vin.AlgorithmName, "1HGEM21292L047875" };
   }

   [Benchmark]
   [ArgumentsSource(nameof(TryCalculateCheckDigitArguments))]
   public void TryCalculateCheckDigit(ISingleCheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.TryCalculateCheckDigit(value, out var checkDigit);
   }

   //[Benchmark]
   //[ArgumentsSource(nameof(ValidateArguments))]
   //public void Validate(ICheckDigitAlgorithm algorithm, String name, String value)
   //{
   //   algorithm.Validate(value);
   //}
}
