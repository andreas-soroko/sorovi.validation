using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using static sorovi.Validation.Validation;


namespace sorovi.Validation.Tests
{
    public class ValidationMemberTests
    {
        private class MemberTestClass
        {
            public string Member { get; set; }

            public ArgumentInfo<MemberTestClass> GetArgInfo() => ThrowOn(() => this);
        }

        

        [Test]
        public void Member_Empty()
        {
            var value = new MemberTestClass();

            Action a = () =>
                ThrowOn(() => value)
                    .IfNull()
                    .Member(m => m.Member, arg => arg.IfEmpty());


            a.Should().Throw<Exception>();
        }

        [Test]
        public void Member_Not_Empty()
        {
            var value = new MemberTestClass()
            {
                Member = "not_empty"
            };

            Action a = () =>
                ThrowOn(() => value)
                    .IfNull()
                    .Member(m => m.Member, arg => arg.IfEmpty());


            a.Should().NotThrow();
        }
    }
}