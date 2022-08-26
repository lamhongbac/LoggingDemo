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
        
        /// <summary>
        /// ham nay dung de luu vao CSDL khi mobile bam nut SAVE, tren man hinh NEW Product
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        [Route("CreateMobStockItems")]
        [HttpPost]
        public async Task<IActionResult> CreateMobStockItems(List<MobStockMasterItem> items)
        {
            MobStockMasterHandler stockHandler = new MobStockMasterHandler(connectionString);
            List<MobStockMasterItem> result = await stockHandler.CreateMobStockItems(items);
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
