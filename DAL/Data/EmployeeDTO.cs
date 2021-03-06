using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    #region data object
    /// <summary>
    /// data object
    /// </summary>
    public class EmployeeDTO
    {
        public string FullName { get; set; }
        public int Salary { get; set; }
        public AddressDTO AddressDTO { get; set; }
        public string Dept { get; set; }
    }
    /// <summary>
    /// data object
    /// Map from object type (Address)to primitive type(string)
    /// </summary>
    public class EmployeeDTO2
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Department { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }

    public class AddressDTO
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
#endregion
}
