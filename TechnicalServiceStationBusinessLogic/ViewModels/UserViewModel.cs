using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnicalServiceStationBusinessLogic.Attributes;
using TechnicalServiceStationBusinessLogic.Enums;

namespace TechnicalServiceStationBusinessLogic.ViewModels
{
    public class UserViewModel : ViewModel
    {
        [Column(title: "Email", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        public override List<string> Properties()
            => new List<string>
            {
                "Id",
                "Email"
            };
    }
}
