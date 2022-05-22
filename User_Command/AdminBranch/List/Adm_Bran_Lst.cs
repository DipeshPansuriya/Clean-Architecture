using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;

namespace User_Command.AdminBranch.List
{
    public class Adm_Bran_Lst : IRequest<Response>
    {
        public int OrgProdId { get; set; }

        public class Adm_Bran_LstHandler : IRequestHandler<Adm_Bran_Lst, Response>
        {
            private readonly INotificationMsg notificationMsg;
            private readonly IDapper<Response> aPPDbContext;

            public Adm_Bran_LstHandler(INotificationMsg notificationMsg,
                IDapper<Response> aPPDbContext
                )
            {
                this.notificationMsg = notificationMsg;
                this.aPPDbContext = aPPDbContext;
            }

            public async Task<Response> Handle(Adm_Bran_Lst request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    DynamicParameters param = new();

                    param.Add("@OrgProdId", request.OrgProdId);

                    response.ResponseObject = (await aPPDbContext.GetDataListAsync<Adm_Bran_Lst_DTO>("sp_AdminBranch_List", param, System.Data.CommandType.StoredProcedure)).ToList();
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