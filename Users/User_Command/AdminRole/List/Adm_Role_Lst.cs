using Application_Common;
using Application_Core.Notification;
using Application_Core.Repositories;
using Dapper;
using MediatR;
using System.Net;
using User_Command.AdminBranch.List;

namespace User_Command.AdminRole.List
{
    public class Adm_Role_Lst : IRequest<Response>
    {
        public int OrgProdId { get; set; }

        public class Adm_Role_LstHandler : IRequestHandler<Adm_Role_Lst, Response>
        {
            private readonly INotificationMsg notification;
            private readonly IDapper<Response> dapper;

            public Adm_Role_LstHandler(INotificationMsg notification,
                IDapper<Response> dapper
                )
            {
                this.notification = notification;
                this.dapper = dapper;
            }

            public async Task<Response> Handle(Adm_Role_Lst request, CancellationToken cancellationToken)
            {
                Response response = new Response();
                try
                {
                    DynamicParameters param = new();

                    param.Add("@OrgProdId", request.OrgProdId);

                    response.ResponseObject = (await dapper.GetDataListAsync<Adm_Role_Lst_DTO>("sp_AdminRole_List", param, System.Data.CommandType.StoredProcedure)).ToList();
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