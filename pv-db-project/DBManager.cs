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
        private DataRow selectedRow;

        //[0] is table name
        //[1] is column name
        //[2] operation type
        private List<string> commandBuilder = new List<string>(5);

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
        public void DoOperation(DBOperations operation, string operand)
        {
            switch (operation)
            {
                // sets table to edit
                case DBOperations.SELECT_TABLE:
                    commandBuilder[0] = operand; break;
                    
                // sets column to edit
                case DBOperations.SELECT_COLUMN_IN_TABLE:
                    commandBuilder[1] = operand; break;

                // sets operation to select rows
                case DBOperations.GET_ROWS_FROM_COLUMN:
                    commandBuilder[2] = "select"; break;

                // sets previous value of row, so when setting row you will not select every existing row in column
                case DBOperations.SELECT_ROW:
                    commandBuilder[3] = operand; break;

                // sets operation to update and operand is a new value for selected row
                case DBOperations.SET_ROW:
                    commandBuilder[2] = "update";
                    commandBuilder[4] = operand;
                    break;

                // SQL_COMMAND means that you will send command directly to the database and operand is meaned as a full sql command
                case DBOperations.SQL_COMMAND:
                    SqlCommand cmd = new SqlCommand(operand, connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    break;

            }
        }
    }

    public enum DBOperations
    {
        SELECT_TABLE = 0,
        SELECT_COLUMN_IN_TABLE = 1,
        GET_COLUMNS_FROM_TABLE = 2,
        GET_ROWS_FROM_COLUMN = 3,
        SELECT_ROW = 4,
        SET_ROW = 5,
        ADD_TABLE = 6,
        ADD_COLUMN = 7,
        ADD_RECORD = 8
        SQL_COMMAND = 9,
        DELETE_RECORD = 10,
        DELETE_TABLE = 11,
        EXECUTE = 12
    }
}
