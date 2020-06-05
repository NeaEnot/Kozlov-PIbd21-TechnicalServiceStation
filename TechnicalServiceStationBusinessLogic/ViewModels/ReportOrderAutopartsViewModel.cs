using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ReportOrderAutopartsViewModel
    {
        [DisplayName("Id заказа")]
        [DataMember]
        public int Id { get; set; }

        [DisplayName("Запчасти")]
        [DataMember]
        public string AutopartsName { get; set; }

        [DisplayName("Количество")]
        [DataMember]
        public int Count { get; set; }

        [DisplayName("Цена")]
        [DataMember]
        public int Price { get; set; }
    }
}
