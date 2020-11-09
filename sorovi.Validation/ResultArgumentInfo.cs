using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;

namespace sorovi.Validation
{

    public class ResultArgumentInfo<TValue> : ArgumentInfo<TValue>
    {
        public override TValue Value { get; }
        public override string MemberName { get; }
        public override string ErrorMessage => ErrorMessages.ToString().Trim();
        protected override ExceptionHandler InnerExceptionHandler { get; }

        public ResultArgumentInfo(in TValue value, in string memberName, in ExceptionHandler exceptionHandler = null, in StringBuilder parentBuilder = null)
        {
            Value = value;
            MemberName = memberName;
            InnerExceptionHandler = exceptionHandler;
            ErrorMessages = parentBuilder;
        }

        public override ArgumentInfo<TValue> WithExceptionHandler(in ExceptionHandler exceptionHandler) =>
            new ResultArgumentInfo<TValue>(Value, MemberName, exceptionHandler);

        internal override ArgumentInfo<TNewValue> New<TNewValue>(in TNewValue value, in string memberName) =>
            new ResultArgumentInfo<TNewValue>(value, memberName, InnerExceptionHandler, ErrorMessages ??= new StringBuilder());

        public override void ExceptionHandler(in string type, in string message)
        {
            var result = (InnerExceptionHandler ?? ValidationExceptionHandler)(type, message, MemberName, Value);

            if (string.IsNullOrWhiteSpace(result)) return;

            ErrorMessages ??= new StringBuilder();
            ErrorMessages.AppendLine(result);
        }

        private static string ValidationExceptionHandler(in string type, in string message, in string memberName, in object value) =>
            message;
    }
}