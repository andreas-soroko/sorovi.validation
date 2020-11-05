using System;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;


namespace sorovi.Validation.Tests
{
    public class ValidationIfNullOrEmptyTests
    {
        private static object[][] _ifNullOrEmptyTestCases =
        {
            new object[] {null, true},
            new object[] {"", true},
            new object[] {"test", false},
            new object[] {" ", true},
        };

        private static object[][] _ifNotNullOrEmptyTestCases = _ifNullOrEmptyTestCases.InverseBool();

        [TestCaseSource(nameof(_ifNullOrEmptyTestCases))]
        public void IfNullOrEmpty(string value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNullOrWhiteSpace();

            if (shouldThrow) { a.Should().Throw<ValidationException>(); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifNotNullOrEmptyTestCases))]
        public void IfNotNullOrEmpty(string value, bool shouldThrow) // todo hacki
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNotNullOrWhiteSpace();

            if (shouldThrow) { a.Should().Throw<ValidationException>(); }
            else { a.Should().NotThrow(); }
        }
    }
}