using System;
using System.Linq.Expressions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class ValidationMember
    {
        public static ref readonly ArgumentInfo<TFirstType> Member<TFirstType, TSecondType>(this in ArgumentInfo<TFirstType> currentArg, Expression<Func<TFirstType, TSecondType>> getterExpression, in Action<ArgumentInfo<TSecondType>> arg)
        {
            if (getterExpression is null) throw new ArgumentNullException(nameof(getterExpression));
            if (!(getterExpression.Body is MemberExpression memberExpression)) throw new ArgumentException("A member expression is expected.");


            var getter = getterExpression.CompileFast(); // caching ? 
            TSecondType value = getter(currentArg.Value);
            if (arg == null) throw new ArgumentNullException(nameof(arg));
            arg(Validation.ThrowOn(value, BuildMemberName(currentArg.MemberName, memberExpression.Member.Name)));
            return ref currentArg;
        }

        private static string BuildMemberName(string firstMember, string secondMember)
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