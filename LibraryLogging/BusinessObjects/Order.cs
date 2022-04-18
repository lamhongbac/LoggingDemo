using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogging.BusinessObjects
{
    public class Order
    {
        public int OrderNo { get; set; }
        public int NumberOfItems { get; set; }
        public int TotalAmount { get; set; }
        public Customer Customer { get; set; }
    }
}
