using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DisplayName("Пользователь")]
        [DataMember]
        public string UserEmail { get; set; }

        [DisplayName("Цена")]
        [DataMember]
        public int Price { get; set; }

        [DisplayName("Статус")]
        [DataMember]
        public OrderStatus Status { get; set; }

        [DisplayName("Дата создания")]
        [DataMember]
        public DateTime CreateDate { get; set; }

        [DisplayName("Дата выполнения")]
        [DataMember]
        public DateTime? DeliveryDate { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public List<OrderServiceViewModel> OrderServices { get; set; }
    }
}