using System;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Exceptions;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public class ThrowOnTests
    {
        [Test]
        public void ThrowOn_WithConstantExpression_Should_Throw_NotSupported()
        {
            Action a = () => ThrowOn(() => this);

            a.Should().Throw<NotSupportedException>();
        }
    }
}