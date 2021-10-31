using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Database;
using Application_Genric;
using Application_Infrastructure.Notificaion;
using AutoMapper;
using MediatR;
using System;
using System.Net;
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
            private readonly IDapper<TblRolemaster> _roles;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public Role_Inst_cmd_Handeler(IMapper mapper, IDapper<TblRolemaster> roles, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _roles = roles;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Role_Inst_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblRolemaster obj = (_mapper.Map<TblRolemaster>(request));
                    obj.CreatedOn = System.DateTime.Now;
                    int result = await _roles.SaveAsync(obj, true, "roles");
                    if (result > 0)
                    {
                        _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Role Created Succesfully " + request.RoleNmae, "Role Created Succesfully " + request.RoleNmae);

                        Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Role Created Succesfully " + request.RoleNmae, "Role Created Succesfully " + request.RoleNmae));
                    }

                    response.ResponseObject = result;
                }
                catch (Exception ex)
                {
                    response.ResponseStatus = false;
                    response.ResponseObject = ex.Message + " ~ " + ex.InnerException;
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
                return response;
            }
        }
    }
}