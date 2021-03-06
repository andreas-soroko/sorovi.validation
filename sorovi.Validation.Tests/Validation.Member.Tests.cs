using System;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public class ValidationMemberTests
    {
        private class MemberTestClass
        {
            public string Member { get; set; }
        }

        [Test]
        public void Member_Empty()
        {
            var value = new MemberTestClass();

            Action a = () =>
                ThrowOn(() => value)
                    .IfNull()
                    .Member(m => m.Member, arg => arg.IfEmpty());

            a.Should()
                .Throw<ValidationException>()
                .WithType(ValidationType.IfEmpty);
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

        [Test]
        public void Member_Optional_Should_Not_Throw_When_Member_Is_Null()
        {
            var value = new MemberTestClass();

            Action a = () =>
                ThrowOn(() => value)
                    .IfNull()
                    .MemberOptional(m => m.Member, arg => arg.IfNull());

            a.Should().NotThrow();
        }

        [Test]
        public void Member_Optional_Should_Throw_When_Member_Is_WhiteSpace()
        {
            var value = new MemberTestClass()
            {
                Member = " "
            };

            Action a = () =>
                ThrowOn(() => value)
                    .IfNull()
                    .MemberOptional(m => m.Member, arg => arg.IfNullOrWhiteSpace());

            a.Should()
                .Throw<ValidationException>()
                .WithType(ValidationType.IfNullOrWhiteSpace);
        }
    }
}