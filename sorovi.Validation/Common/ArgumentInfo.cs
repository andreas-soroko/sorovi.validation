using System;
using System.Text;

namespace sorovi.Validation.Common
{
    public abstract class ArgumentInfo<TValue>
    {
        public bool HasErrors => ErrorMessages?.Length > 0;
        public abstract string ErrorMessage { get; }
        public abstract TValue Value { get; }
        public abstract string MemberName { get; }
        public abstract void ExceptionHandler(in string type, in string message);
        protected abstract ExceptionHandler InnerExceptionHandler { get; }
        protected StringBuilder ErrorMessages { get; set; }

        public abstract ArgumentInfo<TValue> WithExceptionHandler(in ExceptionHandler exceptionHandler);

        public static implicit operator TValue(in ArgumentInfo<TValue> arg) =>
            arg.Value;

        internal abstract ArgumentInfo<TNewValue> New<TNewValue>(in TNewValue value, in string memberName);
    }
}