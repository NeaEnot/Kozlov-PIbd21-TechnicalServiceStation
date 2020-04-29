using System;
using System.Collections.Generic;
using System.ComponentModel;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [DisplayName("Пользователь")]
        public string UserEmail { get; set; }

        [DisplayName("Цена")]
        public int Price { get; set; }

        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime CreateDate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DeliveryDate { get; set; }

        public int UserId { get; set; }

        public Dictionary<int, (string, int)> OrderServices { get; set; }
    }
}
