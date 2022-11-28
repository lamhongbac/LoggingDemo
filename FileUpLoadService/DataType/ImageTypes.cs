using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUpLoadService.DataType
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
    public class ImageConfig
    {
        public string FileName { get; set; }
        public string ImagePath { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public double MBytes { get; set; }
    }
    public class UploadImageConfig
    {
        public ImageConfig ImageFileConfig { get; set; }
        public ImageConfig ThumbConfig { get; set; }
    }

}
