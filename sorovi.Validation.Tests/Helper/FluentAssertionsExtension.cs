using FluentAssertions;
using FluentAssertions.Specialized;
using sorovi.Validation.Exceptions;

namespace sorovi.Validation.Tests.Helper
{
    public static class FluentAssertionsExtension
    {
        public static ExceptionAssertions<ValidationException> WithType(this ExceptionAssertions<ValidationException> exceptionAssertions, string type)
        {
            exceptionAssertions.And.Type.Should().Be(type);
            return exceptionAssertions;
        }
    }
}