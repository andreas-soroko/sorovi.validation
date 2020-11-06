using System;
using System.Collections.Generic;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfGreaterThan
    {
        // x: 1, y: 1 = 0
        // x: 0, y: 1 = < 0
        // x: 1, y: 0 = > 0

        public static ref readonly ArgumentInfo<T> IfGreaterThan<T>(this in ArgumentInfo<T> arg, T value, in string type = ValidationTypes.ValueGreaterThan, in string message = null)
            where T : struct, IComparable<T>
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) <= 0) { return ref arg; }

            arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' not to be greater than {value}");

            return ref arg;
        }

        public static ref readonly ArgumentInfo<T?> IfGreaterThan<T>(this in ArgumentInfo<T?> arg, T? value, in string type = ValidationTypes.ValueGreaterThan, in string message = null)
            where T : struct, IComparable<T>
        {
            var x = Comparer<T?>.Default.Compare(arg.Value, value);
            if (x <= 0) { return ref arg; }

            arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' not to be greater than {value}");

            return ref arg;
        }

        public static ref readonly ArgumentInfo<T> IfGreaterOrEqualsThan<T>(this in ArgumentInfo<T> arg, T value, in string type = ValidationTypes.ValueGreaterOrEqualsThan, in string message = null)
            where T : struct, IComparable<T>
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) < 0) { return ref arg; }

            arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' not to be greater or equals than {value}");

            return ref arg;
        }

        public static ref readonly ArgumentInfo<T?> IfGreaterOrEqualsThan<T>(
            this in ArgumentInfo<T?> arg,
            T? value,
            in string type = ValidationTypes.ValueGreaterOrEqualsThan,
            in string message = null
        )
            where T : struct, IComparable<T>
        {
            if (Comparer<T?>.Default.Compare(arg.Value, value) < 0) { return ref arg; }

            arg.ExceptionHandler(type, message ?? $"Expected '{arg.MemberName}' not to be greater or equals than {value}");

            return ref arg;
        }
    }
}