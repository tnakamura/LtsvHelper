using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper.Configuration
{
    /// <summary>
    /// Configuration used for reading and writing LTSV data.
    /// </summary>
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
                RegisterClassMap(map);
                return map;
            }
        }

        /// <summary>
        /// Use a <see cref="LtsvClassMap{T}"/> to configure mappings.
        /// </summary>
        /// <typeparam name="TMap">The type of mapping class to use.</typeparam>
        /// <returns>The map.</returns>
        public TMap RegisterClassMap<TMap>()
            where TMap : LtsvClassMap
        {
            var map = ReflectionHelper.CreateInstance<TMap>();
            RegisterClassMap(map);
            return map;
        }

        void RegisterClassMap(LtsvClassMap map)
        {
            Ensure.ArgumentNotNull(map, nameof(map));

            Maps[map.ClassType] = map;
        }

        /// <summary>
        /// Unregisters the class map.
        /// </summary>
        /// <typeparam name="TMap">The map type to unregister.</typeparam>
        public void UnregisterClassMap<TMap>()
            where TMap : LtsvClassMap
        {
            var keys = Maps.Where(kvp => kvp.Value is TMap)
                .Select(kvp => kvp.Key)
                .ToList();
            foreach (var key in keys)
            {
                Maps.Remove(key);
            }
        }
    }
}
