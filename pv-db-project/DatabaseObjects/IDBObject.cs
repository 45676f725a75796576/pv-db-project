using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpDatabaseAPI.DatabaseObjects
{
    // Interface for all database objects
    interface IDBObject
    {
        // Add a new record to the table
        public static void Add (string column_name, string input, SqlConnection conn) { }
        // Delete a record from the table, column_name is the column to filter, if its value is equal to filter, the record is deleted
        public static void DeleteRecord (string column_name, string filter, SqlConnection conn) { }
        // Get all values from a column, input is a column name
        public static string[] GetColumn (string input, SqlConnection conn) { return null; }
        // Find all records where column_name is equal to filter
        public static string[] FindByColumn (string column_name, string filter, SqlConnection conn) { return null; }
        // Update a record in the table, f_column_name is the column to filter, if its value is equal to filter, the record is updated to a new_value
        public static void Update (string f_column_name, string filter, string n_column_name, string new_value, SqlConnection conn) { }
    }
}
