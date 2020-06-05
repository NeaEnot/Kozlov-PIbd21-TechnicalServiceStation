using System.ComponentModel;
using System.Runtime.Serialization;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class AutopartsViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DisplayName("Запчасти")]
        [DataMember]
        public string Name { get; set; }

        [DisplayName("Цена за единицу")]
        [DataMember]
        public int Price { get; set; }
    }
}