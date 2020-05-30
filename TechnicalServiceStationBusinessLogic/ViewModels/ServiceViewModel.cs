using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ServiceViewModel : ViewModel
    {
        [Column(title: "Услуга", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string Name { get; set; }

        [Column(title: "Стоимость", width: 150)]
        [DataMember]
        public int Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int, int)> ServiceAutoparts { get; set; }

        public override List<string> Properties()
            => new List<string>
            {
                "Id",
                "Name",
                "Price"
            };
    }
}
