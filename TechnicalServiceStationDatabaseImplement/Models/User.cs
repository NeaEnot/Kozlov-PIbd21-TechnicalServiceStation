using System.Collections.Generic;

namespace TechnicalServiceStationDatabaseImplement.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Order> Orders { get; set; }
    }
}
