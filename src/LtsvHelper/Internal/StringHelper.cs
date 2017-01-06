using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper
{
    static class StringHelper
    {
        public static string Escape(string value)
        {
            return value
                .Replace("\\", "\\\\")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\t", "\\t");
        }

        public static string Unescape(string value)
        {
            return value
                .Replace("\\t", "\t")
                .Replace("\\r", "\r")
                .Replace("\\n", "\n")
                .Replace("\\\\", "\\");
        }
    }
}
