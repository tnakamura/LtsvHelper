using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;

namespace LtsvHelper
{
    internal static class ReflectionHelper
    {
        public static T CreateInstance<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public static MemberInfo GetMember<TModel>(Expression<Func<TModel, object>> expression)
        {
            var member = GetMemberExpression(expression.Body).Member;
            var property = member as PropertyInfo;
            if (property != null)
            {
                return property;
            }

            var field = member as FieldInfo;
            if (field != null)
            {
                return field;
            }

            throw new ArgumentException($"'{member.Name}' is not a property/field.", nameof(expression));
        }

        static MemberExpression GetMemberExpression(Expression expression)
        {
            MemberExpression memberExpression = null;
            if (expression.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression as MemberExpression;
            }
            return memberExpression;
        }

        public static Func<object> CreateConstructor(Type classType)
        {
            var lambda = Expression.Lambda<Func<object>>(
                Expression.Convert(
                    Expression.New(classType),
                    typeof(object)));
            return lambda.Compile();
        }

        public static Func<object, object> CreateGetter(Type classType, PropertyInfo propertyInfo)
        {
            var target = Expression.Parameter(typeof(object), "target");

            var lambda = Expression.Lambda<Func<object, object>>(
                Expression.Convert(
                    Expression.PropertyOrField(
                        Expression.Convert(
                            target,
                            classType),
                        propertyInfo.Name),
                    typeof(object)),
                target);

            return lambda.Compile();
        }

        public static Action<object, object> CreateSetter(Type classType, PropertyInfo propertyInfo)
        {
            var target = Expression.Parameter(typeof(object), "target");
            var value = Expression.Parameter(typeof(object), "value");

            var lambda = Expression.Lambda<Action<object, object>>(
                Expression.Assign(
                    Expression.PropertyOrField(
                        Expression.Convert(
                            target,
                            classType),
                        propertyInfo.Name),
                    Expression.Convert(
                        value,
                        propertyInfo.PropertyType)),
                target,
                value);

            return lambda.Compile();
        }
    }
}
