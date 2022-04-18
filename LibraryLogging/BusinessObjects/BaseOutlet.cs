using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogging.BusinessObjects
{
    /// <summary>
    /// lop logic demo cho doi tuong outlet
    /// </summary>
   public class BaseOutlet
    {
        public Guid ID { get; set; }
        public int CompanyId { get; set; }
        public int? RegionId { get; set; }
        public int BrandId { get; set; }
        public int? BranchId { get; set; }
        public int? GroupId { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }

        public string Glocation { get; set; }
    }
}
