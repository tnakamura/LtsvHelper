using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper.Configuration
{
    public class LtsvPropertyMap
    {
        internal string LabelString { get; set; }

        internal PropertyInfo PropertyInfo { get; set; }

        internal Func<object, object> Getter { get; set; }

        internal Action<object, object> Setter { get; set; }

        public LtsvPropertyMap Label(string label)
        {
            Ensure.ArgumentNotNullOrEmpty(label, nameof(label));
            LabelString = label;
            return this;
        }
    }
}
