using System;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public partial class ResultOnTests
    {
        [Test]
        public void Should_Fill_Error_Messages()
        {
            var value = "";
            var expected = "";

            var result =
                ResultOn(() => value)
                    .If(v => v == expected)
                    .IfEmpty()
                    .IfNullOrWhiteSpace();

            result.HasErrors.Should().BeTrue();
            result.ErrorMessage.Should().NotBeNullOrEmpty();
            result.ErrorMessage.Split(Environment.NewLine).Should().HaveCount(4);
        }
    }
}