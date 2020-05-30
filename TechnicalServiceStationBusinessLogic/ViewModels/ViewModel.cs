using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public abstract class ViewModel
    {
        [Column(visible: false)]
        [DataMember]
        public int Id { get; set; }

        public abstract List<string> Properties();
    }
}
