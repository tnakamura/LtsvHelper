using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using System.Threading.Tasks;

namespace LtsvHelper
{
    /// <summary>
    /// Used to read LTSV files.
    /// </summary>
    public class LtsvReader : ILtsvReader
    {
        readonly LtsvParser _parser;

        IDictionary<string, string> _currentRecord;

        /// <summary>
        /// Initializes a new instance of <see cref="LtsvReader"/> class.
        /// </summary>
        /// <param name="textReader">The reader.</param>
        public LtsvReader(TextReader textReader)
        {
            _parser = new LtsvParser(textReader);
            _currentRecord = null;
        }

        /// <summary>
        /// Advances the reader to the next record.
        /// </summary>
        /// <returns>True if there are more records, otherwise false.</returns>
        public bool Read()
        {
            _currentRecord = _parser.Read();
            if (_currentRecord != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the field at label.
        /// </summary>
        /// <param name="label">Tha label of the field.</param>
        /// <returns>The raw field.</returns>
        public string GetField(string label)
        {
            return _currentRecord[label];
        }

        /// <summary>
        /// Gets the field coverted to <typeparamref name="T"/> at label.
        /// </summary>
        /// <param name="label"></param>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <returns>The field coverted to <typeparamref name="T"/>.</returns>
        public T GetField<T>(string label)
        {
            return (T)Convert.ChangeType(_currentRecord[label], typeof(T));
        }

        /// <summary>
        /// Gets the field coverted to <see cref="object"/>.
        /// </summary>
        /// <param name="type">The type of the field.</param>
        /// <param name="label">The label of the field.</param>
        /// <returns>The field converted to <see cref="object"/></returns>
        object GetField(Type type, string label)
        {
            return Convert.ChangeType(_currentRecord[label], type);
        }

        /// <summary>
        /// Gets the record coverted into <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the record.</typeparam>
        /// <returns>The record converted to <typeparamref name="T"/>.</returns>
        public T GetRecord<T>()
        {
            var record = ReflectionHelper.CreateInstance<T>();
            var properties = typeof(T).GetRuntimeProperties()
                .Where(p => p.CanWrite);
            foreach (var p in properties)
            {
                var label = p.GetCustomAttribute<DataMemberAttribute>()?.Name ?? p.Name;
                p.SetValue(record, GetField(p.PropertyType, label));
            }
            return record;
        }

        /// <summary>
        /// Gets all the records in the LTSV file and converts each to
        /// <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the record.</typeparam>
        /// <returns>An <see cref="IEnumerable{T}"/> of records.</returns>
        public IEnumerable<T> GetRecords<T>()
        {
            while (Read())
            {
                yield return GetRecord<T>();
            }
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
                    _parser.Dispose();
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
