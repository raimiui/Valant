using Valant.Model;

namespace Valant.Services.Interfaces
{
    public interface INotificationService
    {
        void Send(Notification notification);
    }
}
