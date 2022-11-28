using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServiceWeb.Lib
{
    public class ImageResult
    {
        public ImageResult()
        {
            Path = new List<string>(); //tap hop image url  da upload, dung de view tren UI
            Thumb = new List<string>(); //tap hop image thumb url da upload  
            ErrMessage = "Failed";
        }
        public List<string> Path { get; set; }
        public List<string> Thumb { get; set; }
        public string ErrMessage { get; set; } //error neu co loi trong qua trinh upload
    }
}
