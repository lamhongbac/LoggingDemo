using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace EFDBInMemory.Models
{
    public class ProductCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public decimal   Price { get; set; }
    }
}
