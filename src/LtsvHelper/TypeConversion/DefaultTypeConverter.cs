using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtsvHelper.Configuration;

namespace LtsvHelper.TypeConversion
{
    class DefaultTypeConverter : ITypeConverter
    {
        public virtual object ConvertFromString(string value, ILtsvReader reader, LtsvPropertyMap map)
        {
            return Convert.ChangeType(value, map.PropertyInfo.PropertyType);
        }

        public virtual string ConvertToString(object value, ILtsvWriter writer, LtsvPropertyMap map)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString();
        }
    }
}
