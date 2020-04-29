using System;

namespace TechnicalServiceStationBusinessLogic.BindingModels
{
    public class MessageInfoBindingModel
    {
        public string FromMailAddress { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime DateDelivery { get; set; }

        public int UserId { get; set; }
    }
}
