using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pv_db_project
{
    internal class DBManager
    {
        private List<DBTable> usedDatabase;
        public void Connect(string server, string login, string password)
        {

        }
        public void Disconnect() 
        {

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
