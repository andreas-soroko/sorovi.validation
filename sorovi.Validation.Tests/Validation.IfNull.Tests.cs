using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using static sorovi.Validation.Validation;


namespace sorovi.Validation.Tests
{
    public partial class ValidationTest
    {
        static object[] _ifNullTestCases =
        {
            new object[] {0, false},
            new object[] {"", false},
            new object[] {new string[] { }, false},
            new object[] {new Dictionary<string, string>(), false},
            new object[] {null, true},
        };


        [TestCaseSource(nameof(_ifNullTestCases))]
        public void IfNull(object value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNull();

            if (shouldThrow)
            {
                a.Should().Throw<Exception>();
            }
            else
            {
                a.Should().NotThrow();
            }
        }

        [TestCaseSource(nameof(_ifNullTestCases))]
        public void IfNotNull(object value, bool shouldThrowOnIfNull) // todo hacki
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNotNull();

            if (!shouldThrowOnIfNull)
            {
                a.Should().Throw<Exception>();
            }
            else
            {
                a.Should().NotThrow();
            }
        }
    }
}