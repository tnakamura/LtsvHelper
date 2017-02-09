using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace LtsvHelper.TypeConversion
{
    /// <summary>
    /// Options used when doing type conversion.
    /// </summary>
    internal class TypeConverterOptions
    {
        static readonly string[] DefaultBooleanTrueValues = { "yes", "y" };

        static readonly string[] DefaultBooleanFalseValues = { "no", "n" };

        static readonly string[] DefaultNullValues = { "null", "NULL" };

        /// <summary>
        /// Gets or sets the culture info.
        /// </summary>
        public CultureInfo CultureInfo { get; set; }

        /// <summary>
        /// Gets or sets the date time style.
        /// </summary>
        public DateTimeStyles? DateTimeStyle { get; set; }

        /// <summary>
        /// Gets or sets the number style.
        /// </summary>
        public NumberStyles? NumberStyle { get; set; }

        /// <summary>
        /// Gets or sets the string format.
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Gets the list of values that can be
        /// used to represent a boolean of true.
        /// </summary>
        public IList<string> BooleanTrueValues { get; } = new List<string>(DefaultBooleanTrueValues);

        /// <summary>
        /// Gets the list of values that can be
        /// used to represent a boolean of false.
        /// </summary>
        public IList<string> BooleanFalseValues { get; } = new List<string>(DefaultBooleanFalseValues);

        /// <summary>
        /// Gets the list of values that can be used to represent a null value.
        /// </summary>
        public IList<string> NullValues { get; } = new List<string>(DefaultNullValues);
    }
}
