using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtsvHelper.Configuration;

namespace LtsvHelper.TypeConversion
{
    class DateTimeOffsetConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string value, ILtsvReader reader, LtsvPropertyMap map)
        {
            if (value != null)
            {
                DateTimeOffset result;
                if ((map.TypeConverterOptions?.DateTimeStyle != null) &&
                    (map.TypeConverterOptions?.Format != null) &&
                    (map.TypeConverterOptions?.CultureInfo != null))
                {
                    if (DateTimeOffset.TryParseExact(
                        value,
                        map.TypeConverterOptions.Format,
                        map.TypeConverterOptions.CultureInfo,
                        map.TypeConverterOptions.DateTimeStyle.Value,
                        out result))
                    {
                        return result;
                    }
                }
                else
                {
                    if (DateTimeOffset.TryParse(value, out result))
                    {
                        return result;
                    }
                }
            }
            return base.ConvertFromString(value, reader, map);
        }
    }
}
