using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ConnnectionStringManager : IConnnectionStringManager
    {
        public string ConnectionString { get; }
        public ConnnectionStringManager(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
