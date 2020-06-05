using System.ComponentModel;
using System.Runtime.Serialization;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ServiceAutopartsViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DisplayName("Запчасти")]
        [DataMember]
        public string AutopartsName { get; set; }

        [DisplayName("Количество")]
        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public int ServiceId { get; set; }

        [DataMember]
        public int AutopartsId { get; set; }
    }
}