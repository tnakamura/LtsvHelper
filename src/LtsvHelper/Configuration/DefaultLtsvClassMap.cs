using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtsvHelper.TypeConversion;

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
                AddPropertyMap(ClassType, p);
            }
        }
    }
}
