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

        public static MemberInfo GetMemberInfo<T>(Expression<Func<T, object>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
            {
                return member.Member;
            }
            throw new ArgumentException($"{nameof(expression)} is not supported expression.");
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
