using System;

namespace LtsvHelper
{
    class Ensure
    {
        public static void ArgumentNotNull<T>(T arg, string paramName)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void ArgumentNotNullOrEmpty(string arg, string paramName)
        {
            ArgumentNotNull(arg, paramName);
            if (string.IsNullOrEmpty(arg))
            {
                throw new ArgumentException($"{paramName} is empty.", paramName);
            }
        }
    }
}
