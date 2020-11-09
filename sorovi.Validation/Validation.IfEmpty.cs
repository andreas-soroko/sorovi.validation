using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfEmpty
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<string, TEx> IfEmpty<TEx>(this ArgumentInfo<string, TEx> arg, in string type = ValidationType.IfEmpty, in string message = null)
            where TEx : Delegate
        {
            if (string.IsNullOrEmpty(arg.Value))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<Guid, TEx> IfEmpty<TEx>(this ArgumentInfo<Guid, TEx> arg, in string type = ValidationType.IfEmpty, in string message = null)
            where TEx : Delegate
        {
            if (arg.Value == Guid.Empty)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfEmpty<T, TEx>(this ArgumentInfo<T, TEx> arg, in string type = ValidationType.IfEmpty, in string message = null)
            where T : IEnumerable
            where TEx : Delegate
        {
            if (arg.Value is null || !arg.Value.Cast<object>().Any())
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<string, TEx> IfNotEmpty<TEx>(this ArgumentInfo<string, TEx> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
            where TEx : Delegate
        {
            if (!string.IsNullOrEmpty(arg.Value))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<Guid, TEx> IfNotEmpty<TEx>(this ArgumentInfo<Guid, TEx> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
            where TEx : Delegate
        {
            if (arg.Value != Guid.Empty)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T, TEx> IfNotEmpty<T, TEx>(this ArgumentInfo<T, TEx> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
            where T : IEnumerable
            where TEx : Delegate
        {
            if (arg.Value?.Cast<object>().Any() == true)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }
    }
}