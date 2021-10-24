using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Insert_Command.UserConfig
{
    public class Role_Del_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class Role_Del_cmd_Handeler : IRequestHandler<Role_Del_cmd, Response>
        {
            private readonly IRepositoryAsync<role_cls> _roles;
            private readonly INotificationMsg _notificationMsg;

            public Role_Del_cmd_Handeler(IRepositoryAsync<role_cls> roles, INotificationMsg notificationMsg)
            {
                _roles = roles;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Role_Del_cmd request, CancellationToken cancellationToken)
            {
                role_cls entity = await _roles.GetDetails(request.Id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedOn = System.DateTime.Now;

                    Response response = await _roles.UpdateAsync(entity, true, "roles");
                    if (response != null && response.ResponseStatus.ToLower() == "success")
                    {
                        _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Role Delete Succesfully " + entity.RoleNmae, "Role Delete Succesfully " + entity.RoleNmae);
                    }

                    return response;
                }
                return null;
            }
        }
    }
}