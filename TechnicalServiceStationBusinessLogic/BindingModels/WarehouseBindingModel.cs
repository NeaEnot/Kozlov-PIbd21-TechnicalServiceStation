using System.Collections.Generic;

namespace TechnicalServiceStationBusinessLogic.BindingModels
{
    public class WarehouseBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public Dictionary<int, (string, int, int)> WarehouseAutoparts { get; set; }
    }
}
