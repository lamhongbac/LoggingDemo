using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerServiceWeb.DAL.DTO
{
    [Table("mssWebNotification")]
    public class WebNotificationUI
    {
        [Key]
        public int ID { get; set; }
        public string FromUserID { get; set; }
        public string ToUserID { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string URL { get; set; }
        
        public bool IsRead { get; set; }
        public DateTime SendDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }






}
