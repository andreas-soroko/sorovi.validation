using System;
using System.Linq;
using System.Linq.Expressions;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation
{
    public static class Validation
    {
        public static ArgumentInfo<T> ThrowOn<T>(in Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression is null) throw new ArgumentNullException(nameof(propertyExpression));

            var memberName = propertyExpression.Body switch
            {
                MemberExpression memberExpression => memberExpression.Member.Name,
                ConstantExpression constantExpression => "", // IsSimpleType(constantExpression.Type) ? null : constantExpression.Type.Name,
                _ => throw new ArgumentException("unsupported expression type."),
            };

            return ThrowOn(propertyExpression, memberName);
        }

        public static ArgumentInfo<T> ThrowOn<T>(in Expression<Func<T>> propertyExpression, in string memberName)
        {
            if (propertyExpression is null) throw new ArgumentNullException(nameof(propertyExpression));

            var getter = propertyExpression.CompileFast(); // caching ? 
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

        private static bool IsSimpleType(in Type type)
        {
            return
                type.IsValueType ||
                type.IsPrimitive ||
                _simpleTypes.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }
    }
}