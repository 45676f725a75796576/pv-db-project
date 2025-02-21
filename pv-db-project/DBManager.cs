using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace pv_db_project
{
    internal class DBManager : IDisposable
    {
        private SqlConnectionStringBuilder consStringBuilder;
        private SqlConnection connection;

        public DBManager() { }

        public DBManager(string server, string db_name, string login, string password)
        {
            Connect(server, db_name, login, password);
        }

        public void Connect(string server, string db_name, string login, string password)
        {
            consStringBuilder = new SqlConnectionStringBuilder();
            consStringBuilder.DataSource = server;
            consStringBuilder.InitialCatalog = db_name;
            consStringBuilder.UserID = login;
            consStringBuilder.Password = password;
            consStringBuilder.ConnectTimeout = 30;
            consStringBuilder.TrustServerCertificate = true;
            connection = new SqlConnection(consStringBuilder.ConnectionString);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
