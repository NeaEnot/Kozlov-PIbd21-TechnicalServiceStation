using System.Collections.Generic;

namespace TechnicalServiceStationBusinessLogic.BindingModels
{
    public class ServiceBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public Dictionary<int, (string, int)> Autoparts { get; set; }
    }
}
