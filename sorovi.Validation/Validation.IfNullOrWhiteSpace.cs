using System;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNullOrWhiteSpace
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<string, TEx> IfNullOrWhiteSpace<TEx>(this ArgumentInfoBase<string, TEx> arg, in string type = ValidationType.IfNullOrWhiteSpace, in string message = null)
            where TEx : Delegate
        {
            if (string.IsNullOrWhiteSpace(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ArgumentInfoBase<string, TEx> IfNotNullOrWhiteSpace<TEx>(this ArgumentInfoBase<string, TEx> arg, in string type = ValidationType.IfNotNullOrWhiteSpace, in string message = null)
            where TEx : Delegate
        {
            if (!string.IsNullOrWhiteSpace(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return arg;
        }
    }
}