using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using MediatR;
using System.Net;

namespace User_Command.AdminBranch.List
{
    public class Adm_Bran_Lst : IRequest<Response>
    {
        public class Adm_Bran_LstHandler : IRequestHandler<Adm_Bran_Lst, Response>
        {
            private readonly INotificationMsg _notificationMsg;
            private readonly IDapper<Response> _aPPDbContext;

            public Adm_Bran_LstHandler(INotificationMsg notificationMsg,
                IDapper<Response> aPPDbContext
                )
            {
                _notificationMsg = notificationMsg;
                _aPPDbContext = aPPDbContext;
            }

            public async Task<Response> Handle(Adm_Bran_Lst request, CancellationToken cancellationToken)
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