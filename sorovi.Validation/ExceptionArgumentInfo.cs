using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;

namespace sorovi.Validation
{
    public delegate string ExceptionHandler(in string type, in string message, in string memberName, in object value);

    public class ExceptionArgumentInfo<TValue> : ArgumentInfo<TValue>
    {
        public override TValue Value { get; }
        public override string MemberName { get; }
        public override string ErrorMessage { get; } = null;
        protected override ExceptionHandler InnerExceptionHandler { get; }

        public ExceptionArgumentInfo(in TValue value, in string memberName, in ExceptionHandler exceptionHandler = null)
        {
            Value = value;
            MemberName = memberName;
            InnerExceptionHandler = exceptionHandler;
        }

        public override ArgumentInfo<TValue> WithExceptionHandler(in ExceptionHandler exceptionHandler) =>
            new ExceptionArgumentInfo<TValue>(Value, MemberName, exceptionHandler);

        internal override ArgumentInfo<TNewValue> New<TNewValue>(in TNewValue value, in string memberName) =>
            new ExceptionArgumentInfo<TNewValue>(value, memberName, InnerExceptionHandler);

        public override void ExceptionHandler(in string type, in string message)
        {
            (InnerExceptionHandler ?? ValidationExceptionHandler)(type, message, MemberName, Value);
        }

        private static string ValidationExceptionHandler(in string type, in string message, in string memberName, in object value) =>
            throw new ValidationException(type, message);
    }
}