using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpDatabaseAPI.DatabaseObjects
{
    class Model : IDBObject
    {
        public static void Add(string column_name, string input, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand($"insert into model ('{column_name}') values ('{input}');", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
        }
        public static void DeleteRecord(string column_name, string filter, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand($"delete model where {column_name} = {filter}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
        }
        public static string[] GetColumn(string column_name, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand($"select '{column_name}' from model", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) throw new Exception("No data");
            List<string> result = new List<string>();
            while(reader.Read())
            {
                result.Add("" + reader.GetValue(0));
            }
            reader.Close();
            return result.ToArray();
        }

        public static string[] FindByColumn(string column_name, string filter, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand($"select concat(znacka, ' ', typ, ' ', varianta, ' ', verze, ' ', oznaceni, ' ', poc_mis) from model where {column_name} = '{filter}'", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) throw new Exception("No data");
            List<string> result = new List<string>();
            while (reader.Read())
            {
                var value = reader.GetValue(0);
                result.Add(value != null ? value.ToString() : "null");
            }
            reader.Close();
            return result.ToArray();
        }
        public static void Update(string f_column_name, string filter, string n_column_name, string new_value, SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand($"update model set {n_column_name} = {new_value} where {f_column_name} = {filter}", conn);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Close();
        }
    }
}
