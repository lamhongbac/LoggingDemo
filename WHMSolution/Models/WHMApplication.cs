using AutoMapper;
using Microsoft.Extensions.Options;
using SCMDAL.DataHandler;
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
        MobStockMasterHandler _mobStockMasterDatabase;
        MobStockTransHandler _mobStockTransDatabase;
        public MobStockMasterHandler StockMasterDataBase { get => _mobStockMasterDatabase; }
        
            public MobStockTransHandler StockTransDataBase { get => _mobStockTransDatabase; }
        public string UserID { get; set; }
        public WHMApplication(IOptions<AppConfig> settings, 
            MobStockMasterHandler stockMasterdatabase,
            MobStockTransHandler mobTransDatabase)
        {
            UserID = "DemoUser";//hard code
            _appConfig = settings.Value;
            _mobStockMasterDatabase = stockMasterdatabase;
            _mobStockTransDatabase = mobTransDatabase;
            utilities = new Utilities(this);
        }
        AppConfig _appConfig;

        public AppConfig AppConfig { get => _appConfig; set => _appConfig = value; }
    }
}
