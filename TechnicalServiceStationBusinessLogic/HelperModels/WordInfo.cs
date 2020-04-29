﻿using System.Collections.Generic;
using TechnicalServiceStationBusinessLogic.ViewModels;

namespace TechnicalServiceStationBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public Dictionary<int, List<ReportServiceViewModel>> Orders { get; set; }
    }
}