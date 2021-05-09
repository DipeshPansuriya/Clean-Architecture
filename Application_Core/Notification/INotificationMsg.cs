using Application_Domain;

namespace Application_Core.Notification
{
    public interface INotificationMsg
    {
        bool Send(NotficationCls notfication);
    }
}