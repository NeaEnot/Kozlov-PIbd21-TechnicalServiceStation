using System;
using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.BindingModels
{
    public class OrderBindingModel
    {
        public int? Id { get; set; }

        public int Price { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? UserId { get; set; }

        public Dictionary<int, string> OrderServices { get; set; }
    }
}
