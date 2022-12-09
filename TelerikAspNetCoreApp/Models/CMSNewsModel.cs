using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelerikAspNetCoreApp.Models
{
    public class CMSNewsModel
    {

        //[AllowHtml]
        public string NewsContent
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }
        public string LogoImageFile { get; set; }
        public string LogoImage
        {
            get;
            set;
        }
        public string DetailImageFile { get; set; }
        public string DetailImage
        {
            get;
            set;
        }
        public string CoverImageFile { get; set; }
        public string CoverImage
        {
            get;
            set;
        }


    }
}
