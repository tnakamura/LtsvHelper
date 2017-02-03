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
            _converters.Add(typeof(byte), new ByteConverter());
            _converters.Add(typeof(char), new CharConverter());
            _converters.Add(typeof(DateTime), new DateTimeConverter());
            _converters.Add(typeof(DateTimeOffset), new DateTimeOffsetConverter());
            _converters.Add(typeof(decimal), new DecimalConverter());
            _converters.Add(typeof(double), new DoubleConverter());
            _converters.Add(typeof(float), new SingleConverter());
            _converters.Add(typeof(Guid), new GuidConverter());
            _converters.Add(typeof(int), new Int32Converter());
            _converters.Add(typeof(long), new Int64Converter());
            _converters.Add(typeof(sbyte), new SByteConverter());
            _converters.Add(typeof(short), new Int16Converter());
            _converters.Add(typeof(string), new StringConverter());
            _converters.Add(typeof(TimeSpan), new TimeSpanConverter());
            _converters.Add(typeof(uint), new UInt32Converter());
            _converters.Add(typeof(ulong), new UInt64Converter());
            _converters.Add(typeof(ushort), new UInt16Converter());
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
