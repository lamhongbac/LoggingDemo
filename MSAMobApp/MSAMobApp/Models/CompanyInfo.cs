using System;
using System.Collections.Generic;
using System.Text;

namespace MSAMobApp.Models
{
    public class CompanyInfo 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ClientSecret { get; set; }
       
        public string CompanyType { get; set; }
        public string BusinessType { get; set; }
        public string Address { get; set; }
        public int? DistrictID { get; set; }
        public int? ProvinceID { get; set; }
        public int? NationID { get; set; }
        public string PwdHK { get; set; }
        public string Pwd { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string CEOName { get; set; }
        public string Location { get; set; }
        public string Slogan { get; set; }
        public string Logo { get; set; }
        public string RegCode { get; set; }
       
    }
}
