using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper.TypeConversion
{
    internal static class TypeConverterFactory
    {
        public static ITypeConverter GetConverter(Type type)
        {
            return new DefaultTypeConverter();
        }
    }
}
