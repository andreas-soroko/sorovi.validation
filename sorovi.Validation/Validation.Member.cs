using System;
using System.Linq.Expressions;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class ValidationMember
    {
        public static ArgumentInfoBase<TFirstType, TEx> Member<TFirstType, TSecondType, TEx>(
            this ArgumentInfoBase<TFirstType, TEx> currentArg,
            in Expression<Func<TFirstType, TSecondType>> propertyExpression,
            in Action<ArgumentInfoBase<TSecondType, TEx>> arg
        )
            where TEx : Delegate
        {
            if (propertyExpression is null) throw new ArgumentNullException(nameof(propertyExpression));
            if (!(propertyExpression.Body is MemberExpression memberExpression)) throw new ArgumentException("A member expression is expected.");
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            var (value, memberName) = ExpressionHelper.TryGetValue(propertyExpression, currentArg.Value);

            arg(
                currentArg.New(value, BuildMemberName(currentArg.MemberName, memberName))
            );
            return currentArg;
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