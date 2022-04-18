using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStudy.ViewModel
{
    /// <summary>
    /// lop the hien giao dien, lop nay nam ben du an giao dien
    /// lop the hien giao dien, lop nay nam ben du an giao dien
    /// </summary>
  public  class OutletViewModel
    {
        public int BrandId { get; set; }
        public int GroupId { get; set; }
        public string Address { get; set; }
        public string ImageURL { get; set; }

        public string Glocation { get; set; }
    }
}
