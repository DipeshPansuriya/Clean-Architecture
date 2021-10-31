using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Database;
using Application_Genric;
using AutoMapper;
using MediatR;
using System;
using System.Net;
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
            private readonly IRepositoryAsync<TblRolemaster> _roles;
            private readonly IMapper _mapper;
            private readonly INotificationMsg _notificationMsg;

            public Role_Upd_cmd_Handeler(IMapper mapper, IRepositoryAsync<TblRolemaster> roles, INotificationMsg notificationMsg)
            {
                _mapper = mapper;
                _roles = roles;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Role_Upd_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblRolemaster obj = (_mapper.Map<TblRolemaster>(request));
                    TblRolemaster entity = await _roles.GetDetails(obj.RoleId);

                    if (entity != null)
                    {
                        obj.ModifiedOn = System.DateTime.Now;
                        int result = await _roles.UpdateAsync(obj, true, "roles");
                        if (result > 0)
                        {
                            Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Role Updated Succesfully " + request.RoleNmae, "Role Updated Succesfully " + request.RoleNmae));
                        }

                        response.ResponseObject = result;
                    }
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