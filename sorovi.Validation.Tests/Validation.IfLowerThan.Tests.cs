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
    public class ValidationIfLowerThan
    {
        private static object[][] _ifLowerThan =
        {
            new object[] { 0, 0, false },
            new object[] { 0, 1, true },
            new object[] { 1, 0, false },
            new object[] { -1, -2, false },
        };

        private static object[][] _ifLowerThanNullable = _ifLowerThan.Concat(
                new[]
                {
                    new object[] { null, 1, true },
                    new object[] { null, -1, true },
                }
            )
            .ToArray();

        private static object[][] _ifLowerOrEqualsThan = new object[][]
        {
            new object[] { 0, 0, true },
            new object[] { 0, 1, true },
            new object[] { 1, 0, false },
            new object[] { -1, -2, false },
            new object[] { -1, -1, true },
        };

        [TestCaseSource(nameof(_ifLowerThan))]
        public void IfLowerThan(int value, int value2, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfLowerThan(value2);

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationType.IfLowerThan); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifLowerThanNullable))]
        public void IfLowerThanNullable(int? value, int value2, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfLowerThan(value2);

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationType.IfLowerThan); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifLowerOrEqualsThan))]
        public void IfLowerOrEqualsThan(int value, int value2, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfLowerOrEqualsThan(value2);

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationType.IfLowerOrEqualsThan); }
            else { a.Should().NotThrow(); }
        }
    }
}