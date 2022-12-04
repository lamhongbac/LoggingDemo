using Microsoft.AspNetCore.Mvc;
using NewsCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsCMS.Controllers
{
    public class CkEditorController : Controller
    {
        public CkEditorController()
        {

        }
        //DataOperation
        public IActionResult Index()
        {
            List<CKEditor> data = new List<CKEditor>();
            return View(data);
        }
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(CKEditor model)
        {

            return View(model);
        }
    }
}
