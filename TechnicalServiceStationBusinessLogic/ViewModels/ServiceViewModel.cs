using System.Collections.Generic;
using System.ComponentModel;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class ServiceViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название работы")]
        public string Name { get; set; }

        [DisplayName("Цена")]
        public int Price { get; set; }

        public Dictionary<int, (string, int, int)> ServiceAutoparts { get; set; }
    }
}
