using System;
using System.Linq.Expressions;
using sorovi.Validation.Common;
using sorovi.Validation.Exceptions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class ValidationMember
    {
        public static ref readonly ArgumentInfo<TFirstType> Member<TFirstType, TSecondType>(
            this in ArgumentInfo<TFirstType> currentArg,
            in Expression<Func<TFirstType, TSecondType>> propertyExpression,
            in Action<ArgumentInfo<TSecondType>> arg
        )
        {
            if (propertyExpression is null) throw new ArgumentNullException(nameof(propertyExpression));
            if (!(propertyExpression.Body is MemberExpression memberExpression)) throw new ArgumentException("A member expression is expected.");
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            var info = ArgumentMemberInfo.GetMemberInfo(propertyExpression);
            TSecondType value = info.GetValue(currentArg.Value);
            
            arg(
                Validation.ThrowOn(value, BuildMemberName(currentArg.MemberName, info.Name))
                    .WithException(currentArg.CreateException)
            );
            return ref currentArg;
        }

        private static string BuildMemberName(in string firstMember, in string secondMember)
        {
            bool isFirstMemberNullOrWhiteSpace = string.IsNullOrWhiteSpace(firstMember);
            bool isSecondMemberNullOrWhiteSpace = string.IsNullOrWhiteSpace(secondMember);

            if (isFirstMemberNullOrWhiteSpace && isSecondMemberNullOrWhiteSpace) return null;
            if (!isFirstMemberNullOrWhiteSpace && !isSecondMemberNullOrWhiteSpace) return $"{firstMember}.{secondMember}";
            if (!isFirstMemberNullOrWhiteSpace) return $"{firstMember}";

            return $"{secondMember}";
        }
    }
}