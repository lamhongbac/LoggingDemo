using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsCMS.Data
{
    [Table("tblNews")]
    public class NewsData
    {
       
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public string MobileNo { get; set; }

            public string Email { get; set; }


        
    }
}
