using Microsoft.EntityFrameworkCore;
using TshirtInventoryBackend.Models;

namespace TshirtInventoryBackend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Tshirt> Tshirts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<TshirtOrder> TshirtOrders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<BlacklistedToken> BlacklistedTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(user => user.Email);

            modelBuilder.Entity<Customer>()
                .HasKey(customer => customer.Email);

            modelBuilder.Entity<TshirtOrder>()
                .HasKey(to => new { to.OrderId, to.TshirtId });
        }
    }
}
