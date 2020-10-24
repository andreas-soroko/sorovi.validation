using System;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Tests.Exceptions;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public class ExceptionTests
    {
        [Test]
        public void Should_Be_Possible_To_Overide_Exception()
        {
            string test = null;

            Action a = () => ThrowOn(() => test)
                .WithException((in string type, in string message, in string name, string value) => new TestException())
                .IfNull();

            a.Should().Throw<TestException>();
        }
    }
}