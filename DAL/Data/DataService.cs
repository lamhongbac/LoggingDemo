using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DataService
    {
        
        
     
        StoreDataService _storeDataService;
        public DataService(StoreDataService storeDataService) 
        { 
                _storeDataService = new StoreDataService(); 
        }   

        public List<StoreData> GetStoreData()
        {
            
            ClientReloadData storeRelodaInfo = new ClientReloadData();
            if (DataPool.SyncManagement == null)
            {
                DataPool.CreateTracking();
            }
                
            if (!DataPool.SyncManagement.IsInit(EReload.Outlet))
            {
                _storeDataService.InitData();
            }
            else
            {
                _storeDataService.GetLastData(DataPool.SyncManagement.GetLastUpdateDate(EReload.Outlet));
            }
            return DataPool.StoreDatas;
        }

        
    }
}
