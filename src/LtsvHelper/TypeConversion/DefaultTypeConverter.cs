using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtsvHelper.Configuration;

namespace LtsvHelper.TypeConversion
{
    internal class DefaultTypeConverter : ITypeConverter
    {
        public object ConvertFromString(string value, ILtsvReader reader, LtsvPropertyMap map)
        {
            return Convert.ChangeType(value, map.PropertyInfo.PropertyType);
        }

        public string ConvertToString(object value, ILtsvWriter writer, LtsvPropertyMap map)
        {
            return value.ToString();
        }
    }
}
