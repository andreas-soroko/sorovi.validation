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
        public static ref readonly ArgumentInfo<string> IfNullOrWhiteSpace(this in ArgumentInfo<string> arg, in string type = ValidationTypes.ValueEmpty, in string message = null)
        {
            if (!string.IsNullOrWhiteSpace(arg.Value)) { return ref arg; }

            var errorMessage = message ?? $"Expected '{arg.MemberName}' not to be null or whitespace";
            throw arg.CreateException(type, errorMessage, arg.MemberName, arg.MemberName);
        }

        public static ref readonly ArgumentInfo<string> IfNotNullOrWhiteSpace(this in ArgumentInfo<string> arg, in string type = ValidationTypes.ValueNotEmpty, in string message = null)
        {
            if (string.IsNullOrWhiteSpace(arg.Value)) { return ref arg; }

            var errorMessage = message ?? $"Expected '{arg.MemberName}' to be null or whitespace";
            throw arg.CreateException(type, errorMessage, arg.MemberName, arg.MemberName);
        }
    }
}