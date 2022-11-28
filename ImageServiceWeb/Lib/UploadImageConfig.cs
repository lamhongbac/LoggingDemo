using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServiceWeb.Lib
{
    /// <summary>
    /// All config for client pass by http Client
    /// </summary>
    //public class UploadImageConfigA
    //{
    //    public UploadImageConfigA()
    //    {

    //    }
    //    //Image Info
    //    public string ImageFileName { get; set; }
    //    public string ImagePath { get; set; }
    //    public int ImageMaxWidth { get; set; }
    //    public int ImageMaxHeight { get; set; }
    //    public double ImageMBytes { get; set; }
    //    //Thumb Info
    //    public string ThumbFileName { get; set; }
    //    public string ThumbPath { get; set; }
    //    public int ThumbMaxWidth { get; set; }
    //    public int ThumbMaxHeight { get; set; }
    //    public double ThumbMBytes { get; set; }
    //}
    public class ImageConfig
    {
        public string FileName { get; set; }
        public string ImagePath { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public double MBytes { get; set; }
    }
}
