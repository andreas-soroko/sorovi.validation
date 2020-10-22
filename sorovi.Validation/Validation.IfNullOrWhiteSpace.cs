using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class ValidationIfNullOrWhiteSpace
    {
        public static ref readonly ArgumentInfo<string> IfNullOrWhiteSpace(this in ArgumentInfo<string> arg, in string type = ValidationTypes.ValueEmpty, in string message = null) =>
            ref arg.IfNullOrWhiteSpace<Exception>(type, message);

        public static ref readonly ArgumentInfo<string> IfNullOrWhiteSpace<TException>(this in ArgumentInfo<string> arg, in string type = ValidationTypes.ValueEmpty, in string message = null)
            where TException : Exception
        {
            var errorMessage = message ?? $"Expected '{arg.MemberName}' not to be null or whitespace";

            ExceptionFactory.ThrowIf<TException>(string.IsNullOrWhiteSpace(arg.Value), type, errorMessage);
            return ref arg;
        }

        public static ref readonly ArgumentInfo<string> IfNotNullOrWhiteSpace(this in ArgumentInfo<string> arg, in string type = ValidationTypes.ValueNotEmpty, in string message = null) =>
            ref arg.IfNotNullOrWhiteSpace<Exception>(type, message);

        public static ref readonly ArgumentInfo<string> IfNotNullOrWhiteSpace<TException>(this in ArgumentInfo<string> arg, in string type = ValidationTypes.ValueNotEmpty, in string message = null)
            where TException : Exception
        {
            var errorMessage = message ?? $"Expected '{arg.MemberName}' to be null or whitespace";

            ExceptionFactory.ThrowIf<TException>(!string.IsNullOrWhiteSpace(arg.Value), type, errorMessage);
            return ref arg;
        }
    }
}