using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMSolution.Models
{
    public class AppConfig
    {
        public AppConfig()
        {
            DBConfiguration = new DBConfig();
        }
        public string AppName { get; set; }
        public string CompanyCode { get; set; } //cung chinh la customer code
       
        
        DBConfig dbConfiguration;
        public DBConfig DBConfiguration
        {
            get => dbConfiguration; set { dbConfiguration = value; }
        }
    }
}
