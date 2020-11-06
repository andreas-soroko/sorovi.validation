using System;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIf
    {
        public static ref readonly ArgumentInfo<T> If<T>(this in ArgumentInfo<T> arg, Predicate<T> predicate, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            if (!predicate(arg.Value)) { return ref arg; }

            arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' not to be <null>");

            return ref arg;
        }

        public static ref readonly ArgumentInfo<T> IfNot<T>(this in ArgumentInfo<T> arg, Predicate<T> predicate, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            if (predicate(arg.Value)) { return ref arg; }

            arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' to be <null>");

            return ref arg;
        }
    }
}