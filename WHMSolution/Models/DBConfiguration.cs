using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMSolution.Models
{
    public class DBConfig
    {
        public string ProdConnectionString { get; set; }
        public string DevConnectionString { get; set; }
        public string ProductMode { get; set; }
        
        public string GetConnectionString()
        {
            string connectionString = DevConnectionString;
            if (ProductMode == "Prod")
            {
                connectionString = ProdConnectionString;
            }
            return connectionString;
        }
    }
}
