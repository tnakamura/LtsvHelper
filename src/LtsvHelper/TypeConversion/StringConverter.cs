using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtsvHelper.Configuration;

namespace LtsvHelper.TypeConversion
{
    class StringConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string value, ILtsvReader reader, LtsvPropertyMap map)
        {
            return value;
        }
    }
}
