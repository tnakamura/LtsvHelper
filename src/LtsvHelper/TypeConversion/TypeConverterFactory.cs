using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper.TypeConversion
{
    internal static class TypeConverterFactory
    {
        static readonly Dictionary<Type, ITypeConverter> _converters = new Dictionary<Type, ITypeConverter>();

        static TypeConverterFactory()
        {
            _converters.Add(typeof(string), new StringConverter());
            _converters.Add(typeof(int), new Int32Converter());
            _converters.Add(typeof(long), new Int64Converter());
        }

        public static ITypeConverter GetConverter(Type type)
        {
            if (_converters.ContainsKey(type))
            {
                return _converters[type];
            }
            else
            {
                return new DefaultTypeConverter();
            }
        }
    }
}
