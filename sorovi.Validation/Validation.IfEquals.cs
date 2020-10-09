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
        public static ref readonly ArgumentInfo<T> IfEquals<T>(this in ArgumentInfo<T> arg, in T compareValue, in string type = ValidationTypes.ValueNull, in string message = null)
            => ref arg.IfEquals<T, Exception>(compareValue, type, message);

        public static ref readonly ArgumentInfo<T> IfEquals<T, TException>(this in ArgumentInfo<T> arg, in T compareValue, in string type = ValidationTypes.ValueNull, in string message = null)
            where TException : Exception
        {
            ExceptionFactory.ThrowIf<TException>(EqualityComparer<T>.Default.Equals(arg.Value, compareValue), type, message ?? $"Expected '{arg.MemberName}' not to be <null>");
            return ref arg;
        }

        public static ref readonly ArgumentInfo<T> IfNotEquals<T>(this in ArgumentInfo<T> arg, in T compareValue, in string type = ValidationTypes.ValueNull, in string message = null)
            => ref arg.IfNotEquals<T, Exception>(compareValue, type, message);

        public static ref readonly ArgumentInfo<T> IfNotEquals<T, TException>(this in ArgumentInfo<T> arg, in T compareValue, in string type = ValidationTypes.ValueNull, in string message = null)
            where TException : Exception
        {
            ExceptionFactory.ThrowIf<TException>(!EqualityComparer<T>.Default.Equals(arg.Value, compareValue), type, message ?? $"Expected '{arg.MemberName}' to be <null>");
            return ref arg;
        }
    }
}