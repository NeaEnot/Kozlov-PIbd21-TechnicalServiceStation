using System.Collections.Generic;

namespace TechnicalServiceStationDatabaseImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<WarehouseAutoparts> WarehouseAutoparts { get; set; }
    }
}
