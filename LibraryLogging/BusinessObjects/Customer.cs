using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogging.BusinessObjects
{
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(int custID, string fullName, string postCode, string contactNo)
        {
            CustomerID = custID; FullName = fullName; Postcode = postCode; ContactNo = contactNo;
        }
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Postcode { get; set; }
        public string ContactNo { get; set; }
    }
}
