// Ignore Spelling: Customizable

namespace CheckDigits.Net.Tests.Benchmarks;

//[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
//[SimpleJob(RuntimeMoniker.Net10_0)]
[MemoryDiagnoser]
public class CustomizableAlgorithmBenchmarks
{
   private static readonly DammCustomQuasigroupAlgorithm _dammAlgorithmOrder10 = new(new DammQuasigroupOrder10());
   private static readonly DammCustomQuasigroupAlgorithm _dammAlgorithmOrder16 = new(DammQuasigroupOrder16.GetQuasigroup());
   private static readonly ICheckDigitMask _groupsOfThreeMask = new GroupsOfThreeCheckDigitMask();

   public IEnumerable<Object[]> TryCalculateCheckDigitArguments()
   {
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140662"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140662538"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140662538042"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140662538042551"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140662538042551028"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140662538042551028265"];

      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2ED"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C1C3"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C1C34F4"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C1C34F4DA5"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C1C34F4DA55F3"];
   }

   public IEnumerable<Object[]> ValidateArguments()
   {
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "1402"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "1406622"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "1406625388"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "1406625380422"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "1406625380425518"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "1406625380425510280"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "1406625380425510282654"];

      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2ED1"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15F"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C5"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C1C33"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C1C34F46"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C1C34F4DA52"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2EDC15B3C1C34F4DA55F37"];
   }

   public IEnumerable<Object[]> ValidateMaskedArguments()
   {
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140 2"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140 662 2"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140 662 538 8"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140 662 538 042 2"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140 662 538 042 551 8"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140 662 538 042 551 028 0"];
      yield return [_dammAlgorithmOrder10, _dammAlgorithmOrder10.AlgorithmName, "140 662 538 042 551 028 265 4"];

      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2ED 1"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2ED C15 F"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2ED C15 B3C 5"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2ED C15 B3C 1C3 3"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2ED C15 B3C 1C3 4F4 6"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2ED C15 B3C 1C3 4F4 DA5 2"];
      yield return [_dammAlgorithmOrder16, _dammAlgorithmOrder16.AlgorithmName, "2ED C15 B3C 1C3 4F4 DA5 5F3 7"];
   }

   [Benchmark]
   [ArgumentsSource(nameof(TryCalculateCheckDigitArguments))]
   public void TryCalculateCheckDigit(ISingleCheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.TryCalculateCheckDigit(value, out var checkDigit);
   }

   [Benchmark]
   [ArgumentsSource(nameof(ValidateArguments))]
   public void Validate(ICheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.Validate(value);
   }

   [Benchmark]
   [ArgumentsSource(nameof(ValidateMaskedArguments))]
   public void ValidateMasked(IMaskedCheckDigitAlgorithm algorithm, String name, String value)
   {
      algorithm.Validate(value, _groupsOfThreeMask);
   }
}
