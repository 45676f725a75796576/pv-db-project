using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpDatabaseAPI.DatabaseObjects
{
    /// <summary>
    /// Interface for all database objects.
    /// </summary>
    interface IDBObject
    {
        /// <summary>
        /// Adds a new record to the table.
        /// </summary>
        /// <param name="column_name">The name of the column.</param>
        /// <param name="input">The value to insert.</param>
        /// <param name="conn">The SQL connection.</param>
        public static void Add(string column_name, string input, SqlConnection conn) { }

        /// <summary>
        /// Deletes a record from the table.
        /// </summary>
        /// <param name="column_name">The column to filter by.</param>
        /// <param name="filter">The value to filter by.</param>
        /// <param name="conn">The SQL connection.</param>
        public static void DeleteRecord(string column_name, string filter, SqlConnection conn) { }

        /// <summary>
        /// Gets all values from a column.
        /// </summary>
        /// <param name="input">The name of the column.</param>
        /// <param name="conn">The SQL connection.</param>
        /// <returns>An array of values from the specified column.</returns>
        public static string[] GetColumn(string input, SqlConnection conn) { return null; }

        /// <summary>
        /// Finds all records where the specified column is equal to the filter value.
        /// </summary>
        /// <param name="column_name">The column to filter by.</param>
        /// <param name="filter">The value to filter by.</param>
        /// <param name="conn">The SQL connection.</param>
        /// <returns>An array of matching records.</returns>
        public static string[] FindByColumn(string column_name, string filter, SqlConnection conn) { return null; }

        /// <summary>
        /// Updates a record in the table.
        /// </summary>
        /// <param name="f_column_name">The column to filter by.</param>
        /// <param name="filter">The value to filter by.</param>
        /// <param name="n_column_name">The column to update.</param>
        /// <param name="new_value">The new value to set.</param>
        /// <param name="conn">The SQL connection.</param>
        public static void Update(string f_column_name, string filter, string n_column_name, string new_value, SqlConnection conn) { }
    }
}
