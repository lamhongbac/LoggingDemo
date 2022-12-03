using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ckeditor5AspNetCore.Models
{
    public class NewsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }


    }
}
