using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogging.BusinessObjects
{
    #region logic objects
    /// <summary>
    /// logic object
    /// </summary>
    public class Employee
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public Address Address { get; set; }
        public string Department { get; set; }
    }
    /// <summary>
    /// Logic object
    /// </summary>
    public class Address
    {
        public string City { get; set; }
        public string Stae { get; set; }
        public string Country { get; set; }
    }
    #endregion
}
