using System;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Exceptions;
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

            if (shouldThrow) { a.Should().Throw<ValidationException>(); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifLowerOrEqualsThan))]
        public void IfLowerOrEqualsThan(int value, int value2, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfLowerOrEqualsThan(value2);

            if (shouldThrow) { a.Should().Throw<ValidationException>(); }
            else { a.Should().NotThrow(); }
        }
    }
}