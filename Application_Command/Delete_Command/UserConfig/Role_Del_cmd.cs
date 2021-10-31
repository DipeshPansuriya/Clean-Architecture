using Application_Core.Notification;
using Application_Core.Repositories;
using Application_Database;
using Application_Genric;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application_Command.Insert_Command.UserConfig
{
    public class Role_Del_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class Role_Del_cmd_Handeler : IRequestHandler<Role_Del_cmd, Response>
        {
            private readonly IRepositoryAsync<TblRolemaster> _roles;
            private readonly INotificationMsg _notificationMsg;

            public Role_Del_cmd_Handeler(IRepositoryAsync<TblRolemaster> roles, INotificationMsg notificationMsg)
            {
                _roles = roles;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Role_Del_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblRolemaster entity = await _roles.GetDetails(request.Id);

                    if (entity != null)
                    {
                        entity.IsDeleted = true;
                        entity.DeletedOn = System.DateTime.Now;

                        int result = await _roles.UpdateAsync(entity, true, "roles");
                        if (result > 0)
                        {
                            Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Role Delete Succesfully " + entity.RoleNmae, "Role Delete Succesfully " + entity.RoleNmae));
                        }

                        response.ResponseObject = request;
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