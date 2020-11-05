using System.Collections.Generic;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfDefault
    {
        public static ref readonly ArgumentInfo<T> IfDefault<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueDefaultValue, in string message = null)
        {
            if (!EqualityComparer<T>.Default.Equals(arg.Value, default(T))) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' not to default", arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<T> IfNotDefault<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNotDefaultValue, in string message = null)
        {
            if (EqualityComparer<T>.Default.Equals(arg.Value, default(T))) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be default", arg.MemberName, arg.MemberName);
        }
    }
}