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
    public class Right_Del_cmd : IRequest<Response>
    {
        public int Id { get; set; }

        public class Right_Del_cmd_Handeler : IRequestHandler<Right_Del_cmd, Response>
        {
            private readonly IRepositoryAsync<TblRightmaster> _rights;
            private readonly INotificationMsg _notificationMsg;

            public Right_Del_cmd_Handeler(IRepositoryAsync<TblRightmaster> rights, INotificationMsg notificationMsg)
            {
                _rights = rights;
                _notificationMsg = notificationMsg;
            }

            public async Task<Response> Handle(Right_Del_cmd request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    TblRightmaster entity = await _rights.GetDetails(request.Id);

                    if (entity != null)
                    {
                        entity.IsDeleted = true;
                        entity.DeletedOn = System.DateTime.Now;

                        int result = await _rights.UpdateAsync(entity, true, "rights");
                        if (result > 0)
                        {
                            Parallel.Invoke(() => _notificationMsg.SaveMailNotification("dipeshpansuriya@ymail.com", "pansuriya.dipesh@gmail.com", "Right Delete Succesfully " + entity.RightId, "Right Delete Succesfully " + entity.RightId));
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