using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using LtsvHelper.Configuration;

namespace LtsvHelper
{
    /// <summary>
    /// Used to write LTSV files.
    /// </summary>
    public class LtsvWriter : ILtsvWriter
    {
        readonly LtsvSerializer _serializer;

        readonly LtsvConfiguration _configuration;

        readonly List<KeyValuePair<string, string>> _currentRecord;

        /// <summary>
        /// Initializes a new instance of <see cref="LtsvWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The writer.</param>
        public LtsvWriter(TextWriter textWriter)
            : this(textWriter, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="LtsvWriter"/> class.
        /// </summary>
        /// <param name="textWriter">The writer.</param>
        public LtsvWriter(TextWriter textWriter, LtsvConfiguration configuration)
        {
            Ensure.ArgumentNotNull(textWriter, nameof(textWriter));

            _serializer = new LtsvSerializer(textWriter);
            _currentRecord = new List<KeyValuePair<string, string>>();
            _configuration = configuration ?? new LtsvConfiguration();
        }

        /// <summary>
        /// Writes the field to the LTSV file.
        /// </summary>
        /// <param name="label">The label of field</param>
        /// <param name="value">The value of field</param>
        public void WriteField(string label, string value)
        {
            Ensure.ArgumentNotNullOrEmpty(label, nameof(label));

            WriteField<string>(label, value);
        }

        /// <summary>
        /// Writes the field to the LTSV file.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="label">The label of field</param>
        /// <param name="value">The value of field</param>
        public void WriteField<T>(string label, T value)
        {
            Ensure.ArgumentNotNullOrEmpty(label, nameof(label));

            _currentRecord.Add(new KeyValuePair<string, string>(label, value.ToString()));
        }

        /// <summary>
        /// Ends writing of the current record and starts a new record.
        /// This needs to be called to serialize the row to the writer.
        /// </summary>
        public void NextRecord()
        {
            _serializer.Write(_currentRecord);
            _currentRecord.Clear();
        }

        /// <summary>
        /// Writes the record to the LTSV file.
        /// </summary>
        /// <typeparam name="T">The type of the record.</typeparam>
        /// <param name="record">The record to write.</param>
        public void WriteRecord<T>(T record)
        {
            Ensure.ArgumentNotNull(record, nameof(record));

            var classMap = _configuration.GetClassMap(typeof(T));
            foreach (var p in classMap.PropertyMaps)
            {
                var label = p.LabelString;
                var value = p.Getter(record);
                _currentRecord.Add(new KeyValuePair<string, string>(label, value.ToString()));
            }

            NextRecord();
        }

        #region IDisposable Support
        private bool _disposed = false;

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">True if the instance needs to be disposed of.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _serializer.Dispose();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
