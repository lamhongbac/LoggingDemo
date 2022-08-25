using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCMDAL.DataHandler;
using SCMDAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WHMAPI.Models;

namespace WHMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private string connectionString = "Data Source=203.205.30.159,85;User ID=sa;Password=@saomai2022;persist security info=True;initial catalog=FnBSCM";
        [Route("CreateStockItems")]
        [HttpPost]
        public async Task<IActionResult> CreateStockItems(List<MobStockMasterItem> items)
        {
            MobStockMasterHandler stockHandler = new MobStockMasterHandler(connectionString);
            List<MobStockMasterItem> result = await stockHandler.CreateStockItems(items);
            return Ok(result);
        }

        [Route("SyncStockItems")]
        [HttpPost]
        public async Task<IActionResult> SyncStockItems(SyncStockItemModel model)
        {
            MobSyncHandler stockHandler = new MobSyncHandler(connectionString);
            SyncMobStockItemResult result = await stockHandler.MobSyncStockItem(model);
            return Ok(result);
        }
    }
}
