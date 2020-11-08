using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;

namespace sorovi.Validation
{
    public delegate string ResultExceptionHandler(in string type, in string message, in string memberName, in object value);

    public class ResultArgumentInfo<TValue> : ArgumentInfoBase<TValue, ResultExceptionHandler>
    {
        public override TValue Value { get; }
        public override string MemberName { get; }
        public override string ErrorMessage => errorMessages.ToString();
        protected override ResultExceptionHandler InnerExceptionHandler { get; }

        private StringBuilder errorMessages;

        public ResultArgumentInfo(in TValue value, in string memberName, in ResultExceptionHandler exceptionHandler = null)
        {
            Value = value;
            MemberName = memberName;
            InnerExceptionHandler = exceptionHandler;
        }

        public override ArgumentInfoBase<TValue, ResultExceptionHandler> WithExceptionHandler(in ResultExceptionHandler exceptionHandler) =>
            new ResultArgumentInfo<TValue>(Value, MemberName, exceptionHandler);

        internal override ArgumentInfoBase<TNewValue, ResultExceptionHandler> New<TNewValue>(in TNewValue value, in string memberName) =>
            new ResultArgumentInfo<TNewValue>(value, memberName, InnerExceptionHandler);

        public override void ExceptionHandler(in string type, in string message)
        {
            var result = (InnerExceptionHandler ?? ValidationExceptionHandler)(type, message, MemberName, Value);

            if (string.IsNullOrWhiteSpace(result)) return;
            if (!HasErrors) { HasErrors = true; }

            errorMessages ??= new StringBuilder();
            errorMessages.AppendLine(result);
        }

        private static string ValidationExceptionHandler(in string type, in string message, in string memberName, in object value) =>
            message;
    }
}