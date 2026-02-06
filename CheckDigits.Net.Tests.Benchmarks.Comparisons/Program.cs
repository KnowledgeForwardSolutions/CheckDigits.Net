using BenchmarkDotNet.Running;

using CheckDigits.Net.Tests.Benchmarks.Comparisons;

BenchmarkRunner.Run<CheckDigitAttributeComparisons>();

//BenchmarkRunner.Run<FluentValidationComparisons>();

//BenchmarkRunner.Run<LuhnComparisons>(); 

//BenchmarkRunner.Run<DammComparison>();

//BenchmarkRunner.Run<Isbn10Comparisons>();

//BenchmarkRunner.Run<Isbn13Comparisons>();
