using System;
using System.Collections.Generic;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfLowerThan
    {
        public static ref readonly ArgumentInfo<T> IfLowerThan<T>(this in ArgumentInfo<T> arg, T value, in string type = ValidationTypes.ValueLowerThan, in string message = null)
            where T : IComparable<T>
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) >= 0) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' not to be lower than {value}", arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<T?> IfLowerThan<T>(this in ArgumentInfo<T?> arg, T? value, in string type = ValidationTypes.ValueLowerThan, in string message = null)
            where T : struct, IComparable<T>
        {
            if (Comparer<T?>.Default.Compare(arg.Value, value) >= 0) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' not to be lower than {value}", arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<T> IfLowerOrEqualsThan<T>(this in ArgumentInfo<T> arg, T value, in string type = ValidationTypes.ValueLowerOrEqualsThan, in string message = null)
            where T : IComparable<T>
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) > 0) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' not to be lower or equals than {value}", arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<T?> IfLowerOrEqualsThan<T>(this in ArgumentInfo<T?> arg, T? value, in string type = ValidationTypes.ValueLowerOrEqualsThan, in string message = null)
            where T : struct, IComparable<T>
        {
            if (Comparer<T?>.Default.Compare(arg.Value, value) > 0) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' not to be lower or equals than {value}", arg.MemberName, arg.MemberName);
        }
    }
}