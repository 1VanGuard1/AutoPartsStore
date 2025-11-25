using AutoPartsStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFDataApp.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistories{get; set;}
        public DbSet<MotorOils> MotorOils { get; set; }
        public DbSet<SparkPlugs> SparkPlugs { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Batteries> Batteries { get; set; }
        public DbSet<BrakePads> BrakePads { get; set; }
        public DbSet<Wipers> Wipers { get; set; }
        public DbSet<Tires> Tires { get; set; }
        public DbSet<Products> Products { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}