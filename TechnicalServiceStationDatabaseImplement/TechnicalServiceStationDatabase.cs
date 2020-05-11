using Microsoft.EntityFrameworkCore;
using TechnicalServiceStationDatabaseImplement.Models;

namespace TechnicalServiceStationDatabaseImplement
{
    public class TechnicalServiceStationDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder
                    .UseSqlServer(
                        @"Data Source=DESKTOP-S65O0I4\SQLEXPRESS;
                          Initial Catalog=TechnicalServiceStationDatabase;
                          Integrated Security=True;
                          MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Autoparts> Autoparts { set; get; }

        public virtual DbSet<Order> Orders { set; get; }

        public virtual DbSet<OrderService> OrderServices { set; get; }

        public virtual DbSet<Service> Services { set; get; }

        public virtual DbSet<ServiceAutoparts> ServiceAutoparts { set; get; }

        public virtual DbSet<User> Users { set; get; }

        public virtual DbSet<Warehouse> Warehouses { set; get; }

        public virtual DbSet<WarehouseAutoparts> WarehouseAutoparts { set; get; }
    }
}
