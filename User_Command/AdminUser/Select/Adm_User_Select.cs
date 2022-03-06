using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using MediatR;
using System.Net;

namespace User_Command.AdminUser.Select
{
    public class Adm_User_Select : IRequest<Response>
    {
        public class Adm_User_SelectHandler : IRequestHandler<Adm_User_Select, Response>
        {
            private readonly INotificationMsg _notificationMsg;
            private readonly IDapper<Response> _aPPDbContext;

            public Adm_User_SelectHandler(INotificationMsg notificationMsg,
                IDapper<Response> aPPDbContext
                )
            {
                _notificationMsg = notificationMsg;
                _aPPDbContext = aPPDbContext;
            }

            public async Task<Response> Handle(Adm_User_Select request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    response.ResponseObject = "";
                }
                catch (Exception ex)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ResponseStatus = false;
                    response.ResponseObject = ex.Message + " ~ " + ex.InnerException;
                }
                return response;
            }
        }
    }
}