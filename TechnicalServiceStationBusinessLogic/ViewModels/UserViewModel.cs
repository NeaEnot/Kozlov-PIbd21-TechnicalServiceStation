using System.ComponentModel;
using System.Runtime.Serialization;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class UserViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DisplayName("Почта")]
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
