using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfNull
    {
        public static ref readonly ArgumentInfo<T> IfNull<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            if (!(arg.Value is null)) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' not to be <null>", arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<T> IfNotNull<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            if (arg.Value is null) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be <null>", arg.MemberName, arg.MemberName);
        }
    }
}