using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

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
            consStringBuilder.IntegratedSecurity = true;
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
    }
}
