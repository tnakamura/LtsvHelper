using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtsvHelper.Configuration;

namespace LtsvHelper.TypeConversion
{
    class SingleConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string value, ILtsvReader reader, LtsvPropertyMap map)
        {
            if (value != null)
            {
                float result;
                if (float.TryParse(value, out result))
                {
                    return result;
                }
            }
            return base.ConvertFromString(value, reader, map);
        }
    }
}
