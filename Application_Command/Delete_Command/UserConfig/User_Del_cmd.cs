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
    public class User_Del_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class User_Del_cmd_Handeler : IRequestHandler<User_Del_cmd, Response>
        {
            private readonly IDapper<TblUsermaster> _user;
            private readonly INotificationMsg _notificationMsg;

            public User_Del_cmd_Handeler(IDapper<TblUsermaster> user, INotificationMsg notificationMsg)
            {
                _user = user;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(User_Del_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblUsermaster entity = await _user.GetDetails(request.Id);

                    if (entity != null)
                    {
                        entity.IsDeleted = true;
                        entity.DeletedOn = System.DateTime.Now;

                        int result = await _user.UpdateAsync(entity, true, "users");
                        if (result > 0)
                        {
                            Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "User Delete Succesfully " + entity.EmailId, "User Delete Succesfully " + entity.EmailId));
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