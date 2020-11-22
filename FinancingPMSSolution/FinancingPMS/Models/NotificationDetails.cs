using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancingPMS.Models
{
    public class NotificationDetails
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public string ReceiverMailID { get; set; }

        public string NotificationType { get; set; }

        //public FirmOwnerDetails FirmOwnerDetails { get; set; }

    }

    public class FirmOwnerDetails
    {
        public string Name { get; set; }

        public string ID { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
