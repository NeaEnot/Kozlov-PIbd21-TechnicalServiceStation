using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class WarehouseViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DisplayName("Название Склада")]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Dictionary<int, (string, int, int)> WarehouseAutoparts { get; set; }
    }
}
