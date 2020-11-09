using System;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> If<T>(this ArgumentInfo<T> arg, Predicate<T> predicate, in string type = ValidationType.If, in string message = null)
        {
            if (predicate(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfNot<T>(this ArgumentInfo<T> arg, Predicate<T> predicate, in string type = ValidationType.IfNot, in string message = null)
        {
            if (!predicate(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }
    }
}