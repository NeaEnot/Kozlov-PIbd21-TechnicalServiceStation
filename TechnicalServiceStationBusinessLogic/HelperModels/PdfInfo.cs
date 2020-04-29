using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.HelperModels
{
    class PdfInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<ReportOrderAutopartsViewModel> OrderAutoparts { get; set; }
    }
}
