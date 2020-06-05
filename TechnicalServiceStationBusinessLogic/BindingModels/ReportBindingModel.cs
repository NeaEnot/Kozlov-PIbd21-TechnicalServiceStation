using System;
using System.Collections.Generic;

namespace TechnicalServiceStationBusinessLogic.BindingModels
{
    public class ReportBindingModel
    {
        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public List<int> OrdersId { get; set; }
    }
}
