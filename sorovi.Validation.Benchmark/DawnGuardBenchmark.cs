using System;
using BenchmarkDotNet.Attributes;
using Dawn;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using static Dawn.Guard;

namespace sorovi.Validation.Benchmark
{
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class DawnGuardBenchmark
    {
        private static string property = "some_content";

        [Benchmark(Description = "Argument(() => property)")]
        public Guard.ArgumentInfo<string> CtorPropertyGetter() =>
            Argument(() => property);

        [Benchmark(Description = "Argument(property, nameof(property))")]
        public Guard.ArgumentInfo<string> CtorWithoutPropertyGetter() =>
            Argument(property, nameof(property));

        [Benchmark(Description = "Argument(() => property).IfNull")]
        public Guard.ArgumentInfo<string?> PropertyGetterIfNull() =>
            Argument(() => property)
                .NotNull();

        [Benchmark(Description = "Argument(property, nameof(property)).IfNull")]
        public Guard.ArgumentInfo<string?> WithoutPropertyGetter() =>
            Argument(property, nameof(property))
                .NotNull();
    }
}