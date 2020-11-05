using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class ValidationIfLowerThan
    {
        public static ref readonly ArgumentInfo<T> IfLowerThan<T>(this in ArgumentInfo<T> arg, T value, in string type = ValidationTypes.ValueLowerThan, in string message = null)
            where T : IComparable<T>
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) >= 0) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be lower than {value}", arg.MemberName, arg.MemberName);
        }

        // x: 1, y: 1 = 0
        // x: 0, y: 1 = < 0
        // x: 1, y: 0 = > 0

        public static ref readonly ArgumentInfo<T> IfLowerOrEqualsThan<T>(this in ArgumentInfo<T> arg, T value, in string type = ValidationTypes.ValueLowerOrEqualsThan, in string message = null)
            where T : IComparable<T>
        {
            if (Comparer<T>.Default.Compare(arg.Value, value) > 0) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be lower or equals than {value}", arg.MemberName, arg.MemberName);

        }
    }
}