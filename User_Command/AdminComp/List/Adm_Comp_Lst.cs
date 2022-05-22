using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;
using User_Command.AdminBranch.List;

namespace User_Command.AdminComp.List
{
    public class Adm_Comp_Lst : IRequest<Response>
    {
        public int OrgProdId { get; set; }

        public class Adm_Comp_LstHandler : IRequestHandler<Adm_Comp_Lst, Response>
        {
            private readonly INotificationMsg notificationMsg;
            private readonly IDapper<Response> aPPDbContext;

            public Adm_Comp_LstHandler(INotificationMsg notificationMsg,
                IDapper<Response> aPPDbContext
                )
            {
                this.notificationMsg = notificationMsg;
                this.aPPDbContext = aPPDbContext;
            }

            public async Task<Response> Handle(Adm_Comp_Lst request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    DynamicParameters param = new();

                    param.Add("@OrgProdId", request.OrgProdId);

                    response.ResponseObject = (await aPPDbContext.GetDataListAsync<Adm_Comp_Lst_DTO>("sp_AdminCompany_List", param, System.Data.CommandType.StoredProcedure)).ToList();
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