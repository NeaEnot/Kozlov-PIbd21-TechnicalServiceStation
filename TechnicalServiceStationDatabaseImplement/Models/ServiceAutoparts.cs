namespace TechnicalServiceStationDatabaseImplement.Models
{
    public class ServiceAutoparts
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }

        public int AutopartsId { get; set; }

        public Autoparts Autoparts { get; set; }
}
}
