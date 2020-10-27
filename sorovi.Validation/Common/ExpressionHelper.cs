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
            var memberName = "";
            T2 value;

            switch (expression.Body)
            {
                case MemberExpression memberExpression:
                    memberName = memberExpression.Member.Name;
                    switch (memberExpression.Expression)
                    {
                        case ParameterExpression parameterExpression:
                            value = memberExpression.Member switch
                            {
                                PropertyInfo propertyInfo => (T2)propertyInfo.GetValue(target),
                                FieldInfo fieldInfo => (T2)fieldInfo.GetValue(target),
                                _ =>  throw new ArgumentException("unsupported expression type.")
                            };
                            break;
                        default:
                            value = expression.CompileFast()(target);
                            break;
                    }

                    break;
                default:
                    throw new ArgumentException("unsupported expression type.");
            }

            return (value, memberName);
        }

        public static (T value, string memberName) TryGetValue<T>(Expression<Func<T>> expression)
        {
            var memberName = "";
            T value;

            switch (expression.Body)
            {
                case MemberExpression memberExpression:
                    memberName = memberExpression.Member.Name;
                    switch (memberExpression.Expression)
                    {
                        case ConstantExpression innerConstantExpression:
                            value = memberExpression.Member switch
                            {
                                PropertyInfo propertyInfo => (T)propertyInfo.GetValue(innerConstantExpression.Value),
                                FieldInfo fieldInfo => (T)fieldInfo.GetValue(innerConstantExpression.Value),
                                _ =>  throw new ArgumentException("unsupported expression type.")
                            };
                            break;
                        default:
                            value = value = expression.CompileFast()();
                            break;
                    }

                    break;
                case ConstantExpression constantExpression:
                    value = (T) constantExpression.Value;
                    break;

                default:
                    throw new ArgumentException("unsupported expression type.");
            }

            return (value, memberName);
        }
    }
}