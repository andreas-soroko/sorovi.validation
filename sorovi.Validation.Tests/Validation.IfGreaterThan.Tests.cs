using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public class ValidationIfGreaterThan
    {
        private static object[][] _ifGreaterThan =
        {
            new object[] { 0, 0, false },
            new object[] { 0, 1, false },
            new object[] { 1, 0, true },
            new object[] { -1, -2, true },
        };

        private static object[][] _ifGreaterThanNullable = _ifGreaterThan.Concat(
                new[]
                {
                    new object[] { null, 1, false },
                    new object[] { null, -1, false },
                }
            )
            .ToArray();

        private static object[][] _ifGreaterOrEqualsThan = new object[][]
        {
            new object[] { 0, 0, true },
            new object[] { 0, 1, false },
            new object[] { 1, 0, true },
            new object[] { -1, -2, true },
            new object[] { -1, -1, true },
        };

        [TestCaseSource(nameof(_ifGreaterThan))]
        public void IfGreaterThan(int value, int value2, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfGreaterThan(value2);

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationTypes.IfGreaterThan); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifGreaterThanNullable))]
        public void IfGreaterThanNullable(int? value, int value2, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfGreaterThan(value2);

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationTypes.IfGreaterThan); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifGreaterOrEqualsThan))]
        public void IfGreaterOrEqualsThan(int value, int value2, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfGreaterOrEqualsThan(value2);

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationTypes.IfGreaterOrEqualsThan); }
            else { a.Should().NotThrow(); }
        }
    }
}