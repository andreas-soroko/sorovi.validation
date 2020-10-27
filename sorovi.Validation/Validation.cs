using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using sorovi.Validation.Common;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class Validation
    {
        public static ArgumentInfo<T> ThrowOn<T>(in Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression is null) throw new ArgumentNullException(nameof(propertyExpression));

            var (value, memberName) = ExpressionHelper.TryGetValue(propertyExpression);

            return ThrowOn(value, memberName);
        }

        public static ArgumentInfo<T> ThrowOn<T>(in Func<T> propertyGetter, in string memberName)
        {
            if (propertyGetter is null) throw new ArgumentNullException(nameof(propertyGetter));

            return ThrowOn<T>(propertyGetter(), memberName);
        }

        public static ArgumentInfo<T> ThrowOn<T>(in T value) => ThrowOn(value, null);
        public static ArgumentInfo<T> ThrowOn<T>(in T value, in string memberName) => new ArgumentInfo<T>(value, memberName);
    }
}