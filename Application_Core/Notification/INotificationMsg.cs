using Application_Domain;

namespace Application_Core.Notification
{
    public interface INotificationMsg
    {
        void SaveMailNotification(string From, string To, string Subject, string Body);

        bool Send(NotficationCls notfication);
    }
}