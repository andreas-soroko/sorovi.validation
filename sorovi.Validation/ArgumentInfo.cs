using System;
using System.Collections.Generic;
using sorovi.Validation.Exceptions;

namespace sorovi.Validation
{
    public delegate Exception CreateExceptionDelegate<in T>(in string type, in string message, in string memberName, T value);

    public readonly struct ArgumentInfo<TValue>
    {
        public TValue Value { get; }
        internal string MemberName { get; }
        internal CreateExceptionDelegate<object> CreateException { get; }

        public ArgumentInfo(in TValue value, in string memberName, in CreateExceptionDelegate<object> createExceptionDelegate = null)
        {
            Value = value;
            MemberName = memberName;
            CreateException = createExceptionDelegate ?? CreateValidationException;
        }

        public ArgumentInfo<TValue> WithValidationException() => WithException(CreateValidationException);

        public ArgumentInfo<TValue> WithException(in CreateExceptionDelegate<object> createExceptionDelegate) =>
            new ArgumentInfo<TValue>(this.Value, this.MemberName, createExceptionDelegate);

        internal ArgumentInfo<TValue> ThrowIf(in bool predicate, in string type, in string message)
        {
            if (!predicate) return this;

            throw CreateException(type, message, this.MemberName, this.Value);
        }

        private static Exception CreateValidationException<T>(in string type, in string message, in string memberName, T value) => new ValidationException(type, message);

        public static implicit operator TValue(in ArgumentInfo<TValue> arg) => arg.Value;
    }
}