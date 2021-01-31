using FinancingPMS.Models;

namespace FinancingPMS.Interfaces
{
    public interface INotificationService
    {
        public string SendNotification(NotificationDetails notificationDetails);
    }
}
