using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper.Configuration
{
    internal class DefaultLtsvClassMap : LtsvClassMap
    {
        public DefaultLtsvClassMap(Type classType)
            : base(classType)
        {
            AutoMap();
        }

        private void AutoMap()
        {
            Constructor = ReflectionHelper.CreateConstructor(ClassType);

            var properties = ClassType.GetRuntimeProperties();
            foreach (var p in properties)
            {
                var map = new LtsvPropertyMap()
                {
                    PropertyInfo = p,
                    Setter = ReflectionHelper.CreateSetter(ClassType, p),
                    Getter = ReflectionHelper.CreateGetter(ClassType, p)
                };
                map.LabelString = p.Name;
                PropertyMaps.Add(map);
            }
        }
    }
}
