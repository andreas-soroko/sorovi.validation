using System.Collections.Generic;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfDefault
    {
        public static ref readonly ArgumentInfo<T> IfDefault<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueDefaultValue, in string message = null)
        {
            if (!EqualityComparer<T>.Default.Equals(arg.Value, default(T))) { return ref arg; }

            arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' not to default");

            return ref arg;
        }

        public static ref readonly ArgumentInfo<T> IfNotDefault<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNotDefaultValue, in string message = null)
        {
            if (EqualityComparer<T>.Default.Equals(arg.Value, default(T))) { return ref arg; }

            arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' to be default");

            return ref arg;
        }
    }
}