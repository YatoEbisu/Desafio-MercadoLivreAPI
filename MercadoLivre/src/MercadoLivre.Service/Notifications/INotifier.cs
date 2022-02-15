using System.Collections.Generic;

namespace MercadoLivre.Service.Notifications
{
    public interface INotifier
    {
        bool HaveNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}