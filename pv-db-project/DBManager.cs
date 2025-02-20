using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace pv_db_project
{
    internal class DBManager
    {
        private SqlConnectionStringBuilder consStringBuilder;
        private SqlConnection connection;

        //[0] table is used to get metadata
        //[1] table is used to get all databases on server
        //[2] table is used to get all the tables in database
        //every other tables will load later
        private List<DataTable> usedDatabase = new List<DataTable>(3);

        public void Connect(string server, string db_name, string login, string password)
        {
            consStringBuilder = new SqlConnectionStringBuilder();
            consStringBuilder.DataSource = server;
            consStringBuilder.InitialCatalog = db_name;
            consStringBuilder.UserID = login;
            consStringBuilder.Password = password;
            consStringBuilder.ConnectTimeout = 30;
            connection = new SqlConnection(consStringBuilder.ConnectionString);
            connection.Open();
            usedDatabase[0] = connection.GetSchema("MetaDataCollections");
            usedDatabase[1] = connection.GetSchema("Databases");
            usedDatabase[2] = connection.GetSchema("Tables");
        }
        public void Disconnect()
        {
            connection.Close();
        }
        public DataTable GetTablesInDB() { return usedDatabase[2]; }
        public void DeleteDBSchemeFromProgram()
        {
            usedDatabase = null;
        }
        public List<DBTable> GetDatabase()
        {
            return usedDatabase;
        }
        public DBTable GetTable(string tableName)
        {
            foreach (DBTable t in usedDatabase)
            {
                if(t.table_name.Equals(tableName)) return t;
            }

            throw new Exception("Table with this name doesn't exist.");
        }
    }
}
