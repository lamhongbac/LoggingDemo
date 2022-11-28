using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerServiceWeb.DAL.DTO
{
    [Table("G_AppUser")]
    public class AppUserUI
    {
        [Key]
        public int ID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public int AppID { get; set; }
        public int AppRoleID { get; set; }
        public string Pwd { get; set; }
    }
}
