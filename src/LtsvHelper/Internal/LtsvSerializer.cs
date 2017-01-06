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
            var sb = new StringBuilder();
            foreach (var field in record.Take(1))
            {
                sb.Append($"{field.Key}:{StringHelper.Escape(field.Value)}");
            }
            foreach (var field in record.Skip(1))
            {
                sb.Append($"\t{field.Key}:{StringHelper.Escape(field.Value)}");
            }
            TextWriter.WriteLine(sb);
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
