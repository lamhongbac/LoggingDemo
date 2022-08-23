using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMSolution.Models
{
    public class WHMApplication
    {
        Utilities utilities;
        public Utilities AppUtil { get => utilities; }
           SQLDataBase _database;
        public SQLDataBase DataBase { get => _database; }
        public string UserID { get; set; }
        public WHMApplication(IOptions<AppConfig> settings, SQLDataBase database)
        {
            UserID = "DemoUser";//hard code
            _appConfig = settings.Value;
            _database = database;
            utilities = new Utilities(this);
        }
        AppConfig _appConfig;

        public AppConfig AppConfig { get => _appConfig; set => _appConfig = value; }
    }
}
