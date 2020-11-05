using System;
using System.Linq.Expressions;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class ValidationIf
    {
        public static ref readonly ArgumentInfo<T> If<T>(this in ArgumentInfo<T> arg, Predicate<T> predicate, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            if (!predicate(arg.Value)) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' not to be <null>", arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<T> IfNot<T>(this in ArgumentInfo<T> arg, Predicate<T> predicate, in string type = ValidationTypes.ValueNull, in string message = null)
        {
            if (predicate(arg.Value)) { return ref arg; }

            throw arg.CreateException(type, message ?? $"Expected '{arg.MemberName}' to be <null>", arg.MemberName, arg.MemberName);
        }
    }
}