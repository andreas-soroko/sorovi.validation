using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfDefault
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<T> IfDefault<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueDefaultValue, in string message = null)
        {
            if (EqualityComparer<T>.Default.Equals(arg.Value, default(T)))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }

            return ref arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<T> IfNotDefault<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNotDefaultValue, in string message = null)
        {
            if (!EqualityComparer<T>.Default.Equals(arg.Value, default(T)))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));
            }

            return ref arg;
        }
    }
}