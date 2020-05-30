using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class WarehouseViewModel : ViewModel
    {
        [Column(title: "Название Склада", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string WarehouseName { get; set; }

        [DataMember]
        public Dictionary<int, (string, int, int)> WarehouseAutoparts { get; set; }

        public override List<string> Properties()
            => new List<string>
            {
                "Id",
                "WarehouseName"
            };
    }
}
