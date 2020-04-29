using System.ComponentModel;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ServiceAutopartsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Запчасти")]
        public string AutopartTypeName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        public int ServiceId { get; set; }

        public int AutopartTypeId { get; set; }
    }
}
