using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
   public class AppSetting
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }
        
        [Indexed]
        public string AppGroup { get; set; }
        public string AppKey { get; set; }
        public string AppValue { get; set; }
    }
}
