using BicycleShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BicycleShop.Data
{
    public class BicycleContext : DbContext
    {
        public BicycleContext(DbContextOptions<BicycleContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Bicycle> Bicycles { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
