using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public Dictionary<int, List<ReportServiceViewModel>> Orders { get; set; }
    }
}
