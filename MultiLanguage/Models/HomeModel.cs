using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MultiLanguage.Models
{
    public class HomeModel
    {
        [Display(Name = "AppTitle")]
        public string AppTitle { get; set; }
    }
}
