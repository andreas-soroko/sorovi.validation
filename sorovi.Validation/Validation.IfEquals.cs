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
            if (!EqualityComparer<T>.Default.Equals(arg.Value, compareValue)) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' not to be equal to {compareValue}", arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<T> IfNotEqualsTo<T>(this in ArgumentInfo<T> arg, in T compareValue, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            if (EqualityComparer<T>.Default.Equals(arg.Value, compareValue)) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be equal to {compareValue}", arg.MemberName, arg.MemberName);
        }
    }
}