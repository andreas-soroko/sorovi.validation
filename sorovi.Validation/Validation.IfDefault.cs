using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfDefault
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfDefault<T>(this ArgumentInfo<T> arg, in string type = ValidationType.IfDefault, in string message = null)
        {
            if (EqualityComparer<T>.Default.Equals(arg.Value, default(T)))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfNotDefault<T>(this ArgumentInfo<T> arg, in string type = ValidationType.IfNotDefault, in string message = null)
        {
            if (!EqualityComparer<T>.Default.Equals(arg.Value, default(T)))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }

            return arg;
        }
    }
}