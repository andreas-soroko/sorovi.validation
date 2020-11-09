using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfEqualsTo
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfEqualsTo<T, TEx>(this ArgumentInfo<T, TEx> arg, in T compareValue, in string type = ValidationType.IfEqualsTo, in string message = null)
            where TEx : Delegate
        {
            if (EqualityComparer<T>.Default.Equals(arg.Value, compareValue))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, compareValue));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfNotEqualsTo<T, TEx>(this ArgumentInfo<T, TEx> arg, in T compareValue, in string type = ValidationType.IfNotEqualsTo, in string message = null)
            where TEx : Delegate
        {
            if (!EqualityComparer<T>.Default.Equals(arg.Value, compareValue))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, compareValue));
            }

            return arg;
        }
    }
}