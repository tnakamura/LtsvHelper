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
            _converters.Add(typeof(bool), new BooleanConverter());
            _converters.Add(typeof(DateTime), new DateTimeConverter());
            _converters.Add(typeof(Guid), new GuidConverter());
            _converters.Add(typeof(int), new Int32Converter());
            _converters.Add(typeof(long), new Int64Converter());
            _converters.Add(typeof(short), new Int16Converter());
            _converters.Add(typeof(string), new StringConverter());
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
