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
        private string property = "some_content";

        [Benchmark(Description = "ThrowOn(() => property)")]
        public ArgumentInfo<string> PropertyGetterOnly() =>
            ThrowOn(() => property)
                .IfNull();

        [Benchmark(Description = "ThrowOn(() => property, nameof(property))")]
        public ArgumentInfo<string> PropertyGetterWithMemberName() =>
            ThrowOn(() => property, nameof(property))
                .IfNull();

        [Benchmark(Description = "ThrowOn(property, nameof(property))")]
        public ArgumentInfo<string> WithoutPropertyGetter() =>
            ThrowOn(property, nameof(property))
                .IfNull();

        [Benchmark]
        public string Classic()
        {
            if (property is null) { throw new ValidationException(ValidationTypes.ValueNull, ""); }

            return property;
        }
    }
}