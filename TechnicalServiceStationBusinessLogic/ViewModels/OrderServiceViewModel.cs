using System.ComponentModel;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class OrderServiceViewModel
    {
        [DisplayName("Работа")]
        public string AutopartTypeName { get; set; }

        public int OrderId { get; set; }

        public int ServiceId { get; set; }
    }
}
