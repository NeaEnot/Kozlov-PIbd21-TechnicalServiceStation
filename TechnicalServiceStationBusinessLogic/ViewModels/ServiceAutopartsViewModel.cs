using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ServiceAutopartsViewModel : ViewModel
    {
        [Column(title: "Запчасти", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string AutopartTypeName { get; set; }

        [Column(title: "Количество", width: 100)]
        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public int ServiceId { get; set; }

        [DataMember]
        public int AutopartTypeId { get; set; }

        public override List<string> Properties()
            => new List<string>
            {
                "Id",
                "AutopartTypeName",
                "Count"
            };
    }
}
