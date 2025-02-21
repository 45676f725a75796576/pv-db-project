using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpDatabaseAPI.DatabaseObjects
{
    class Pojistovna : IDBObject
    {
        public static void Add(string column_name, string input, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand($"insert into pojistovna (nazev) values ('{input}');", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
        }
        public static void DeleteRecord(string column_name, string filter, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand($"delete pojistovna where {column_name} = '{filter}'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
        }

        public static string[] GetColumn(string input, SqlConnection conn)
        {
            List<string> result = new List<string>();
            SqlCommand cmd = new SqlCommand("select nazev from pojistovna", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) result.Add(reader.GetString(0));
            reader.Close();
            return result.ToArray();
        }
        public static string[] FindByColumn(string column_name, string filter, SqlConnection conn) 
        {
            List<string> result = new List<string>();
            SqlCommand cmd = new SqlCommand($"select nazev from pojistovna where {column_name} = {filter}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) result.Add(reader.GetString(0));
            reader.Close();
            return result.ToArray();
        }

        public static void Update(string f_column_name, string filter, string n_column_name, string new_value, SqlConnection conn) 
        {
            SqlCommand cmd = new SqlCommand($"update pojistovna set {n_column_name} = {new_value} where {f_column_name} = {filter}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
        }
    }
}
