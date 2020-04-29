using System.ComponentModel;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class WarehouseAutopartsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Запчасти")]
        public string AutopartTypeName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Зарезервировано")]
        public int Reserved { get; set; }

        public int WarehouseId { get; set; }

        public int AutopartTypeId { get; set; }
    }
}
