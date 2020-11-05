using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.ExpressionTrees;

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

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be greater than {value}", arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<T?> IfGreaterThan<T>(this in ArgumentInfo<T?> arg, T? value, in string type = ValidationTypes.ValueGreaterThan, in string message = null)
            where T : struct, IComparable<T>
        {
            var x = Comparer<T?>.Default.Compare(arg.Value, value);
            if (x <= 0) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be greater than {value}", arg.MemberName, arg.MemberName);
        }


        public static ref readonly ArgumentInfo<T> IfGreaterOrEqualsThan<T>(this in ArgumentInfo<T> arg, T value, in string type = ValidationTypes.ValueGreaterOrEqualsThan, in string message = null)
            where T : struct, IComparable<T>
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) < 0) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be greater or equals than {value}", arg.MemberName, arg.MemberName);

        }

        public static ref readonly ArgumentInfo<T?> IfGreaterOrEqualsThan<T>(this in ArgumentInfo<T?> arg, T? value, in string type = ValidationTypes.ValueGreaterOrEqualsThan, in string message = null)
            where T : struct, IComparable<T>
        {
            if (Comparer<T?>.Default.Compare(arg.Value, value) < 0) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be greater or equals than {value}", arg.MemberName, arg.MemberName);

        }
    }
}