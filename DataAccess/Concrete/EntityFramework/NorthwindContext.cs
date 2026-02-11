using Entities.Concrate;
using Entities.Concrete;
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
        public DbSet <Order> Orders { get; set; }
        //DbSet belirlenmesinin amacı dbset tablonun bu verilere bağlı olduğunu söyler,
        //eğer list olsaydı  gelen çıktılar sadece bellekteki veriler olurdu ve her seferinde SQL yazmak gerekirdi. 

    }
}
