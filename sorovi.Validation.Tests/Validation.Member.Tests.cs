using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using static sorovi.Validation.Validation;


namespace sorovi.Validation.Tests
{
    public partial class ValidationTest
    {
        private class MemberTestClass
        {
            public string Member { get; set; }
        }

        [Test]
        public void Member()
        {
            var value = new MemberTestClass();

            Action a = () =>
                ThrowOn(() => value)
                    .IfNull()
                    .Member(m => m.Member, arg => arg.IfEmpty());


            a.Should().Throw<Exception>();
        }
    }
}