using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class WarehouseAutopartsViewModel : ViewModel
    {
        [Column(title: "Запчасти", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string AutopartTypeName { get; set; }

        [Column(title: "Количество", width: 100)]
        [DataMember]
        public int Count { get; set; }

        [Column(title: "Зарезервировано", width: 100)]
        [DataMember]
        public int Reserved { get; set; }

        [DataMember]
        public int WarehouseId { get; set; }

        [DataMember]
        public int AutopartTypeId { get; set; }

        public override List<string> Properties() 
            => new List<string>
            {
                "Id",
                "AutopartTypeName",
                "Count",
                "Reserved"
            };
    }
}
