using AutoMapper;
using Ckeditor5AspNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NewsCMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ckeditor5AspNetCore.Controllers
{
    public class DisplayNewsController : Controller
    {
        private readonly ILogger<DisplayNewsController> _logger;
        IConfiguration _configuration;
        IMapper _mapper;
        public DisplayNewsController(ILogger<DisplayNewsController> logger,
            IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            string conn = _configuration.GetConnectionString("DevDB");
            DataOperation dataOP = new DataOperation(conn);
            List<NewsData> datas = await dataOP.ReadList("", null, "ID");
            List<NewsModel> models = _mapper.Map<List<NewsModel>>(datas);
            return View(models);
        }
        public async Task<IActionResult> Edit(int id)
        {
            string conn = _configuration.GetConnectionString("DevDB");
            DataOperation dataOP = new DataOperation(conn);
            
            string whereString = "ID=@ID";
            var para = new { ID = id };
            NewsData data = await dataOP.Read(whereString, para);
            NewsModel model = _mapper.Map<NewsModel>(data);
            return View(model);
        }
    }
}
