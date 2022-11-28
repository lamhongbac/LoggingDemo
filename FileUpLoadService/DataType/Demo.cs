using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpLoadService.DataType
{

    public class FileDataDto
    {
        public IFormFile FileToUpload1 { get; set; }


        public DataDto Data { get; set; }
    }
    public class DataDto
    {
        public string Name { get; set; }
        public string[] Tags { get; set; }
        public ChildDataDto ChildData { get; set; }
    }

    public class ChildDataDto
    {
        public string Description { get; set; }
    }
}
