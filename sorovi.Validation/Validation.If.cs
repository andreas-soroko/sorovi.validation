using System;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<T> If<T>(this in ArgumentInfo<T> arg, Predicate<T> predicate, in string type = ValidationTypes.ValueIf, in string message = null)
        {
            if (predicate(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.Value)); }

            return ref arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<T> IfNot<T>(this in ArgumentInfo<T> arg, Predicate<T> predicate, in string type = ValidationTypes.ValueIfNot, in string message = null)
        {
            if (!predicate(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.Value)); }

            return ref arg;
        }
    }
}