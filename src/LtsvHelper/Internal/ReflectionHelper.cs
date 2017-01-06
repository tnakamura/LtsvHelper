using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper
{
    static class ReflectionHelper
    {
        public static T CreateInstance<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
