using System;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ReportOrderAutopartsViewModel
    {
        public DateTime OrderCreateDate { get; set; }

        public string AutopartsName { get; set; }

        public int Count { get; set; }

        public int Price { get; set; }
    }
}
