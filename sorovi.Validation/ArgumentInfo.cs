using System;
using sorovi.Validation.Exceptions;

namespace sorovi.Validation
{
    public delegate void ExceptionHandler(in string type, in string message, in string memberName, in object value);
    public delegate void ExceptionHandlerInternal(in string type, in string message);

    public readonly struct ArgumentInfo<TValue>
    {
        public TValue Value { get; }

        internal string MemberName { get; }

        private readonly ExceptionHandler innerExceptionHandler;

        public ArgumentInfo(in TValue value, in string memberName, in ExceptionHandler exceptionHandler = null)
        {
            Value = value;
            MemberName = memberName;
            innerExceptionHandler = exceptionHandler;
        }

        public ArgumentInfo<TValue> WithExceptionHandler(in ExceptionHandler exceptionHandler) =>
            new ArgumentInfo<TValue>(Value, MemberName, exceptionHandler);

        internal void ExceptionHandler(in string type, in string message)
        {
            (innerExceptionHandler ?? CreateValidationException)(type, message, MemberName, Value);
        }

        internal ArgumentInfo<TNewValue> New<TNewValue>(in TNewValue value, in string memberName) =>
            new ArgumentInfo<TNewValue>(value, memberName, innerExceptionHandler);

        private static void CreateValidationException(in string type, in string message, in string memberName, in object value) =>
            throw new ValidationException(type, message);

        public static implicit operator TValue(in ArgumentInfo<TValue> arg) =>
            arg.Value;
    }
}