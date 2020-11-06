using System;
using System.Linq.Expressions;
using System.Reflection;
using sorovi.Validation.ExpressionTrees;

namespace sorovi.Validation.Common
{
    // TODO remove duplicate code parts
    public static class ExpressionHelper
    {
        public static (T2 value, string memberName) TryGetValue<T, T2>(Expression<Func<T, T2>> expression, T target)
        {
            if (!(expression.Body is MemberExpression memberExpression))
            {
                throw new NotSupportedException($"Expected expression to be 'MemberExpression' but was '{expression.Body.GetType().Name}'");
            }

            var value = memberExpression.Expression switch
            {
                ParameterExpression parameterExpression => GetValue<T2>(memberExpression.Member, target),
                _ => expression.CompileFast()(target),
            };

            return (value, memberExpression.Member.Name);
        }

        public static (T value, string memberName) TryGetValue<T>(Expression<Func<T>> expression)
        {
            if (!(expression.Body is MemberExpression memberExpression))
            {
                throw new NotSupportedException($"Expected expression to be 'MemberExpression' but was '{expression.Body.GetType().Name}'");
            }

            var value = memberExpression.Expression switch
            {
                ConstantExpression innerConstantExpression => GetValue<T>(memberExpression.Member, innerConstantExpression.Value),
                null => GetValue<T>(memberExpression.Member, null),
                _ => expression.CompileFast()(),
            };

            return (value, memberExpression.Member.Name);
        }

        private static T GetValue<T>(MemberInfo memberInfo, object target = null)
        {
            return memberInfo switch
            {
                PropertyInfo propertyInfo => (T)propertyInfo.GetValue(target),
                FieldInfo fieldInfo => (T)fieldInfo.GetValue(target),
                _ => throw new ArgumentException("unsupported expression type."),
            };
        }
    }
}