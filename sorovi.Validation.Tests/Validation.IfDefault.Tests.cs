using System;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public class ValidationIfDefaultTests
    {
        [TestCase]
        public void IfDefault_Int_Should_Throw()
        {
            var value = 0;

            Action a = () =>
                ThrowOn(() => value)
                    .IfDefault();

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfDefault);
        }

        [TestCase]
        public void IfDefault_Int_Should_Not_Throw()
        {
            var value = 1;

            Action a = () =>
                ThrowOn(() => value)
                    .IfDefault();

            a.Should().NotThrow();
        }

        [TestCase]
        public void IfDefault_String_Should_Throw()
        {
            string value = null;

            Action a = () =>
                ThrowOn(() => value)
                    .IfDefault();

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfDefault);
        }

        [TestCase]
        public void IfDefault_String_Should_Not_Throw()
        {
            string value = "";

            Action a = () =>
                ThrowOn(() => value)
                    .IfDefault();

            a.Should().NotThrow();
        }

        [TestCase]
        public void IfNotDefault_Int_Should_Throw()
        {
            var value = 1;

            Action a = () =>
                ThrowOn(() => value)
                    .IfNotDefault();

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfNotDefault);
        }

        [TestCase]
        public void IfNotDefault_Int_Should_Not_Throw()
        {
            var value = 0;

            Action a = () =>
                ThrowOn(() => value)
                    .IfNotDefault();

            a.Should().NotThrow();
        }

        [TestCase]
        public void IfNotDefault_String_Should_Throw()
        {
            string value = "";

            Action a = () =>
                ThrowOn(() => value)
                    .IfNotDefault();

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfNotDefault);
        }

        [TestCase]
        public void IfNotDefault_String_Should_Not_Throw()
        {
            string value = null;

            Action a = () =>
                ThrowOn(() => value)
                    .IfNotDefault();

            a.Should().NotThrow();
        }
    }
}