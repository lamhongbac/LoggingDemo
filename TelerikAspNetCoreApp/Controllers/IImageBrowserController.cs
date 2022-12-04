using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelerikAspNetCoreApp.Controllers
{
    public interface IImageBrowserController : IFileBrowserController
    {
        IActionResult Thumbnail(string path);
    }
}
