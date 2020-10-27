using FluentAssertions;
using NUnit.Framework;
using static sorovi.Validation.Validation;


namespace sorovi.Validation.Tests
{
    public class ThrowOnTests
    {
        [Test]
        public void ThrowOn_PropertyGetter_MemberName_On_This_Should_Be_Empty()
        {
            var argInfo = ThrowOn(() => this);

            argInfo.MemberName.Should().Be("");
        }
    }
}