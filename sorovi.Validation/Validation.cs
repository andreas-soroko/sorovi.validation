using System;
using System.Linq.Expressions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class Validation
    {
        public static ArgumentInfo<T> ThrowOn<T>(in Expression<Func<T>> getterExpression)
        {
            if (getterExpression is null) throw new ArgumentNullException(nameof(getterExpression));
            if (!(getterExpression.Body is MemberExpression memberExpression)) throw new ArgumentException("A member expression is expected.");
            return ThrowOn(getterExpression, memberExpression.Member.Name);
        }

        public static ArgumentInfo<T> ThrowOn<T>(in Expression<Func<T>> getterExpression, in string memberName)
        {
            if (getterExpression is null) throw new ArgumentNullException(nameof(getterExpression));

            var getter = getterExpression.CompileFast(); // caching ? 
            return ThrowOn<T>(getter(), memberName);
        }

        public static ArgumentInfo<T> ThrowOn<T>(in T value) => ThrowOn(value, null);
        public static ArgumentInfo<T> ThrowOn<T>(in T value, in string memberName) => new ArgumentInfo<T>(value, memberName);
    }
}