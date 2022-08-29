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
    public class StockTransController : ControllerBase
    {
        private string connectionString = "Data Source=203.205.30.159,85;User ID=sa;Password=@saomai2022;persist security info=True;initial catalog=FnBSCM";
        /// <summary>
        /// Luu data tu mob vao trong CSDL
        /// Tra ve Danh sach cac GUID ID da cap nhat thanh cong
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        [Route("CreateStockTrans")]
        [HttpPost]
        public async Task<IActionResult> CreateStockTrans(List<MobStockTrans> items)
        {
            MobStockTransHandler stockHandler = new MobStockTransHandler(connectionString);
            foreach (var item in items)
            {
                //kiem tra va loai bo cac item kg can
                item.SyncDate = DateTime.Now;
                item.DataState = "Posted";
            }
            var result = await stockHandler.CreateStockTrans(items);
            return Ok(result);
        }
       
    }
}
