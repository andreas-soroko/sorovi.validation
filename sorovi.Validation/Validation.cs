using System;
using System.Linq;
using System.Linq.Expressions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class Validation
    {
        public static ArgumentInfo<T> ThrowOn<T>(in Expression<Func<T>> getterExpression)
        {
            if (getterExpression is null) throw new ArgumentNullException(nameof(getterExpression));

            var memberName = getterExpression.Body switch
            {
                MemberExpression memberExpression => memberExpression.Member.Name,
                ConstantExpression constantExpression => "", // IsSimpleType(constantExpression.Type) ? null : constantExpression.Type.Name,
                _ => throw new ArgumentException("unsupported expression type."),
            };

            return ThrowOn(getterExpression, memberName);
        }

        public static ArgumentInfo<T> ThrowOn<T>(in Expression<Func<T>> getterExpression, in string memberName)
        {
            if (getterExpression is null) throw new ArgumentNullException(nameof(getterExpression));

            var getter = getterExpression.CompileFast(); // caching ? 
            return ThrowOn<T>(getter(), memberName);
        }

        public static ArgumentInfo<T> ThrowOn<T>(in T value) => ThrowOn(value, null);
        public static ArgumentInfo<T> ThrowOn<T>(in T value, in string memberName) => new ArgumentInfo<T>(value, memberName);

        private static Type[] _simpleTypes = new[]
        {
            typeof(String),
            typeof(Decimal),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan),
            typeof(Guid)
        };

        private static bool IsSimpleType(Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                _simpleTypes.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}