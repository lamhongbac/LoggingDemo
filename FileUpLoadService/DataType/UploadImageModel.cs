using Microsoft.AspNetCore.Http;
using MSA.FW.Utilities.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpLoadService.DataType
{
    public class UploadImageModel
    {
        public UploadImageModel()
        {
            Config = new UploadImageConfig();
        }
        public UploadImageConfig Config { get; set; }
        public IFormFile File { get; set; }
    }

    public class UploadImageModelA
    {
        public UploadImageModelA()
        {
            Config = new UploadImageConfigA();
        }
        public UploadImageConfigA Config { get; set; }
        public IFormFile File { get; set; }
    }
}
