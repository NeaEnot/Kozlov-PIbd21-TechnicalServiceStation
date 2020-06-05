using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ReportServiceViewModel
    {
        [DisplayName("Id заказа")]
        [DataMember]
        public int OrderId { get; set; }

        [DisplayName("Услуги")]
        [DataMember]
        public List<(string, int)> Services { get; set; }
    }
}
