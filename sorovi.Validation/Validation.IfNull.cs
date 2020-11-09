using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfNull<T>(this ArgumentInfo<T> arg, in string type = ValidationType.IfNull, in string message = null)
        {
            if (arg.Value is null) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfNotNull<T>(this ArgumentInfo<T> arg, in string type = ValidationType.IfNotNull, in string message = null)
        {
            if (!(arg.Value is null)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        public static ArgumentInfo<KeyValuePair<TKey, TValue>> IfNull<TKey, TValue>(
            this ArgumentInfo<KeyValuePair<TKey, TValue>> arg,
            in string type = ValidationType.IfNull,
            in string message = null
        )
        {
            if (arg.Value.Value is null) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<KeyValuePair<TKey, TValue>> IfNotNull<TKey, TValue>(
            this ArgumentInfo<KeyValuePair<TKey, TValue>> arg,
            in string type = ValidationType.IfNotNull,
            in string message = null
        )
        {
            if (!(arg.Value.Value is null)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }
    }
}