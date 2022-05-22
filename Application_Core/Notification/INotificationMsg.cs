using Application_Common;
using System.Threading.Tasks;

namespace Application_Core.Notification
{
    public interface INotificationMsg
    {
        void SaveMailNotification(string From, string To, string Subject, string Body);

        Task<bool> SendAsync(NotficationCls notfication);
    }
}