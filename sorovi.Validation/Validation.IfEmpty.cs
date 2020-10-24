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
        {
            var errorMessage = message ?? $"Expected '{arg.MemberName}' not to be empty";
            arg.IfNull<T>();

            switch (arg.Value)
            {
                case string sValue:
                    arg.ThrowIf(string.IsNullOrEmpty(sValue), type, errorMessage);
                    break;
                case IEnumerable<object> eValue:
                    arg.ThrowIf(!eValue.Any(), type, errorMessage);
                    break;
                case IDictionary sValue:
                    arg.ThrowIf(sValue.Count == 0, type, errorMessage);
                    break;
                case Guid sValue:
                    arg.ThrowIf(sValue == Guid.Empty, type, errorMessage);
                    break;
                default:
                    throw new NotSupportedException($"Specified type({typeof(T)}) is not supported.");
            }

            return ref arg;
        }

        public static ref readonly ArgumentInfo<T> IfNotEmpty<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNotEmpty, in string message = null)
        {
            var errorMessage = message ?? $"Expected '{arg.MemberName}' to be empty";
            if (arg.Value is null) { return ref arg; }

            switch (arg.Value)
            {
                case string sValue:
                    arg.ThrowIf(!string.IsNullOrEmpty(sValue), type, errorMessage);
                    break;
                case IEnumerable<object> eValue:
                    arg.ThrowIf(eValue.Any(), type, errorMessage);
                    break;
                case IDictionary sValue:
                    arg.ThrowIf(sValue.Count > 0, type, errorMessage);
                    break;
                case Guid sValue:
                    arg.ThrowIf(sValue != Guid.Empty, type, errorMessage);
                    break;
                default:
                    throw new NotSupportedException($"Specified type({typeof(T)}) is not supported.");
            }

            return ref arg;
        }
    }
}