using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.IO;

namespace pv_db_project
{
    /// <summary>
    /// Manages the database connection and operations.
    /// </summary>
    internal class DBManager : IDisposable
    {
        private SqlConnectionStringBuilder consStringBuilder;
        private SqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DBManager"/> class.
        /// </summary>
        public DBManager() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DBManager"/> class with connection parameters and automatically connects to the database.
        /// </summary>
        /// <param name="server">The server name or IP address.</param>
        /// <param name="db_name">The name of the database.</param>
        /// <param name="login">The login username.</param>
        /// <param name="password">The login password.</param>
        public DBManager(string server, string db_name, string login, string password)
        {
            Connect(server, db_name, login, password);
        }

        /// <summary>
        /// Sets the connection parameters and opens a connection to the database.
        /// </summary>
        /// <param name="server">The server name or IP address.</param>
        /// <param name="db_name">The name of the database.</param>
        /// <param name="login">The login username.</param>
        /// <param name="password">The login password.</param>
        public void Connect(string server, string db_name, string login, string password)
        {
            consStringBuilder = new SqlConnectionStringBuilder();
            consStringBuilder.DataSource = server;
            consStringBuilder.InitialCatalog = db_name;
            consStringBuilder.UserID = login;
            consStringBuilder.Password = password;
            consStringBuilder.ConnectTimeout = 30;
            consStringBuilder.TrustServerCertificate = true;
            //consStringBuilder.IntegratedSecurity = true;
            connection = new SqlConnection(consStringBuilder.ConnectionString);
            connection.Open();
        }

        /// <summary>
        /// Gets the SQL connection object.
        /// </summary>
        public SqlConnection Connection { get { return connection; } }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        public void Dispose()
        {
            connection.Close();
        }

        /// <summary>
        /// Exports all data from the database to a JSON file.
        /// </summary>
        /// <param name="filePath">The file path where the JSON file will be saved.</param>
        public void ExportDatabaseToJson(string filePath)
        {
            var tables = new List<string>();

            // Get the list of tables in the database
            using (var cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }
            }

            var databaseData = new Dictionary<string, List<Dictionary<string, object>>>();

            // Get data from each table
            foreach (var table in tables)
            {
                using (var cmd = new SqlCommand($"SELECT * FROM {table}", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        var tableData = new List<Dictionary<string, object>>();
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            tableData.Add(row);
                        }
                        databaseData[table] = tableData;
                    }
                }
            }

            // Serialize the data to JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(databaseData, options);

            // Write the JSON to a file
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Exports all data from the database to a CSV file.
        /// </summary>
        /// <param name="filePath">The file path where the CSV file will be saved.</param>
        public void ExportDatabaseToCsv(string filePath)
        {
            var tables = new List<string>();

            // Get the list of tables in the database
            using (var cmd = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'", connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }
            }

            using (var writer = new StreamWriter(filePath))
            {
                foreach (var table in tables)
                {
                    writer.WriteLine($"Table: {table}");

                    using (var cmd = new SqlCommand($"SELECT * FROM {table}", connection))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            // Write column headers
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                writer.Write(reader.GetName(i));
                                if (i < reader.FieldCount - 1)
                                    writer.Write(",");
                            }
                            writer.WriteLine();

                            // Write rows
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    writer.Write(reader.GetValue(i));
                                    if (i < reader.FieldCount - 1)
                                        writer.Write(",");
                                }
                                writer.WriteLine();
                            }
                        }
                    }

                    writer.WriteLine();
                }
            }
        }
    }
}
