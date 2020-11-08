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
        public static ArgumentInfoBase<string, TEx> IfEmpty<TEx>(this ArgumentInfoBase<string, TEx> arg, in string type = ValidationType.IfEmpty, in string message = null)
            where TEx : Delegate
        {
            if (string.IsNullOrEmpty(arg.Value))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<Guid, TEx> IfEmpty<TEx>(this ArgumentInfoBase<Guid, TEx> arg, in string type = ValidationType.IfEmpty, in string message = null)
            where TEx : Delegate
        {
            if (arg.Value == Guid.Empty)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<T, TEx> IfEmpty<T, TEx>(this ArgumentInfoBase<T, TEx> arg, in string type = ValidationType.IfEmpty, in string message = null)
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
        public static ArgumentInfoBase<string, TEx> IfNotEmpty<TEx>(this ArgumentInfoBase<string, TEx> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
            where TEx : Delegate
        {
            if (!string.IsNullOrEmpty(arg.Value))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<Guid, TEx> IfNotEmpty<TEx>(this ArgumentInfoBase<Guid, TEx> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
            where TEx : Delegate
        {
            if (arg.Value != Guid.Empty)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<T, TEx> IfNotEmpty<T, TEx>(this ArgumentInfoBase<T, TEx> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
            where T : IEnumerable
            where TEx : Delegate
        {
            if (arg.Value?.Cast<object>().Any() == true)
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }
            return arg;
        }

        // public static ArgumentInfoBase<T, TEx> IfNotEmpty<T, TEx>(this ArgumentInfoBase<T, TEx> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
        //     where TEx : Delegate
        // {
        //     if (arg.Value is null) { return arg; }
        //
        //     switch (arg.Value)
        //     {
        //         case string sValue:
        //             if (string.IsNullOrEmpty(sValue)) { return arg; }
        //
        //             break;
        //         case IEnumerable<object> eValue:
        //             if (!eValue.Any()) { return arg; }
        //
        //             break;
        //         case IDictionary sValue:
        //             if (sValue.Count == 0) { return arg; }
        //
        //             break;
        //         case Guid sValue:
        //             if (sValue == Guid.Empty) { return arg; }
        //
        //             break;
        //         default: throw new NotSupportedException($"Specified type({typeof(T)}) is not supported.");
        //     }
        //
        //     arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
        //
        //     return arg;
        // }
    }
}