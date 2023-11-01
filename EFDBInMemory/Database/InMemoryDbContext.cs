using EFDBInMemory.Models;
using Microsoft.EntityFrameworkCore;

namespace EFDBInMemory.Database
{
    public class InMemoryDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ProductDb");
        }
        public DbSet<ProductCategory> Categories { get; set; }
    }
}
