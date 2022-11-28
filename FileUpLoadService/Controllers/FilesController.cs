using FileUpLoadService.DataType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSA.FW.Utilities.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpLoadService.Controllers
{
    /// <summary>
    /// study image upload in body para
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        [HttpPost]
        [Route("Test")]
        public IActionResult Upload([FromForm] FileDataDto dto)
        {
            // code responsible for file processing
            return Ok();
        }

        [HttpPost]
        [Route("UploadImageA")]
        public IActionResult UploadImageA([FromForm] UploadImageModelA dto)
        {
            // code responsible for file processing
            return Ok();
        }
    }
}
