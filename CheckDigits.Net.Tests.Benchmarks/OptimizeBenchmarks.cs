#pragma warning disable CS0618 // Type or member is obsolete

namespace CheckDigits.Net.Tests.Benchmarks;

[MemoryDiagnoser]
public class OptimizeBenchmarks
{
   private static readonly Icao9303SizeTD2Algorithm _algorithm = new();

   [Benchmark(Baseline = true)]
   [Arguments("I<UTOERIKSSON<<ANNA<MARIA<<<<<<<<<<<D231458907UTO7408122F1204159<<<<<<<6")]
   [Arguments("I<UTOQWERTY<<ASDF<<<<<<<<<<<<<<<<<<<\r\nD23145890<UTO7408122F1204159AB1124<4")]
   [Arguments("I<UTOSKYWALKER<<LUKE<<<<<<<<<<<<<<<<\nSTARWARS45UTO7705256M2405252<<<<<<<4")]
   public void OriginalVersion(String value) => _ = _algorithm.Validate(value);

   //[Benchmark()]
   //[Arguments("1406")]
   //[Arguments("1406620")]
   //[Arguments("1406625385")]
   //[Arguments("1406625380421")]
   //[Arguments("1406625380425510")]
   //[Arguments("1406625380425510288")]
   //[Arguments("1406625380425510282650")]
   //public void UpdatedVersion(String value) => _ = _algorithm.Validate(value);

}