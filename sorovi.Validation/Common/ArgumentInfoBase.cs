using System;
using System.Text;

namespace sorovi.Validation.Common
{
    public abstract class ArgumentInfoBase<TValue, TExceptionDelegate>
        where TExceptionDelegate : Delegate
    {
        public bool HasErrors { get; protected set; } = false;
        public abstract string ErrorMessage { get; }
        public abstract TValue Value { get; }
        public abstract string MemberName { get; }
        public abstract void ExceptionHandler(in string type, in string message);
        protected abstract TExceptionDelegate InnerExceptionHandler { get; }

        public abstract ArgumentInfoBase<TValue, TExceptionDelegate> WithExceptionHandler(in TExceptionDelegate exceptionHandler);

        public static implicit operator TValue(in ArgumentInfoBase<TValue, TExceptionDelegate> arg) =>
            arg.Value;

        internal abstract ArgumentInfoBase<TNewValue, TExceptionDelegate> New<TNewValue>(in TNewValue value, in string memberName);
    }
}