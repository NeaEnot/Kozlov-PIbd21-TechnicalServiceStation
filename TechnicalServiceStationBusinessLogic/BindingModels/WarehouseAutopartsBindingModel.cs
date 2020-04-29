namespace TechnicalServiceStationBusinessLogic.BindingModels
{
    public class WarehouseAutopartsBindingModel
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public int Reserved { get; set; }

        public int ServiceId { get; set; }

        public int AutopartTypeId { get; set; }
    }
}
