using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpDatabaseAPI.DatabaseObjects
{
    interface IDBObject
    {
        public static void Add (string column_name, string input, SqlConnection conn) { }
        public static void DeleteRecord (string column_name, string filter, SqlConnection conn) { }
        public static string[] GetColumn (string input, SqlConnection conn) { return null; }
        public static string[] FindByColumn (string column_name, string filter, SqlConnection conn) { return null; }
        public static void Update (string f_column_name, string filter, string n_column_name, string new_value, SqlConnection conn) { }
    }
}
