using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Insert_Command.UserConfig
{
    public class User_Del_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class User_Del_cmd_Handeler : IRequestHandler<User_Del_cmd, Response>
        {
            private readonly IRepositoryAsync<user_cls> _user;
            private readonly INotificationMsg _notificationMsg;

            public User_Del_cmd_Handeler(IRepositoryAsync<user_cls> user, INotificationMsg notificationMsg)
            {
                _user = user;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(User_Del_cmd request, CancellationToken cancellationToken)
            {
                user_cls entity = await _user.GetDetails(request.Id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedOn = System.DateTime.Now;

                    Response response = await _user.UpdateAsync(entity, true, "users");
                    if (response != null && response.ResponseStatus.ToLower() == "success")
                    {
                        Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "User Delete Succesfully " + entity.EmailId, "User Delete Succesfully " + entity.EmailId));
                    }

                    return response;
                }
                return null;
            }
        }
    }
}