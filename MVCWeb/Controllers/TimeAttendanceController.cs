using Microsoft.AspNetCore.Mvc;
using MVCWeb.Models;

namespace MVCWeb.Controllers
{
    public class TimeAttendanceController : Controller
    {
        private string _MACAddress = "3C219C3E544F";
        public IActionResult Index()
        {
            TimeAttendanceModel model = new TimeAttendanceModel();
            MacAddressUtil util = new MacAddressUtil();
            model.MacAddress = util.GetMACAddress();
            if (model.MacAddress != _MACAddress)
            {
                model.Message = "You are not in SASIN OFFICE TRAN HUNG DAO";
            }
            else
            {
                model.Message = "SASIN OFFICE TRAN HUNG DAO: Please select your operation";
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(TimeAttendanceModel model)
        {

            return RedirectToAction("Index");

        }
        /// <summary>
        /// Check In
        /// Time In
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckIn()
        {
            return View();
        }
        /// <summary>
        /// Save Check In
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckIn(TimeAttendanceModel model)
        {
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Check Out
        /// </summary>
        /// <returns></returns>
        public IActionResult CheckOut()
        {
            return View();
        }
        /// <summary>
        /// Save CheckOut
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckOut(TimeAttendanceModel model)
        {
            return RedirectToAction("Index");
        }
    }
}
