using System.Collections.Generic;
using System.Collections.ObjectModel;
using FluentAssertions;
using NUnit.Framework;

namespace sorovi.Validation.Tests
{
    public class ValidationCommonTests
    {
        [Test]
        public void As_Should_Cast_Values()
        {
            IReadOnlyDictionary<string, string> dic = new ReadOnlyDictionary<string, string>(
                new Dictionary<string, string>()
                {
                    { "k1", "v1" },
                    { "k2", null },
                }
            );

            var result = Validation
                .ResultOn(dic, nameof(dic))
                .Every(item => item.IfNull());

            result.HasErrors.Should().BeTrue();
            result.ErrorMessage.Should().Be("Expected 'dic[k2]' not to be <null>");
        }
    }
}