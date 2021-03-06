using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfLowerThan
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfLowerThan<T>(this ArgumentInfo<T> arg, T value, in string type = ValidationType.IfLowerThan, in string message = null)
            where T : IComparable<T>
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) < 0)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, value));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T?> IfLowerThan<T>(this ArgumentInfo<T?> arg, T? value, in string type = ValidationType.IfLowerThan, in string message = null)
            where T : struct, IComparable<T>
        {
            if (Comparer<T?>.Default.Compare(arg.Value, value) < 0)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, value));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfLowerOrEqualsThan<T>(this ArgumentInfo<T> arg, T value, in string type = ValidationType.IfLowerOrEqualsThan, in string message = null)
            where T : IComparable<T>
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) <= 0)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, value));
            }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T?> IfLowerOrEqualsThan<T>(this ArgumentInfo<T?> arg, T? value, in string type = ValidationType.IfLowerOrEqualsThan, in string message = null)
            where T : struct, IComparable<T>
        {
            if (Comparer<T?>.Default.Compare(arg.Value, value) <= 0)
            {
                arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' not to be lower or equals than {value}");
            }

            return arg;
        }
    }
}