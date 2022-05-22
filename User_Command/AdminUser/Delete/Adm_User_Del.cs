using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;

namespace User_Command.AdminUser.Delete
{
    public class Adm_User_Del : IRequest<Response>
    {
        public int UserId { get; set; }
        public int OrgProdId { get; set; }

        public class Adm_User_DelHandler : IRequestHandler<Adm_User_Del, Response>
        {
            private readonly INotificationMsg notificationMsg;
            private readonly IDapper<Response> aPPDbContext;

            public Adm_User_DelHandler(INotificationMsg notificationMsg,
                IDapper<Response> aPPDbContext
                )
            {
                this.notificationMsg = notificationMsg;
                this.aPPDbContext = aPPDbContext;
            }

            public async Task<Response> Handle(Adm_User_Del request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    DynamicParameters param = new();

                    param.Add("@UserId", request.UserId);
                    param.Add("@OrgProdId", request.OrgProdId);

                    response.ResponseObject = await aPPDbContext.ExecuteScalarAsync("sp_AdminUser_Delete", param, System.Data.CommandType.StoredProcedure);
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