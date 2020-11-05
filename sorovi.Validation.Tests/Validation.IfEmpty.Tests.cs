using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;


namespace sorovi.Validation.Tests
{
    public class ValidationIfEmptyTests
    {
        private static object[][] _ifEmptyTestCases =
        {
            new object[] {null, true},
            new object[] {"", true},
            new object[] {"test", false},
            new object[] {" ", false},
            new object[] {new string[] { }, true},
            new object[] {new string[] {"test"}, false},
            new object[] {new Dictionary<string, string>(), true},
        };

        private static object[][] _ifNotEmptyTestCases = _ifEmptyTestCases.InverseBool();

        [TestCaseSource(nameof(_ifEmptyTestCases))]
        public void IfEmpty(object value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfEmpty();

            if (shouldThrow) { a.Should().Throw<ValidationException>(); }
            else { a.Should().NotThrow(); }
        }

        [TestCaseSource(nameof(_ifNotEmptyTestCases))]
        public void IfNotEmpty(object value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNotEmpty();

            if (shouldThrow) { a.Should().Throw<ValidationException>(); }
            else { a.Should().NotThrow(); }
        }

        [Test]
        public void IfEmpty_Should_Throw_NotSupport_When_A_Not_Processable_Type_Was_Passed()
        {
            int value = 0;

            Action a = () =>
                ThrowOn(() => value)
                    .IfEmpty();

            a.Should().Throw<NotSupportedException>();
        }
    }
}