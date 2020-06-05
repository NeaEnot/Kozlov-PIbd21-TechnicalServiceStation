using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public void FillDatabase()
        {
            Autoparts autoparts1 = new Autoparts { Name = "Тормозные колодки", Price = 200 };
            Autoparts autoparts2 = new Autoparts { Name = "Свеча зажигания", Price = 80 };
            Autoparts autoparts3 = new Autoparts { Name = "Масло", Price = 240 };
            Autoparts autoparts4 = new Autoparts { Name = "Глушитель", Price = 240 };
            Autoparts autoparts5 = new Autoparts { Name = "Подшипник", Price = 600 };
            Autoparts autoparts6 = new Autoparts { Name = "Лобовое стекло", Price = 1600 };

            Autoparts.Add(autoparts1);
            Autoparts.Add(autoparts2);
            Autoparts.Add(autoparts3);
            Autoparts.Add(autoparts4);
            Autoparts.Add(autoparts5);
            Autoparts.Add(autoparts6);

            Service service1 = new Service { Name = "Диагностика автомобиля", Price = 200 };
            Service service2 = new Service { Name = "Замена тормозных колодок", Price = 500 };
            Service service3 = new Service { Name = "Замена свечей зажигания", Price = 400 };
            Service service4 = new Service { Name = "Замена масла в двигателе", Price = 300 };
            Service service5 = new Service { Name = "Замена глушителя", Price = 300 };
            Service service6 = new Service { Name = "Замена подшипника ступицы", Price = 750 };
            Service service7 = new Service { Name = "Замена лобового стекла", Price = 200 };

            Services.Add(service1);
            Services.Add(service2);
            Services.Add(service3);
            Services.Add(service4);
            Services.Add(service5);
            Services.Add(service6);
            Services.Add(service7);

            SaveChanges();

            ServiceAutoparts sa1 = new ServiceAutoparts
            {
                ServiceId = Services.FirstOrDefault(rec => rec.Name == "Замена тормозных колодок").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Тормозные колодки").Id,
                Count = 2
            };
            ServiceAutoparts sa2 = new ServiceAutoparts
            {
                ServiceId = Services.FirstOrDefault(rec => rec.Name == "Замена свечей зажигания").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Свеча зажигания").Id,
                Count = 4
            };
            ServiceAutoparts sa3 = new ServiceAutoparts
            {
                ServiceId = Services.FirstOrDefault(rec => rec.Name == "Замена масла в двигателе").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Масло").Id,
                Count = 1
            };
            ServiceAutoparts sa4 = new ServiceAutoparts
            {
                ServiceId = Services.FirstOrDefault(rec => rec.Name == "Замена глушителя").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Глушитель").Id,
                Count = 1
            };
            ServiceAutoparts sa5 = new ServiceAutoparts
            {
                ServiceId = Services.FirstOrDefault(rec => rec.Name == "Замена подшипника ступицы").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Подшипник").Id,
                Count = 1
            };
            ServiceAutoparts sa6 = new ServiceAutoparts
            {
                ServiceId = Services.FirstOrDefault(rec => rec.Name == "Замена лобового стекла").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Лобовое стекло").Id,
                Count = 1
            };

            ServiceAutoparts.Add(sa1);
            ServiceAutoparts.Add(sa2);
            ServiceAutoparts.Add(sa3);
            ServiceAutoparts.Add(sa4);
            ServiceAutoparts.Add(sa5);
            ServiceAutoparts.Add(sa6);

            SaveChanges();

            Warehouse warehouse = new Warehouse { Name = "Склад в Новом городе" };

            Warehouses.Add(warehouse);

            SaveChanges();

            WarehouseAutoparts wa1 = new WarehouseAutoparts
            {
                WarehouseId = Warehouses.FirstOrDefault(rec => rec.Name == "Склад в Новом городе").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Тормозные колодки").Id,
                Count = 100,
                Reserved = 0
            };
            WarehouseAutoparts wa2 = new WarehouseAutoparts
            {
                WarehouseId = Warehouses.FirstOrDefault(rec => rec.Name == "Склад в Новом городе").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Свеча зажигания").Id,
                Count = 100,
                Reserved = 0
            };
            WarehouseAutoparts wa3 = new WarehouseAutoparts
            {
                WarehouseId = Warehouses.FirstOrDefault(rec => rec.Name == "Склад в Новом городе").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Масло").Id,
                Count = 100,
                Reserved = 0
            };
            WarehouseAutoparts wa4 = new WarehouseAutoparts
            {
                WarehouseId = Warehouses.FirstOrDefault(rec => rec.Name == "Склад в Новом городе").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Глушитель").Id,
                Count = 100,
                Reserved = 0
            };
            WarehouseAutoparts wa5 = new WarehouseAutoparts
            {
                WarehouseId = Warehouses.FirstOrDefault(rec => rec.Name == "Склад в Новом городе").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Подшипник").Id,
                Count = 100,
                Reserved = 0
            };
            WarehouseAutoparts wa6 = new WarehouseAutoparts
            {
                WarehouseId = Warehouses.FirstOrDefault(rec => rec.Name == "Склад в Новом городе").Id,
                AutopartsId = Autoparts.FirstOrDefault(rec => rec.Name == "Лобовое стекло").Id,
                Count = 100,
                Reserved = 0
            };

            WarehouseAutoparts.Add(wa1);
            WarehouseAutoparts.Add(wa2);
            WarehouseAutoparts.Add(wa3);
            WarehouseAutoparts.Add(wa4);
            WarehouseAutoparts.Add(wa5);
            WarehouseAutoparts.Add(wa6);

            SaveChanges();
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
