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
            
            ClientReloadData storeRelodaInfo = new ClientReloadData();
            if (!DataPool.SyncManagement.IsInit(EReload.Outlet))
            {
                 storeDataService.InitData();
                
                
                

            }
            else
            {
                storeDataService.GetLastData(DataPool.SyncManagement.GetLastUpdateDate(EReload.Outlet));
            }
            return DataPool.StoreDatas;
        }

        private List<StoreData> InitStoreData()
        {
            throw new NotImplementedException();
        }
    }
}
