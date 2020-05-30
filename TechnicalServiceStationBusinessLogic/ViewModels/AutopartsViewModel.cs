using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class AutopartsViewModel : ViewModel
    {
        [Column(title: "Запчасти", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string Name { get; set; }

        [Column(title: "Цена за единицу", width: 150)]
        [DataMember]
        public int Price { get; set; }

        public override List<string> Properties()
            => new List<string>
            {
                "Id",
                "Name",
                "Price"
            };
    }
}
