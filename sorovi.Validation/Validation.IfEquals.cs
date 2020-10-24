using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class ValidationIfEquals
    {
        public static ref readonly ArgumentInfo<T> IfEqualsTo<T>(this in ArgumentInfo<T> arg, in T compareValue, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            arg.ThrowIf(EqualityComparer<T>.Default.Equals(arg.Value, compareValue), type, message ?? $"Expected '{arg.MemberName}' not to be <null>");
            return ref arg;
        }

        public static ref readonly ArgumentInfo<T> IfNotEqualsTo<T>(this in ArgumentInfo<T> arg, in T compareValue, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            arg.ThrowIf(!EqualityComparer<T>.Default.Equals(arg.Value, compareValue), type, message ?? $"Expected '{arg.MemberName}' to be <null>");
            return ref arg;
        }
    }
}