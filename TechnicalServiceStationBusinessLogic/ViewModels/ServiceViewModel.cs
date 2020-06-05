using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ServiceViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DisplayName("Название работы")]
        [DataMember]
        public string Name { get; set; }

        [DisplayName("Цена")]
        [DataMember]
        public int Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int, int)> ServiceAutoparts { get; set; }
    }
}