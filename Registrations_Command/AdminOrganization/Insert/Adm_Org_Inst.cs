using Application_Core.Notification;
using Application_Database;
using Application_Services;
using MediatR;
using System.Net;

namespace Registrations_Command.AdminOrganization.Insert
{
    public class Adm_Org_Inst : IRequest<Response>
    {
        public string OrgName { get; set; }
        public string OrgEmail { get; set; }

        public class Adm_Org_InstHandler : IRequestHandler<Adm_Org_Inst, Response>
        {
            private readonly INotificationMsg _notificationMsg;
            private readonly APP_DbContext _aPPDbContext;

            public Adm_Org_InstHandler(INotificationMsg notificationMsg,
                                       APP_DbContext aPPDbContext)
            {
                _notificationMsg = notificationMsg;
                _aPPDbContext = aPPDbContext;
            }

            public async Task<Response> Handle(Adm_Org_Inst request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    List<sp_AdminOrganization_InsertResult>? data = await _aPPDbContext.Procedures.sp_AdminOrganization_InsertAsync(request.OrgName, request.OrgEmail, cancellationToken: cancellationToken);

                    if (data != null)
                    {
                        response.ResponseStatus = true;
                        response.ResponseObject = data[0].MSG;
                    }
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