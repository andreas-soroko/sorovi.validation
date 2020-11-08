using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public class ValidationIfEmptyTests
    {
        private static object[][] _ifEmptyStringTestCases =
        {
            new object[] { null, true },
            new object[] { "", true },
            new object[] { "test", false },
            new object[] { " ", false }
        };

        private static object[][] _ifEmptyGuidTestCases =
        {
            new object[] { Guid.Empty, true },
            new object[] { Guid.NewGuid(), false },
        };

        private static object[][] _ifEmptyIEnumerableTestCases =
        {
            new object[] { new string[] { }, true },
            new object[] { new string[] { "test" }, false },
            new object[] { new Dictionary<string, string>(), true },
        };

        private static object[][] _ifNotEmptyStringTestCases = _ifEmptyStringTestCases.InverseBool();
        private static object[][] _ifNotEmptyGuidTestCases = _ifEmptyGuidTestCases.InverseBool();
        private static object[][] _ifNotEmptyIEnumarableTestCases = _ifEmptyIEnumerableTestCases.InverseBool();

        [TestCaseSource(nameof(_ifEmptyStringTestCases))]
        public void IfEmpty_String(string value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfEmpty();

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifEmptyGuidTestCases))]
        public void IfEmpty_Guid(Guid value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfEmpty();

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifEmptyIEnumerableTestCases))]
        public void IfEmpty_IEnumerable(IEnumerable value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfEmpty();

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifNotEmptyStringTestCases))]
        public void IfNotEmpty_String(string value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNotEmpty();

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationType.IfNotEmpty); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifNotEmptyGuidTestCases))]
        public void IfNotEmpty_Guid(Guid value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNotEmpty();

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationType.IfNotEmpty); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifNotEmptyIEnumarableTestCases))]
        public void IfNotEmpty_IEnumerable(IEnumerable value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNotEmpty();

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationType.IfNotEmpty); }
            else { a.Should().NotThrow(); }
        }
    }
}