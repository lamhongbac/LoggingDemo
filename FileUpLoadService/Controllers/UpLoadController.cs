using FileUpLoadService.DataType;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MSA.FW.Utilities.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpLoadService.Controllers
{
    [Route("image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ImageServerConfiguration configuration;

        public ImageController(IWebHostEnvironment hostEnvironment, IOptions<ImageServerConfiguration> _apiSetting)
        {
            webHostEnvironment = hostEnvironment;
            configuration = _apiSetting.Value;

        }
        [HttpPost]
        [Route("uploadsingle")]
        public IActionResult UploadSingleImage([FromForm] UploadImageModelA model)
        {
            ImageResult uploadResult = new ImageResult();
            // code responsible for file processing
            try
            {
                IFormFile file = model.File;
                if (file == null || file.Length == 0)
                {
                    uploadResult.ErrMessage="No uploaded file found";
                }
                else
                {
                    UploadImageConfigA allConFig = model.Config;

                    ImageConfig ImageInfo = new ImageConfig()
                    {
                        FileName = allConFig.ImageFileName,
                        ImagePath = allConFig.ImagePath,
                        MaxHeight = allConFig.ImageMaxHeight,
                        MaxWidth = allConFig.ImageMaxWidth,
                        MBytes = allConFig.ImageMBytes
                    };
                    ImageConfig thumbInfo = null;
                    if (!(allConFig.ThumbMaxHeight == 0 && allConFig.ThumbMaxWidth == 0 && allConFig.ThumbMBytes == 0))
                    {
                        thumbInfo = new ImageConfig()
                        {
                            FileName = allConFig.ThumbFileName,
                            ImagePath = allConFig.ThumbPath,
                            MaxHeight = allConFig.ThumbMaxHeight,
                            MaxWidth = allConFig.ThumbMaxWidth,
                            MBytes = allConFig.ThumbMBytes
                        };
                    }
                    string startupPath = webHostEnvironment.ContentRootPath;
                    //duong dan cua Application
                    string imageRootPath = configuration.ImageRootPath; 
                    //Images

                    string fullImageRootPath = Path.Combine(startupPath, imageRootPath);
                    if (!Directory.Exists(fullImageRootPath))
                    {
                        Directory.CreateDirectory(fullImageRootPath);
                    }

                    APIHelper.SaveImage(uploadResult, ImageInfo, thumbInfo, file, fullImageRootPath);
                }
            }
            catch(Exception ex)
            {
                uploadResult.ErrMessage = ex.Message.ToString();
            }

            return Ok(uploadResult);
        }

        
    }
}
