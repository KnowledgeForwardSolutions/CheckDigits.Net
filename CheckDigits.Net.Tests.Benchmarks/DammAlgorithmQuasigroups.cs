// Ignore Spelling: Damm Quasigroup

namespace CheckDigits.Net.Tests.Benchmarks;

public class DammQuasigroupOrder10 : IDammQuasigroup
{
   // 2D array flattened to 1D. Gives ~30% performance improvement vs 2D.
   private static readonly Int32[] _quasigroupTable =
   [
      0, 3, 1, 7, 5, 9, 8, 6, 4, 2,
      7, 0, 9, 2, 1, 5, 4, 8, 6, 3,
      4, 2, 0, 6, 8, 7, 1, 3, 5, 9,
      1, 7, 5, 0, 9, 8, 3, 4, 2, 6,
      6, 1, 2, 3, 0, 4, 5, 9, 7, 8,
      3, 6, 7, 4, 2, 0, 9, 5, 8, 1,
      5, 8, 6, 9, 7, 2, 0, 1, 3, 4,
      8, 9, 4, 5, 3, 6, 2, 0, 1, 7,
      9, 4, 3, 8, 6, 1, 7, 2, 0, 5,
      2, 5, 8, 1, 4, 3, 6, 7, 9, 0,
   ];

   public Int32 this[Int32 interim, Int32 next] => _quasigroupTable[(interim * 10) + next];

   public Int32 Order => 10;

   public Char GetCheckCharacter(Int32 interim) => interim.ToDigitChar();

   public Int32 MapCharacter(Char ch) => ch.ToIntegerDigit();
}

public static class DammQuasigroupOrder16
{
   public static DammCustomQuasigroup GetQuasigroup()
      => new(
         new Char[,]
         {
            { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' },
            { 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E' },
            { 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D' },
            { 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C' },
            { 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B' },
            { 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A' },
            { 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' },
            { '9', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8' },
            { '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7' },
            { '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6' },
            { '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5' },
            { '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4' },
            { '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3' },
            { '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2' },
            { '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1' },
            { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', '0' },
         },
         MapCharacter,
         GetCheckCharacter);

   public static Int32 MapCharacter(Char ch) => ch switch
   {
      var d when ch >= Chars.DigitZero && ch <= Chars.DigitNine => d.ToIntegerDigit(),
      var c when ch >= Chars.UpperCaseA && ch <= Chars.UpperCaseF => c - Chars.UpperCaseA + 10,
      _ => -1
   };

   public static Char GetCheckCharacter(Int32 interim) => interim switch
   {
      var d when interim >= 0 && interim <= 9 => d.ToDigitChar(),
      var c when interim >= 10 && interim <= 15 => (Char)(c - 10 + Chars.UpperCaseA),
      _ => Chars.NUL
   };
}