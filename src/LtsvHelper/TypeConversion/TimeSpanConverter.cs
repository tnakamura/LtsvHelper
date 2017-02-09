using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtsvHelper.Configuration;

namespace LtsvHelper.TypeConversion
{
    class TimeSpanConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string value, ILtsvReader reader, LtsvPropertyMap map)
        {
            if (value != null)
            {
                TimeSpan result;
                if ((map.TypeConverterOptions?.Format != null) &&
                    (map.TypeConverterOptions?.CultureInfo != null))
                {
                    if (TimeSpan.TryParseExact(
                        value,
                        map.TypeConverterOptions.Format,
                        map.TypeConverterOptions.CultureInfo,
                        out result))
                    {
                        return result;
                    }
                }
                else
                {
                    if (TimeSpan.TryParse(value, out result))
                    {
                        return result;
                    }
                }
            }
            return base.ConvertFromString(value, reader, map);
        }
    }
}
