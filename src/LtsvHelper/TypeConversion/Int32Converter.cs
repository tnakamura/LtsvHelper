using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtsvHelper.Configuration;

namespace LtsvHelper.TypeConversion
{
    class Int32Converter : DefaultTypeConverter
    {
        public override object ConvertFromString(string value, ILtsvReader reader, LtsvPropertyMap map)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return base.ConvertFromString(value, reader, map);
            }
        }
    }
}
