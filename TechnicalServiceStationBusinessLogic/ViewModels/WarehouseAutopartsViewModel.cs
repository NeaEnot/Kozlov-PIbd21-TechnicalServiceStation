using System.ComponentModel;
using System.Runtime.Serialization;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class WarehouseAutopartsViewModel
    {
        [DisplayName("Запчасти")]
        [DataMember]
        public string AutopartTypeName { get; set; }

        [DisplayName("Количество")]
        [DataMember]
        public int Count { get; set; }

        [DisplayName("Зарезервировано")]
        [DataMember]
        public int Reserved { get; set; }

        [DataMember]
        public int WarehouseId { get; set; }

        [DataMember]
        public int AutopartTypeId { get; set; }
    }
}
