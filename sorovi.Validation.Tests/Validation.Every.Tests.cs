using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using FluentAssertions;
using NUnit.Framework;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.Tests.Helper;
using static sorovi.Validation.Validation;

namespace sorovi.Validation.Tests
{
    public partial class ValidationEveryTests
    {
        private readonly List<string> listItems = new List<string>()
        {
            " test",
            "",
            null,
        };

        private readonly Dictionary<string, string> dictionaryItems = new Dictionary<string, string>()
        {
            { "", " " },
            { "test1", null },
            { "test2", null },
            { "test3", null },
            { "test5", "" },
            { "test6", " " },
        };

        [Test]
        public void Every_List()
        {
            var value = listItems;
            Action a = () => ThrowOn(() => listItems)
                .Every(item => item.IfEmpty());

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty);
        }

        [Test]
        public void Every_IList()
        {
            var value = listItems as IList<string>;

            Action a = () => ThrowOn(() => value)
                .Every(item => item.IfEmpty());

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty);
        }

        [Test]
        public void Every_Array()
        {
            var value = listItems.ToArray();

            Action a = () => ThrowOn(() => value)
                .Every(item => item.IfEmpty());

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty);
        }

        [Test]
        public void Every_IEnumerable()
        {
            var value = listItems as IEnumerable<string>;

            Action a = () => ThrowOn(() => value)
                .Every(item => item.IfEmpty());

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty);
        }

        public void Every_Dictionary_If()
        {
            var value = dictionaryItems;

            Action a = () => ThrowOn(() => value)
                .Every(item => item.If(kv => kv.Key.StartsWith("test")));

            a.Should().Throw<ValidationException>().WithType(ValidationType.If).And.Message.Should().Be("Condition for 'value[test]' was not complied");
        }

        [Test]
        public void Every_Dictionary_KeyValuePair_IfNull()
        {
            var value = dictionaryItems;

            Action a = () => ThrowOn(() => value)
                .Every(item => item.IfNull());

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfNull).And.Message.Should().Be("Expected 'value[test1]' not to be <null>");
        }

        [Test]
        public void Every_Dictionary_Key()
        {
            var value = dictionaryItems;

            Action a = () => ThrowOn(() => value)
                .EveryKey(item => item.IfEmpty());

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty);
        }

        [Test]
        public void Every_IDictionary_Key()
        {
            var value = dictionaryItems as IDictionary<string, string>;

            Action a = () => ThrowOn(() => value)
                .EveryKey(item => item.IfEmpty());

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty);
        }

        [Test]
        public void Every_Dictionary_Value()
        {
            var value = dictionaryItems;

            Action a = () => ThrowOn(() => value)
                .EveryValue(item => item.IfEmpty());

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfEmpty);
        }

        [Test]
        public void Every_IDictionary_Value()
        {
            var value = dictionaryItems as IDictionary<string, string>;

            Action a = () => ThrowOn(() => value)
                .EveryValue(item => item.IfNullOrWhiteSpace());

            a.Should().Throw<ValidationException>().WithType(ValidationType.IfNullOrWhiteSpace);
        }
    }
}