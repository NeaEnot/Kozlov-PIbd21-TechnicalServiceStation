using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ReportServiceViewModel : ViewModel
    {
        [Column(title: "Дата создания заказа", width: 150)]
        [DataMember]
        public DateTime OrderCreateDate { get; set; }

        [Column(title: "Услуга", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string ServiceName { get; set; }

        [Column(title: "Цена", width: 150)]
        [DataMember]
        public int Price { get; set; }

        public override List<string> Properties()
            => new List<string>
            {
                "Id",
                "OrderCreateDate",
                "ServiceName",
                "Price"
            };
    }
}
