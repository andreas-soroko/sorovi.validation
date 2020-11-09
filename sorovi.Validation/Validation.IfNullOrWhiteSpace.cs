using System;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNullOrWhiteSpace
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<string> IfNullOrWhiteSpace(this ArgumentInfo<string> arg, in string type = ValidationType.IfNullOrWhiteSpace, in string message = null)
        {
            if (string.IsNullOrWhiteSpace(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<string> IfNotNullOrWhiteSpace(this ArgumentInfo<string> arg, in string type = ValidationType.IfNotNullOrWhiteSpace, in string message = null)
        {
            if (!string.IsNullOrWhiteSpace(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }
    }
}