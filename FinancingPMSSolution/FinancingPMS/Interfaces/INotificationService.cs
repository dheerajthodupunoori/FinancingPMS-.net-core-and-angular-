using FinancingPMS.Models;

namespace FinancingPMS.Interfaces
{
    public interface INotificationService
    {
        public void SendNotification(NotificationDetails notificationDetails);
    }
}
