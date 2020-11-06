using System;
using System.Linq.Expressions;
using sorovi.Validation.Common;

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

            var (value, memberName) = ExpressionHelper.TryGetValue(propertyExpression, currentArg.Value);

            arg(
                currentArg.New(value, BuildMemberName(currentArg.MemberName, memberName))
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