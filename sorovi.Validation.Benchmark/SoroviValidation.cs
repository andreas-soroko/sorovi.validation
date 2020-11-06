using BenchmarkDotNet.Attributes;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Benchmark
{
    [MemoryDiagnoser]
    [ThreadingDiagnoser]
    public class SoroviValidation
    {
        private static string property = "some_content";

        [Benchmark(Description = "ThrowOn(() => property).IfNull")]
        public ArgumentInfo<string> PropertyGetterOnly() =>
            ThrowOn(() => property)
                .IfNull();

        [Benchmark(Description = "ThrowOn(property, nameof(property)).IfNull")]
        public ArgumentInfo<string> WithoutPropertyGetter() =>
            ThrowOn(property, nameof(property))
                .IfNull();

        [Benchmark(Description = "Classic - if (property is null)")]
        public string Classic()
        {
            if (property is null) { throw new ValidationException(ValidationTypes.ValueNull, ""); }

            return property;
        }
    }
}