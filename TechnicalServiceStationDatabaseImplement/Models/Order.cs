using System;
using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationDatabaseImplement.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<OrderService> OrderServices { get; set; }
    }
}
