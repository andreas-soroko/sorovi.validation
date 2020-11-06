using System;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<T, TEx> IfNull<T, TEx>(this ArgumentInfoBase<T, TEx> arg, in string type = ValidationType.IfNull, in string message = null)
            where TEx : Delegate
        {
            if (arg.Value is null) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<T, TEx> IfNotNull<T, TEx>(this ArgumentInfoBase<T, TEx> arg, in string type = ValidationType.IfNotNull, in string message = null)
            where TEx : Delegate
        {
            if (!(arg.Value is null)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }
    }
}