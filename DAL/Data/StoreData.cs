using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    [Table("G_Outlet")]
    public class StoreData
    {
        [Key]
        public int Id { get; set; }
        public int? Priority { get; set; }
        public int? CardWhid { get; set; }
        public int BrandId { get; set; }
        public int? RegionId { get; set; }
        public string ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string Number { get; set; }
        public string OutletName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Website { get; set; }
        public string Tags { get; set; }
        public decimal? Nps { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public bool? Parking { get; set; }
        public string Picture { get; set; }
        public bool? IsCdc { get; set; }
        public bool? IsUsed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string AccCode { get; set; }
        public string Merchant { get; set; }
        public string MerchantG { get; set; }
        public string Merchant2G { get; set; }
        public string Merchant3G { get; set; }
        public string MerchantId { get; set; }
        public int? LicenseQuantity { get; set; }
        public int? FloorId { get; set; }
        public int? CategoryId { get; set; }
        public string FloorPosition { get; set; }
        public string ImagesList { get; set; }
        public string FloorImage { get; set; }
        public string Image { get; set; }
        public int? BranchId { get; set; }
        public string RedeemCode { get; set; }
        public string PwdCode { get; set; }
        public bool? IsAccPoint { get; set; }
        public bool? IsShowMobileApp { get; set; }
        public bool? IsActive { get; set; }
        public string Images { get; set; }

        public string StoreContent { get; set; }
        public string Videos { get; set; }

        public string Icon { get; set; }
        public string Detail { get; set; }
        public string OutletName_EN { get; set; }
        public string Description_EN { get; set; }
        public string Address_EN { get; set; }
        public string StoreContent_EN { get; set; }
    }
}
