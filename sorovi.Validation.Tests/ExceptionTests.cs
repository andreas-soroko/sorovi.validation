using System;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Tests.Exceptions;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public class ExceptionTests
    {
        private class TestClass
        {
            public string MyString { get; set; }
        }

        [Test]
        public void Should_Be_Possible_To_Overide_Exception()
        {
            string test = null;

            Action a = () => ThrowOn(() => test)
                .WithException((in string type, in string message, in string name, object value) => new TestException())
                .IfNull();

            a.Should().Throw<TestException>();
        }

        [Test]
        public void Should_Be_Possible_To_Overide_Exception_With_Member_Call()
        {
            var testClass = new TestClass
            {
                MyString = "test",
            };

            Action a = () => ThrowOn(() => testClass)
                .WithException((in string type, in string message, in string name, object value) => new TestException())
                .IfNull()
                .Member(p => p.MyString, v => v.IfEqualsTo("test"));

            a.Should().Throw<TestException>();
        }
    }
}