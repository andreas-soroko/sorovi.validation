using System.Runtime.CompilerServices;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNullOrWhiteSpace
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<string> IfNullOrWhiteSpace(this in ArgumentInfo<string> arg, in string type = ValidationTypes.IfNullOrWhiteSpace, in string message = null)
        {
            if (string.IsNullOrWhiteSpace(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return ref arg;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref readonly ArgumentInfo<string> IfNotNullOrWhiteSpace(this in ArgumentInfo<string> arg, in string type = ValidationTypes.IfNotNullOrWhiteSpace, in string message = null)
        {
            if (!string.IsNullOrWhiteSpace(arg.Value)) { arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName)); }

            return ref arg;
        }
    }
}