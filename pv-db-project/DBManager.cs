using System;
using System.Collections.Generic;
using System.Linq;
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
        private DataRow selectedRow;

        //[0] is table name
        //[1] is column name
        //[2] operation type
        private List<string> commandBuilder = new List<string>(5);

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
            connection = new SqlConnection(consStringBuilder.ConnectionString);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
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
                    commandBuilder[2] = "select";
                    if(operand != null) commandBuilder[3] = operand;
                    break;

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
                    SqlCommand _cmd = new SqlCommand(operand, connection);
                    SqlDataReader reader = _cmd.ExecuteReader();
                    break;

                // sets operation and value of column you have to have to delete record
                case DBOperations.DELETE_RECORD:
                    commandBuilder[2] = "delete";
                    break;

                case DBOperations.EXECUTE:
                    string cmd;
                    switch (commandBuilder[2]) {
                        case "select":
                            if (commandBuilder[3] == null) cmd = $"{commandBuilder[2]} {commandBuilder[1]} from {commandBuilder[0]}";
                            else cmd = $"{commandBuilder[2]} {commandBuilder[1]} from {commandBuilder[0]} where {commandBuilder[1]} = {commandBuilder[3]}";
                            SqlCommand command = new SqlCommand(cmd, connection);
                            SqlDataReader reader2 = command.ExecuteReader();
                            break;
                        case "update":
                            if (commandBuilder[3] == null) cmd = $"{commandBuilder[2]} {commandBuilder[0]} set {commandBuilder[1]} = {commandBuilder[4]}";
                            else cmd = $"{commandBuilder[2]} {commandBuilder[0]} set {commandBuilder[1]} = {commandBuilder[4]} where {commandBuilder[1]} = {commandBuilder[3]}";
                            SqlCommand command2 = new SqlCommand (cmd, connection);
                            SqlDataReader reader3 = command2.ExecuteReader();
                            break;
                        case "delete":
                            cmd = $"delete from {commandBuilder[0]} where {commandBuilder[1]} = {commandBuilder[3]}";
                            break;
                    }
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
        ADD_RECORD = 8,
        SQL_COMMAND = 9,
        DELETE_RECORD = 10,
        DELETE_TABLE = 11,
        EXECUTE = 12
    }
}
