using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pv_db_project
{
    internal class DBTable
    {
        public string table_name { get; }
        private List<Object> attributes;

        public DBTable()
        {
            attributes = new List<Object>();
        }
    }

    internal class Attribute<T>
    {
        private T type;

        public string name { get; }
        public List<T> Records { get; }

        public Attribute(T type, string name)
        {
            this.type = type;
            this.name = name;
            this.Records = new List<T>();
        }
    }
}

