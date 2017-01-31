using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtsvHelper.Configuration;

namespace LtsvHelper.TypeConversion
{
    internal interface ITypeConverter
    {
        string ConvertToString(object value, ILtsvWriter writer, LtsvPropertyMap map);

        object ConvertFromString(string value, ILtsvReader reader, LtsvPropertyMap map);
    }
}
