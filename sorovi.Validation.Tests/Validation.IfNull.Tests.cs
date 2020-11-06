using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public class ValidationIfNullTests
    {
        private static object[][] _ifNullTestCases =
        {
            new object[] { 0, false },
            new object[] { "", false },
            new object[] { new string[] { }, false },
            new object[] { new Dictionary<string, string>(), false },
            new object[] { null, true },
        };

        private static object[][] _ifNotNullTestCases = _ifNullTestCases.InverseBool();

        [TestCaseSource(nameof(_ifNullTestCases))]
        public void IfNull(object value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNull();

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationTypes.ValueNull); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifNotNullTestCases))]
        public void IfNotNull(object value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNotNull();

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationTypes.ValueNotNull); }
            else { a.Should().NotThrow(); }
        }
    }
}