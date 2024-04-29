using Microsoft.EntityFrameworkCore;
using WebInvManagement.Models;

namespace WebInvManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // many to many
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<WarehouseProductionStock>()
                .HasKey(wp => new { wp.WarehouseId, wp.ProductionStockId });

            modelBuilder.Entity<WarehouseProductionStock>()
                .HasOne(wp => wp.Warehouse)
                .WithMany(w => w.WarehouseProductionStocks)
                .HasForeignKey(wp => wp.WarehouseId);

            modelBuilder.Entity<WarehouseProductionStock>()
                .HasOne(wp => wp.ProductionStock)
                .WithMany(p => p.WarehouseProductionStocks)
                .HasForeignKey(wp => wp.ProductionStockId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> Roles { get; set; }
        public DbSet<StockType> StockTypes { get; set; }
        public DbSet<ProductionStock> ProductionStocks { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseProductionStock> WarehouseProductionStocks {  get; set; }
        public DbSet<ABCGroup> ABCGroups { get; set; }
        public DbSet<XYZGroup> XYZGroups { get; set; }
        public DbSet<IMStrategy> IMStrategys { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Report> Reports { get; set; }

    }
}
