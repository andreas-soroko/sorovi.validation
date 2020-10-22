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
    public static class ValidationIfEmpty
    {
        public static ref readonly ArgumentInfo<T> IfEmpty<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueEmpty, in string message = null)
            => ref arg.IfEmpty<T, Exception>(type, message);

        public static ref readonly ArgumentInfo<T> IfEmpty<T, TException>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueEmpty, in string message = null)
            where TException : Exception
        {
            var errorMessage = message ?? $"Expected '{arg.MemberName}' not to be empty";
            arg.IfNull<T, TException>();

            switch (arg.Value)
            {
                case string sValue:
                    ExceptionFactory.ThrowIf<TException>(string.IsNullOrEmpty(sValue), type, errorMessage);
                    break;
                case IEnumerable<object> eValue:
                    ExceptionFactory.ThrowIf<TException>(!eValue.Any(), type, errorMessage);
                    break;
                case IDictionary sValue:
                    ExceptionFactory.ThrowIf<TException>(sValue.Count == 0, type, errorMessage);
                    break;
                case Guid sValue:
                    ExceptionFactory.ThrowIf<TException>(sValue == Guid.Empty, type, errorMessage);
                    break;
                default:
                    throw new NotSupportedException($"Specified type({arg.Value.GetType()}) is not supported.");
            }

            return ref arg;
        }
        
        
        public static ref readonly ArgumentInfo<T> IfNotEmpty<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNotEmpty, in string message = null)
            => ref arg.IfNotEmpty<T, Exception>(type, message);

        public static ref readonly ArgumentInfo<T> IfNotEmpty<T, TException>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNotEmpty, in string message = null)
            where TException : Exception
        {
            var errorMessage = message ?? $"Expected '{arg.MemberName}' to be empty";

            switch (arg.Value)
            {
                case string sValue:
                    ExceptionFactory.ThrowIf<TException>(!string.IsNullOrEmpty(sValue), type, errorMessage);
                    break;
                case IEnumerable<object> eValue:
                    ExceptionFactory.ThrowIf<TException>(eValue.Any(), type, errorMessage);
                    break;
                case IDictionary sValue:
                    ExceptionFactory.ThrowIf<TException>(sValue.Count > 0, type, errorMessage);
                    break;
                case Guid sValue:
                    ExceptionFactory.ThrowIf<TException>(sValue != Guid.Empty, type, errorMessage);
                    break;

            }

            return ref arg;
        }
    }
}