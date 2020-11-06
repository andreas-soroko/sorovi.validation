using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfEmpty
    {
        public static ref readonly ArgumentInfo<T> IfEmpty<T>(this in ArgumentInfo<T> arg, in string type = ValidationType.IfEmpty, in string message = null)
        {
            arg.IfNull<T>(type);

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
                default: throw new NotSupportedException($"Specified type({typeof(T)}) is not supported.");
            }

            arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));

            return ref arg;
        }

        public static ref readonly ArgumentInfo<T> IfNotEmpty<T>(this in ArgumentInfo<T> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
        {
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
                default: throw new NotSupportedException($"Specified type({typeof(T)}) is not supported.");
            }

            arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));

            return ref arg;
        }
    }
}