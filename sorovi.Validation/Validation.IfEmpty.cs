using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationIfEmpty
    {
        public static ArgumentInfoBase<T, TEx> IfEmpty<T, TEx>(this ArgumentInfoBase<T, TEx> arg, in string type = ValidationType.IfEmpty, in string message = null)
            where TEx : Delegate
        {
            arg.IfNull<T, TEx>(type);

            switch (arg.Value)
            {
                case string sValue:
                    if (!string.IsNullOrEmpty(sValue)) { return arg; }

                    break;
                case IEnumerable<object> eValue:
                    if (eValue.Any()) { return arg; }

                    break;
                case IDictionary sValue:
                    if (sValue.Count > 0) { return arg; }

                    break;
                case Guid sValue:
                    if (sValue != Guid.Empty) { return arg; }

                    break;
                default: throw new NotSupportedException($"Specified type({typeof(T)}) is not supported.");
            }

            arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));

            return arg;
        }

        public static ArgumentInfoBase<T, TEx> IfNotEmpty<T, TEx>(this ArgumentInfoBase<T, TEx> arg, in string type = ValidationType.IfNotEmpty, in string message = null)
            where TEx : Delegate
        {
            if (arg.Value is null) { return arg; }

            switch (arg.Value)
            {
                case string sValue:
                    if (string.IsNullOrEmpty(sValue)) { return arg; }

                    break;
                case IEnumerable<object> eValue:
                    if (!eValue.Any()) { return arg; }

                    break;
                case IDictionary sValue:
                    if (sValue.Count == 0) { return arg; }

                    break;
                case Guid sValue:
                    if (sValue == Guid.Empty) { return arg; }

                    break;
                default: throw new NotSupportedException($"Specified type({typeof(T)}) is not supported.");
            }

            arg.ExceptionHandler(type, ErrorMessage.For(type, message, arg.MemberName));

            return arg;
        }
    }
}