using System.ComponentModel;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class AutopartsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Запчасти")]
        public string Name { get; set; }

        [DisplayName("Цена за единицу")]
        public int Price { get; set; }
    }
}
