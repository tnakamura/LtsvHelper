using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper.Configuration
{
    /// <summary>
    /// Mapping info for a property/field to a LTSV field.
    /// </summary>
    public class LtsvPropertyMap
    {
        internal string LabelString { get; set; }

        internal PropertyInfo PropertyInfo { get; set; }

        internal Func<object, object> Getter { get; set; }

        internal Action<object, object> Setter { get; set; }

        /// <summary>
        /// When reading, is used to get the field.
        /// When writing, sets the label of the field.
        /// </summary>
        /// <param name="label">The label of the field.</param>
        /// <returns>The property/field mapping.</returns>
        public LtsvPropertyMap Label(string label)
        {
            Ensure.ArgumentNotNullOrEmpty(label, nameof(label));
            LabelString = label;
            return this;
        }
    }
}
