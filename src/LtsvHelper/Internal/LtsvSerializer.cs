using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LtsvHelper
{
    class LtsvSerializer : IDisposable
    {
        TextWriter TextWriter { get; }

        public LtsvSerializer(TextWriter textWriter)
        {
            TextWriter = textWriter;
        }

        public void Write(IEnumerable<KeyValuePair<string, string>> record)
        {
            var line = ToLtsvString(record);
            TextWriter.WriteLine(line);
        }

        private string ToLtsvString(IEnumerable<KeyValuePair<string, string>> record)
        {
            var sb = new StringBuilder();
            var first = true;
            foreach (var field in record)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append("\t");
                }
                sb.Append($"{field.Key}:{StringHelper.Escape(field.Value)}");
            }
            return sb.ToString();
        }

        #region IDisposable Support
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    TextWriter.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
