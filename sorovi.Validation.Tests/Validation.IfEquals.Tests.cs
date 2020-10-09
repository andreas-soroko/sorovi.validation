using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using static sorovi.Validation.Validation;


namespace sorovi.Validation.Tests
{
    public partial class ValidationTest
    {
        static object[] _ifEqualsTestCases =
        {
            new object[] {0, 0, true},
            new object[] {0, 1, false},
            new object[] {"", "", true},
            new object[] {"", "asd", false},
            new object[] {new string[] { }, new string[] { }, false},
            new object[] {new Dictionary<string, string>(), new Dictionary<string, string>(), false},
            new object[] {null, null, true},
        };


        [TestCaseSource(nameof(_ifEqualsTestCases))]
        public void IfEquals(object value, object compareValue, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfEquals(compareValue);

            if (shouldThrow)
            {
                a.Should().Throw<Exception>();
            }
            else
            {
                a.Should().NotThrow();
            }
        }

        [TestCaseSource(nameof(_ifEqualsTestCases))]
        public void IfNotEquals(object value, object compareValue, bool shouldThrowOnIfNull) // todo hacki
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNotEquals(compareValue);

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