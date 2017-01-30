using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace LtsvHelper.Configuration
{
    public abstract class LtsvClassMap
    {
        protected LtsvClassMap(Type classType)
        {
            ClassType = classType;
            Constructor = ReflectionHelper.CreateConstructor(classType);
        }

        internal Type ClassType { get; private set; }

        internal Func<object> Constructor { get; set; }

        internal IList<LtsvPropertyMap> PropertyMaps { get; } = new List<LtsvPropertyMap>();
    }

    public abstract class LtsvClassMap<T> : LtsvClassMap
    {
        protected LtsvClassMap()
            : base(typeof(T))
        {
        }

        public LtsvPropertyMap Map(Expression<Func<T, object>> expression)
        {
            var propertyInfo = (PropertyInfo)ReflectionHelper.GetMemberInfo(expression);

            var propertyMap = new LtsvPropertyMap();
            propertyMap.PropertyInfo = propertyInfo;
            propertyMap.Setter = ReflectionHelper.CreateSetter(typeof(T), propertyInfo);
            propertyMap.Getter = ReflectionHelper.CreateGetter(typeof(T), propertyInfo);

            PropertyMaps.Add(propertyMap);
            return propertyMap;
        }
    }
}
