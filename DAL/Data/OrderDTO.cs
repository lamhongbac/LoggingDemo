using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int NumberOfItems { get; set; }
        public int TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Postcode { get; set; }
        public string MobileNo { get; set; }
    }
}
