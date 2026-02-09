using Entities.Concrate;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrate.EntityFramework
{
    // Context : Db tabloları ile proje classlarını bağlamak.
    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
