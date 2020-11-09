using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfGreaterThan
    {
        // x: 1, y: 1 = 0
        // x: 0, y: 1 = < 0
        // x: 1, y: 0 = > 0

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfGreaterThan<T, TEx>(this ArgumentInfo<T, TEx> arg, T value, in string type = ValidationType.IfGreaterThan, in string message = null)
            where T : struct, IComparable<T>
            where TEx : Delegate
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) > 0)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, value));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T?, TEx> IfGreaterThan<T, TEx>(this ArgumentInfo<T?, TEx> arg, T? value, in string type = ValidationType.IfGreaterThan, in string message = null)
            where T : struct, IComparable<T>
            where TEx : Delegate
        {
            if (Comparer<T?>.Default.Compare(arg.Value, value) > 0)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, value));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfGreaterOrEqualsThan<T, TEx>(
            this ArgumentInfo<T, TEx> arg,
            T value,
            in string type = ValidationType.IfGreaterOrEqualsThan,
            in string message = null
        )
            where T : struct, IComparable<T>
            where TEx : Delegate
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) >= 0)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, value));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T?, TEx> IfGreaterOrEqualsThan<T, TEx>(
            this ArgumentInfo<T?, TEx> arg,
            T? value,
            in string type = ValidationType.IfGreaterOrEqualsThan,
            in string message = null
        )
            where T : struct, IComparable<T>
            where TEx : Delegate
        {
            if (Comparer<T?>.Default.Compare(arg.Value, value) >= 0)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, value));
            }

            return arg;
        }
    }
}