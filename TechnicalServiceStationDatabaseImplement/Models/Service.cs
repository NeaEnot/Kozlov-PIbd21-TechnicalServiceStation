using System.Collections.Generic;

namespace TechnicalServiceStationDatabaseImplement.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public List<ServiceAutoparts> ServiceAutoparts { get; set; }

        public List<OrderService> OrderServices { get; set; }
    }
}
