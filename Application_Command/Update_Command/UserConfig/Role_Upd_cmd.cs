using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Domain;
using Application_Domain.UserConfig;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Insert_Command.UserConfig
{
    public class Role_Upd_cmd : IRequest<Response>
    {
        public int RoleId { get; set; }
        public string RoleNmae { get; set; }
        public bool IsActive { get; set; }

        public class Role_Upd_cmd_Handeler : IRequestHandler<Role_Upd_cmd, Response>
        {
            private readonly IRepositoryAsync<role_cls> _roles;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public Role_Upd_cmd_Handeler(IMapper mapper, IRepositoryAsync<role_cls> roles, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _roles = roles;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Role_Upd_cmd request, CancellationToken cancellationToken)
            {
                role_cls obj = (_mapper.Map<role_cls>(request));
                role_cls entity = await _roles.GetDetails(obj.RoleId);

                if (entity != null)
                {
                    obj.ModifiedOn = System.DateTime.Now;
                    Response response = await _roles.UpdateAsync(obj, true, "roles");
                    if (response != null && response.ResponseStatus.ToLower() == "success")
                    {
                        Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Role Updated Succesfully " + request.RoleNmae, "Role Updated Succesfully " + request.RoleNmae));
                    }

                    return response;
                }
                return null;
            }
        }
    }
}