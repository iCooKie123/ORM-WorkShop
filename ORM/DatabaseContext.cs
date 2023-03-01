using Microsoft.EntityFrameworkCore;
using ORM.Objects;

namespace ORM
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Association> Associations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=NexusLite-PC\\SQLEXPRESS;Database=ORM;Integrated Security=sspi; TrustServerCertificate=True;MultipleActiveResultSets=True");
        }
    }
}
