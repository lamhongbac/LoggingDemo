using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DataPool
    {
        public static DataSync SyncManagement { get; }
        public static List<StoreData> StoreDatas { get; }
        public static List<BrandData> BrandDatas { get; }
        public DataPool() { }


    }
}
