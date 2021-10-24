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
    public class Role_Inst_cmd : IRequest<Response>
    {
        public string RoleNmae { get; set; }
        public bool IsActive { get; set; }

        public class Role_Inst_cmd_Handeler : IRequestHandler<Role_Inst_cmd, Response>
        {
            private readonly IRepositoryAsync<role_cls> _roles;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public Role_Inst_cmd_Handeler(IMapper mapper, IRepositoryAsync<role_cls> roles, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _roles = roles;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Role_Inst_cmd request, CancellationToken cancellationToken)
            {
                role_cls obj = (_mapper.Map<role_cls>(request));
                obj.CreatedOn = System.DateTime.Now;
                Response response = await _roles.SaveAsync(obj, true, "roles");

                if (response != null && response.ResponseStatus.ToLower() == "success")
                {
                    _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Role Created Succesfully " + request.RoleNmae, "Role Created Succesfully " + request.RoleNmae);
                }

                return response;
            }
        }
    }
}