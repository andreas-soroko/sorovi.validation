using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfNull<T, TEx>(this ArgumentInfo<T, TEx> arg, in string type = ValidationType.IfNull, in string message = null)
            where TEx : Delegate
        {
            if (arg.Value is null) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfNotNull<T, TEx>(this ArgumentInfo<T, TEx> arg, in string type = ValidationType.IfNotNull, in string message = null)
            where TEx : Delegate
        {
            if (!(arg.Value is null)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        public static ArgumentInfo<KeyValuePair<TKey, TValue>, TEx> IfNull<TKey, TValue, TEx>(
            this ArgumentInfo<KeyValuePair<TKey, TValue>, TEx> arg,
            in string type = ValidationType.IfNull,
            in string message = null
        )
            where TEx : Delegate
        {
            if (arg.Value.Value is null) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<KeyValuePair<TKey, TValue>, TEx> IfNotNull<TKey, TValue, TEx>(
            this ArgumentInfo<KeyValuePair<TKey, TValue>, TEx> arg,
            in string type = ValidationType.IfNotNull,
            in string message = null
        )
            where TEx : Delegate
        {
            if (!(arg.Value.Value is null)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }
    }
}