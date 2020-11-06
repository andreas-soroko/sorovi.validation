using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<T> IfNull<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            if (arg.Value is null)
            {
                arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' not to be <null>");
            }

            return ref arg;
        }

        public static ref readonly ArgumentInfo<T> IfNotNull<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            if (arg.Value is null) { return ref arg; }

            arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' to be <null>");

            return ref arg;
        }
    }
}