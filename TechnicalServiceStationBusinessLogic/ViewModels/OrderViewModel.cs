using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class OrderViewModel : ViewModel
    {
        [Column(title: "Пользователь", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string UserEmail { get; set; }

        [Column(title: "Цена", width: 50)]
        [DataMember]
        public int Price { get; set; }

        [Column(title: "Статус", width: 50)]
        [DataMember]
        public OrderStatus Status { get; set; }

        [Column(title: "Цена", width: 150)]
        [DataMember]
        public DateTime CreateDate { get; set; }

        [Column(title: "Цена", width: 150)]
        [DataMember]
        public DateTime? DeliveryDate { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> OrderServices { get; set; }

        public override List<string> Properties()
            => new List<string>
            {
                "Id",
                "UserEmail",
                "Price",
                "Status",
                "CreateDate",
                "DeliveryDate"
            };
    }
}
