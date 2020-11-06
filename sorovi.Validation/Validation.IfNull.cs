using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNull
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<T> IfNull<T>(this in ArgumentInfo<T> arg, in string type = ValidationType.IfNull, in string message = null)
        {
            if (arg.Value is null) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return ref arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<T> IfNotNull<T>(this in ArgumentInfo<T> arg, in string type = ValidationType.IfNotNull, in string message = null)
        {
            if (!(arg.Value is null)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return ref arg;
        }
    }
}