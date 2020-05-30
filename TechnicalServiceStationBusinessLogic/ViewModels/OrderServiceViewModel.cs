using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class OrderServiceViewModel : ViewModel
    {
        [Column(title: "Услуга", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string ServiceName { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int ServiceId { get; set; }

        public override List<string> Properties()
            => new List<string>
            {
                "Id",
                "ServiceName"
            };
    }
}
