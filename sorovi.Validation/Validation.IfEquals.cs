using System.Collections.Generic;
using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfEquals
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<T> IfEqualsTo<T>(this in ArgumentInfo<T> arg, in T compareValue, in string type = ValidationTypes.ValueEquals, in string message = null)
        {
            if (EqualityComparer<T>.Default.Equals(arg.Value, compareValue))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, compareValue));
            }

            return ref arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<T> IfNotEqualsTo<T>(this in ArgumentInfo<T> arg, in T compareValue, in string type = ValidationTypes.ValueNotEquals, in string message = null)
        {
            if (!EqualityComparer<T>.Default.Equals(arg.Value, compareValue))
            {
                arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName, compareValue));
            }

            return ref arg;
        }
    }
}