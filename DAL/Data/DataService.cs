using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    internal class DataService
    {
      static  DataPool _dataPool;
        StoreDataService storeDataService;
        public DataService() { storeDataService = new StoreDataService(); }

        public List<StoreData> GetStoreData()
        {
            List<StoreData> storeDatas=new List<StoreData>
            if (!DataPool.SyncManagement.IsInit(EReload.Outlet))
            {
                storeDatas = storeDataService.InitData();
            }
            else
            {
                storeDatas = storeDataService.GetLastData(DataPool.SyncManagement.GetLastUpdateDate(EReload.Outlet));
            }
            return storeDatas;
        }

        private List<StoreData> InitStoreData()
        {
            throw new NotImplementedException();
        }
    }
}
