using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using LtsvHelper.TypeConversion;

namespace LtsvHelper.Configuration
{
    /// <summary>
    /// Maps class properties to LTSV fields.
    /// </summary>
    public abstract class LtsvClassMap
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LtsvClassMap"/> class.
        /// </summary>
        /// <param name="classType">The class type.</param>
        protected LtsvClassMap(Type classType)
        {
            ClassType = classType;
            Constructor = ReflectionHelper.CreateConstructor(classType);
        }

        internal Type ClassType { get; private set; }

        internal Func<object> Constructor { get; set; }

        internal IList<LtsvPropertyMap> PropertyMaps { get; } = new List<LtsvPropertyMap>();
    }

    /// <summary>
    /// Maps class properties/fields to LTSV fields.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of class to map.</typeparam>
    public abstract class LtsvClassMap<T> : LtsvClassMap
    {
        /// <summary>
        /// Initializes a new instance of <see cref="LtsvClassMap{T}"/> class.
        /// </summary>
        protected LtsvClassMap()
            : base(typeof(T))
        {
        }

        /// <summary>
        /// Maps a property/field to a LTSV field.
        /// </summary>
        /// <param name="expression">The property/field to map.</param>
        /// <returns>The property/field mapping.</returns>
        public LtsvPropertyMap Map(Expression<Func<T, object>> expression)
        {
            var propertyInfo = (PropertyInfo)ReflectionHelper.GetMember(expression);

            var propertyMap = new LtsvPropertyMap();
            propertyMap.PropertyInfo = propertyInfo;
            propertyMap.Setter = ReflectionHelper.CreateSetter(typeof(T), propertyInfo);
            propertyMap.Getter = ReflectionHelper.CreateGetter(typeof(T), propertyInfo);
            propertyMap.TypeConverter = TypeConverterFactory.GetConverter(propertyInfo.PropertyType);
            propertyMap.Label(propertyInfo.Name);

            PropertyMaps.Add(propertyMap);
            return propertyMap;
        }
    }
}
