using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfEqualsTo
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfEqualsTo<T>(this ArgumentInfo<T> arg, in T compareValue, in string type = ValidationType.IfEqualsTo, in string message = null)
        {
            if (EqualityComparer<T>.Default.Equals(arg.Value, compareValue))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, compareValue));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfNotEqualsTo<T>(this ArgumentInfo<T> arg, in T compareValue, in string type = ValidationType.IfNotEqualsTo, in string message = null)
        {
            if (!EqualityComparer<T>.Default.Equals(arg.Value, compareValue))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, compareValue));
            }

            return arg;
        }
    }
}