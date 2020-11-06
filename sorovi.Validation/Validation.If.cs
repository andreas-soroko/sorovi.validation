using System;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<T, TEx> If<T, TEx>(this ArgumentInfoBase<T, TEx> arg, Predicate<T> predicate, in string type = ValidationType.If, in string message = null)
            where TEx : Delegate
        {
            if (predicate(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.Value)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<T, TEx> IfNot<T, TEx>(this ArgumentInfoBase<T, TEx> arg, Predicate<T> predicate, in string type = ValidationType.IfNot, in string message = null)
            where TEx : Delegate
        {
            if (!predicate(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.Value)); }

            return arg;
        }
    }
}