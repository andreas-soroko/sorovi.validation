using System;
using BenchmarkDotNet.Attributes;
using Dawn;
using EnsureThat;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using static EnsureThat.Ensure;

namespace sorovi.Validation.Benchmark
{
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class EnsureThatBenchmark
    {
        private static string property = "some_content";

        [Benchmark(Description = "That(property, nameof(property))")]
        public StringParam CtorWithoutPropertyGetter() =>
            That(property, nameof(property));

        [Benchmark(Description = "That(property, nameof(property)).IfNull")]
        public void WithoutPropertyGetter() =>
            That(property, nameof(property))
                .IsNotNull();
    }
}