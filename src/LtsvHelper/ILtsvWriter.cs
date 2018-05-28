using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LtsvHelper
{
    /// <summary>
    /// Defines methods used to write to a LTSV file.
    /// </summary>
    public interface ILtsvWriter : IDisposable
    {
        /// <summary>
        /// Writes the field to the LTSV file.
        /// </summary>
        /// <param name="label">The label of field</param>
        /// <param name="value">The value of field</param>
        void WriteField(string label, string value);

        /// <summary>
        /// Writes the field to the LTSV file.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="label">The label of field</param>
        /// <param name="value">The value of field</param>
        void WriteField<T>(string label, T value);

        /// <summary>
        /// Ends writing of the current record and starts a new record.
        /// This needs to be called to serialize the row to the writer.
        /// </summary>
        void NextRecord();

        /// <summary>
        /// Ends writing of the current record and starts a new record.
        /// This needs to be called to serialize the row to the writer.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous write operation.
        /// </returns>
        Task NextRecordAsync();

        /// <summary>
        /// Writes the record to the LTSV file.
        /// </summary>
        /// <typeparam name="T">The type of the record.</typeparam>
        /// <param name="record">The record to write.</param>
        void WriteRecord<T>(T record);

        /// <summary>
		/// Writes the list of records to the LTSV file.
        /// </summary>
        /// <typeparam name="T">The type of the record.</typeparam>
		/// <param name="records">The list of records to write.</param>
        void WriteRecords<T>(IEnumerable<T> records);
    }
}
