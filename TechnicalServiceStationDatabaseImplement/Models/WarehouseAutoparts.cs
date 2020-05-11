namespace TechnicalServiceStationDatabaseImplement.Models
{
    public class WarehouseAutoparts
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public int Reserved { get; set; }

        public int AutopartsId { get; set; }

        public Autoparts Autoparts { get; set; }

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; }
    }
}
