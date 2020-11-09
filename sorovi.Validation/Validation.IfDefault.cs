using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfDefault
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfDefault<T, TEx>(this ArgumentInfo<T, TEx> arg, in string type = ValidationType.IfDefault, in string message = null)
            where TEx : Delegate
        {
            if (EqualityComparer<T>.Default.Equals(arg.Value, default(T)))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfNotDefault<T, TEx>(this ArgumentInfo<T, TEx> arg, in string type = ValidationType.IfNotDefault, in string message = null)
            where TEx : Delegate
        {
            if (!EqualityComparer<T>.Default.Equals(arg.Value, default(T)))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }

            return arg;
        }
    }
}