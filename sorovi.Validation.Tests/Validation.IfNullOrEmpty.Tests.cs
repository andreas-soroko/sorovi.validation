using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using static sorovi.Validation.Validation;


namespace sorovi.Validation.Tests
{
    public class ValidationIfNullOrEmptyTests
    {
        static object[] _ifNullOrEmptyTestCases =
        {
            new object[] {null, true},
            new object[] {"", true},
            new object[] {"test", false},
            new object[] {" ", true},
        };

        [TestCaseSource(nameof(_ifNullOrEmptyTestCases))]
        public void IfNullOrEmpty(string value, bool shouldThrow)
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNullOrWhiteSpace();

            if (shouldThrow)
            {
                a.Should().Throw<Exception>();
            }
            else
            {
                a.Should().NotThrow();
            }
        }
        
        [TestCaseSource(nameof(_ifNullOrEmptyTestCases))]
        public void IfNotNullOrEmpty(string value, bool shouldThrowOnIfEmpty) // todo hacki
        {
            Action a = () =>
                ThrowOn(() => value)
                    .IfNotNullOrWhiteSpace();

            if (!shouldThrowOnIfEmpty)
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