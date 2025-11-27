using Microsoft.EntityFrameworkCore;
using AutoPartsStore.Models;

namespace AutoPartsStore.Data
{
    public class AutoPartsStoreContext : DbContext
    {
        public AutoPartsStoreContext(DbContextOptions<AutoPartsStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistory { get; set; }

        public DbSet<Tire> Tires { get; set; }
        public DbSet<BrakePad> BrakePads { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<Wiper> Wipers { get; set; }
        public DbSet<SparkPlug> SparkPlugs { get; set; }
        public DbSet<MotorOil> MotorOils { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Составной ключ PurchaseHistory
            modelBuilder.Entity<PurchaseHistory>()
                .HasKey(p => new { p.UserID, p.ProductID });

            // 1:1 связи
            modelBuilder.Entity<Tire>()
                .HasOne(t => t.Product)
                .WithOne(p => p.Tire)
                .HasForeignKey<Tire>(t => t.ProductID);

            modelBuilder.Entity<BrakePad>()
                .HasOne(t => t.Product)
                .WithOne(p => p.BrakePad)
                .HasForeignKey<BrakePad>(t => t.ProductID);

            modelBuilder.Entity<Battery>()
                .HasOne(t => t.Product)
                .WithOne(p => p.Battery)
                .HasForeignKey<Battery>(t => t.ProductID);

            modelBuilder.Entity<Wiper>()
                .HasOne(t => t.Product)
                .WithOne(p => p.Wiper)
                .HasForeignKey<Wiper>(t => t.ProductID);

            modelBuilder.Entity<SparkPlug>()
                .HasOne(t => t.Product)
                .WithOne(p => p.SparkPlug)
                .HasForeignKey<SparkPlug>(t => t.ProductID);

            modelBuilder.Entity<MotorOil>()
                .HasOne(t => t.Product)
                .WithOne(p => p.MotorOil)
                .HasForeignKey<MotorOil>(t => t.ProductID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
