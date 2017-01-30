using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper.Configuration
{
    public class LtsvConfiguration
    {
        private Dictionary<Type, LtsvClassMap> Maps { get; } = new Dictionary<Type, LtsvClassMap>();

        internal LtsvClassMap GetClassMap(Type classType)
        {
            if (Maps.ContainsKey(classType))
            {
                return Maps[classType];
            }
            else
            {
                var map = new DefaultLtsvClassMap(classType);
                return RegisterClassMap(map);
            }
        }

        public TMap RegisterClassMap<TMap>()
            where TMap : LtsvClassMap
        {
            var map = ReflectionHelper.CreateInstance<TMap>();
            RegisterClassMap(map);
            return map;
        }

        public LtsvClassMap RegisterClassMap(LtsvClassMap map)
        {
            Ensure.ArgumentNotNull(map, nameof(map));

            Maps[map.ClassType] = map;
            return map;
        }
    }
}
