using System;
using BenchmarkDotNet.Attributes;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Benchmark
{
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class SoroviValidationBenchmark
    {
        private static string property = "some_content";

        [Benchmark(Description = "Classic - if (property is null)")]
        public string Classic()
        {
            if (property is null) { throw new ArgumentNullException(nameof(property)); }

            return property;
        }

        [Benchmark(Description = "ThrowOn(() => property)")]
        public ExceptionArgumentInfo<string> CtorPropertyGetter() =>
            ThrowOn(() => property);

        [Benchmark(Description = "ThrowOn(property, nameof(property))")]
        public ExceptionArgumentInfo<string> CtorWithoutPropertyGetter() =>
            ThrowOn(property, nameof(property));

        [Benchmark(Description = "ThrowOn(() => property).IfNull")]
        public ArgumentInfoBase<string, ExceptionHandler> PropertyGetterIfNull() =>
            ThrowOn(() => property)
                .IfNull();

        [Benchmark(Description = "ThrowOn(property, nameof(property)).IfNull")]
        public ArgumentInfoBase<string, ExceptionHandler> WithoutPropertyGetter() =>
            ThrowOn(property, nameof(property))
                .IfNull();
    }
}