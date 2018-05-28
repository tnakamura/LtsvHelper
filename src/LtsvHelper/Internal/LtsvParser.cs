using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LtsvHelper
{
    class LtsvParser : IDisposable
    {
        TextReader TextReader { get; }

        public LtsvParser(TextReader textReader)
        {
            TextReader = textReader;
        }

        public IDictionary<string, string> Read()
        {
            var record = TextReader.ReadLine();

            if (string.IsNullOrEmpty(record))
            {
                return null;
            }

            var fields = Parse(record);
            return fields;
        }

        static IDictionary<string, string> Parse(string record)
        {
            var fields = record.Split('\t')
                .Select(s => s.Split(new char[] { ':' }, 2))
                .Where(a => a.Length == 2)
                .ToDictionary(
                    a => a[0],
                    a => StringHelper.Unescape(a[1])
                );
            return fields;
        }

        public async Task<IDictionary<string, string>> ReadAsync()
        {
            var record = await TextReader.ReadLineAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(record))
            {
                return null;
            }

            var fields = Parse(record);
            return fields;
        }

        #region IDisposable Support
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    TextReader.Dispose();
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
