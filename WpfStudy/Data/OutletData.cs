using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStudy.Data
{
    /// <summary>
    /// lop DTO cho dai dien table outlet
    /// </summary>
    [Table("Outlet", Schema = "dbo")]
    public  class OutletData
    {
        public Guid ID { get; set; }
        public int? CompanyId { get; set; }
        public int? RegionId { get; set; }
        public int BrandId { get; set; }
        public int? BranchId { get; set; }
        public int? GroupId { get; set; }
        public string Address { get; set; }
        public string Image { get; set; } //ImageFile nay se dc chuyen thanh ImageURL can cu vao cau hinh Image Server

        public string Glocation { get; set; }

        //Meta data for tracking: ngoai data nay se dc log vao log file
        public string UserID { get; set; } //nguoi thuc hien khoi tao, hay modified,deleted this data
        public DateTime ChangedOn { get; set; } //thoi diem thuc hien

    }
}
