using System.ComponentModel;
using System.Runtime.Serialization;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class OrderServiceViewModel
    {
        [DisplayName("Работа")]
        [DataMember]
        public string ServiceName { get; set; }

        [DisplayName("Цена")]
        [DataMember]
        public int Price { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int ServiceId { get; set; }
    }
}