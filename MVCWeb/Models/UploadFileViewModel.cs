using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCWeb.Models
{
    public class UploadFileViewModel
    {
        [Required(ErrorMessage = "Chọn một file")]
        [DataType(DataType.Upload)]
        [FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Chọn file upload")]
        [BindProperty]
        public IFormFile[] FileUploads { get; set; }
    }
}
