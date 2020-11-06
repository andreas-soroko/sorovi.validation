using System;
using System.Linq.Expressions;
using sorovi.Validation.Common;

namespace sorovi.Validation
{
    public static class Validation
    {
        public static ArgumentInfo<T> ThrowOn<T>(in Expression<Func<T>> propertyExpression, in ExceptionHandler exceptionHandler = null)
        {
            if (propertyExpression is null) { throw new ArgumentNullException(nameof(propertyExpression)); }

            var (value, memberName) = ExpressionHelper.TryGetValue(propertyExpression);

            return new ArgumentInfo<T>(value, memberName, exceptionHandler);
        }

        public static ArgumentInfo<T> ThrowOn<T>(in T value, in ExceptionHandler exceptionHandler = null) =>
            new ArgumentInfo<T>(value, null, exceptionHandler);

        public static ArgumentInfo<T> ThrowOn<T>(in T value, in string memberName, in ExceptionHandler exceptionHandler = null) =>
            new ArgumentInfo<T>(value, memberName, exceptionHandler);
    }
}