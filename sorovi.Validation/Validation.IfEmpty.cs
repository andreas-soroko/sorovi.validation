using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using sorovi.Validation.Common;

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
                    if (!string.IsNullOrEmpty(sValue)) { return ref arg; }

                    break;
                case IEnumerable<object> eValue:
                    if (eValue.Any()) { return ref arg; }

                    break;
                case IDictionary sValue:
                    if (sValue.Count > 0) { return ref arg; }

                    break;
                case Guid sValue:
                    if (sValue != Guid.Empty) { return ref arg; }

                    break;
                default:
                    throw new NotSupportedException($"Specified type({typeof(T)}) is not supported.");
            }

            throw arg.CreateException(type, errorMessage, arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<T> IfNotEmpty<T>(this in ArgumentInfo<T> arg, in string type = ValidationTypes.ValueNotEmpty, in string message = null)
        {
            var errorMessage = message ?? $"Expected '{arg.MemberName}' to be empty";
            if (arg.Value is null) { return ref arg; }

            switch (arg.Value)
            {
                case string sValue:
                    if (string.IsNullOrEmpty(sValue)) { return ref arg; }

                    break;
                case IEnumerable<object> eValue:
                    if (!eValue.Any()) { return ref arg; }

                    break;
                case IDictionary sValue:
                    if (sValue.Count == 0) { return ref arg; }

                    break;
                case Guid sValue:
                    if (sValue == Guid.Empty) { return ref arg; }

                    break;
                default:
                    throw new NotSupportedException($"Specified type({typeof(T)}) is not supported.");
            }

            throw arg.CreateException(type, errorMessage, arg.MemberName, arg.MemberName);
        }
    }
}