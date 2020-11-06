using System;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public class ValidationIfTests
    {
        [TestCase("", "", true)]
        [TestCase("", "test", false)]
        [TestCase("test", "", false)]
        public void If(string value, string expected, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .If(v => v == expected);

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationTypes.ValueIf); }
            else { a.Should().NotThrow(); }
        }

        [TestCase("", "", false)]
        [TestCase("", "test", true)]
        [TestCase("test", "", true)]
        public void IfNot(string value, string expected, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNot(v => v == expected);

            if (shouldThrow) { a.Should().Throw<ValidationException>().WithType(ValidationTypes.ValueIfNot); }
            else { a.Should().NotThrow(); }
        }
    }
}