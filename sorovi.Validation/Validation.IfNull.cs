using System;
using System.Linq.Expressions;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class ValidationIfNull
    {
        public static ref readonly ArgumentInfo<T> IfNull<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNull, in string message = null)
            => ref arg.IfNull<T, ValidationException>(type, message);

        public static ref readonly ArgumentInfo<T> IfNull<T, TException>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNull, in string message = null)
            where TException : Exception
        {
            ExceptionFactory.ThrowIf<TException>(arg.Value is null, type, message ?? $"Expected '{arg.MemberName}' not to be <null>", arg.MemberName);
            return ref arg;
        }

        public static ref readonly ArgumentInfo<T> IfNotNull<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNull, in string message = null)
            => ref arg.IfNotNull<T, ValidationException>(type, message);

        public static ref readonly ArgumentInfo<T> IfNotNull<T, TException>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNull, in string message = null)
            where TException : Exception
        {
            ExceptionFactory.ThrowIf<TException>(!(arg.Value is null), type, message ?? $"Expected '{arg.MemberName}' to be <null>", arg.MemberName);
            return ref arg;
        }
    }
}