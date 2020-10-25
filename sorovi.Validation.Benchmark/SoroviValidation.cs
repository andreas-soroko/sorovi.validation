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

        [Benchmark]
        public ArgumentInfo<string> PropertyGetterOnly() =>
            ThrowOn(() => property)
                .IfNull();

        [Benchmark]
        public ArgumentInfo<string> PropertyGetterWithMemberName() =>
            ThrowOn(() => property, nameof(property))
                .IfNull();

        [Benchmark]
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