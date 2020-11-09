using System;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNullOrWhiteSpace
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<string, TEx> IfNullOrWhiteSpace<TEx>(this ArgumentInfo<string, TEx> arg, in string type = ValidationType.IfNullOrWhiteSpace, in string message = null)
            where TEx : Delegate
        {
            if (string.IsNullOrWhiteSpace(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfo<string, TEx> IfNotNullOrWhiteSpace<TEx>(this ArgumentInfo<string, TEx> arg, in string type = ValidationType.IfNotNullOrWhiteSpace, in string message = null)
            where TEx : Delegate
        {
            if (!string.IsNullOrWhiteSpace(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }
    }
}