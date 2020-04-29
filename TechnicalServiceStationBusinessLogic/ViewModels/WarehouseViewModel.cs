using System.Collections.Generic;
using System.ComponentModel;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название склада")]
        public string WarehouseName { get; set; }

        public Dictionary<int, (string, int)> AutopartsInWarehouse { get; set; }
    }
}
