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
        public static ArgumentInfo<string> IfEmpty(this ArgumentInfo<string> arg, in string type = ValidationType.IfEmpty, in string message = null)
        {
            if (string.IsNullOrEmpty(arg.Value))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<Guid> IfEmpty(this ArgumentInfo<Guid> arg, in string type = ValidationType.IfEmpty, in string message = null)
        {
            if (arg.Value == Guid.Empty)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<Guid?> IfEmpty(this ArgumentInfo<Guid?> arg, in string type = ValidationType.IfEmpty, in string message = null)
        {
            if (arg.Value is null || arg.Value == Guid.Empty)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfEmpty<T>(this ArgumentInfo<T> arg, in string type = ValidationType.IfEmpty, in string message = null)
            where T : IEnumerable
        {
            if (arg.Value is null || !arg.Value.Cast<object>().Any())
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<string> IfNotEmpty(this ArgumentInfo<string> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
        {
            if (!string.IsNullOrEmpty(arg.Value))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<Guid> IfNotEmpty(this ArgumentInfo<Guid> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
        {
            if (arg.Value != Guid.Empty)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<Guid?> IfNotEmpty(this ArgumentInfo<Guid?> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
        {
            if (arg.Value != null && arg.Value != Guid.Empty)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<T> IfNotEmpty<T>(this ArgumentInfo<T> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
            where T : IEnumerable
        {
            if (arg.Value?.Cast<object>().Any() == true)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }
    }
}