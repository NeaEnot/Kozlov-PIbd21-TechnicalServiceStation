using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ReportOrderAutopartsViewModel : ViewModel
    {
        [Column(title: "Дата создания заказа", width: 150)]
        [DataMember]
        public DateTime OrderCreateDate { get; set; }

        [Column(title: "Запчасти", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string AutopartsName { get; set; }

        [Column(title: "Количество", width: 150)]
        [DataMember]
        public int Count { get; set; }

        [Column(title: "Цена", width: 150)]
        [DataMember]
        public int Price { get; set; }

        public override List<string> Properties()
            => new List<string>
            {
                "Id",
                "OrderCreateDate",
                "AutopartsName",
                "Count",
                "Price"
            };
    }
}
