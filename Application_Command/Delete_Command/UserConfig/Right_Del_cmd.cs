using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Insert_Command.UserConfig
{
    public class Right_Del_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class Right_Del_cmd_Handeler : IRequestHandler<Right_Del_cmd, Response>
        {
            private readonly IRepositoryAsync<rights_cls> _rights;
            private readonly INotificationMsg _notificationMsg;

            public Right_Del_cmd_Handeler(IRepositoryAsync<rights_cls> rights, INotificationMsg notificationMsg)
            {
                _rights = rights;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Right_Del_cmd request, CancellationToken cancellationToken)
            {
                rights_cls entity = await _rights.GetDetails(request.Id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedOn = System.DateTime.Now;

                    Response response = await _rights.UpdateAsync(entity, true, "rights");
                    if (response != null && response.ResponseStatus.ToLower() == "success")
                    {
                        Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Right Delete Succesfully " + entity.RightId, "Right Delete Succesfully " + entity.RightId));
                    }

                    return response;
                }
                return null;
            }
        }
    }
}