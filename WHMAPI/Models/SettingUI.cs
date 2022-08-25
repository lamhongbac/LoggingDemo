using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WHMAPI.Models
{
    public class SettingUI
    {
        public string Group { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
    }
}
